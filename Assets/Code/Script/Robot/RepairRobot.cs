using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RepairRobot: MonoBehaviour
{
    [Header("機器人攝影機")]
    public Camera robotCamera;
    [Header("視角靈敏度")]
    public float mouseSensitivity;
    [Header("移動速度")]
    public float moveSpeed;
    [Header("機器人UI")]
    public GameObject robotUI;
    [Header("普通地板")]
    public Mesh normal_M;
    [Header("節點地板")]
    public Mesh node_M;
    [Header("修復中地板")]
    public Mesh repairing_M;
    [Header("修復完成地板")]
    public Mesh repaired_M;
    [Header("終點地板")]
    public GameObject goalFloor;

    [Header("斷橋音效")]
    public AudioClip bridgeBreak_S;
    [Header("修橋音效")]
    public AudioClip bridgeFix_S;
    [Header("取消附身音效")]
    public AudioClip untransfer_S;

    public GameObject[] normalFloor;
    public GameObject[] nodeFloor;

    Rigidbody rb;
    float xRotation = 0f;

    Mesh holdingBlueprint;

    public Text UI;
    float uiTimer;

    //正在操作此機器人//
    bool isControlling;
    //能源機器人攝影機//
    Camera energyRobotCamera;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        normalFloor = GameObject.FindGameObjectsWithTag("Normal");
        nodeFloor = GameObject.FindGameObjectsWithTag("Node");

        isControlling = false;
        robotCamera.gameObject.SetActive(false);
        robotUI.gameObject.SetActive(false);
        energyRobotCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (isControlling)
        {
            FirstPersonLook();
            Movement();
            GetBluePrint();
            Repair();

            if (Input.GetKeyDown("q"))//按Q取消附身
            {
                if (Vector3.Distance(energyRobotCamera.transform.position, transform.position) < 6)
                {
                    Transferred(false);
                    this.GetComponent<AudioSource>().PlayOneShot(untransfer_S);
                }
                else
                {
                    UI.text = "距離能源機器人過遠";
                    uiTimer = 0;
                }
            }

            if (Input.GetKeyDown("0"))
            {
                RecordPointManager.BackToRecord(this.gameObject);
            }
        }
        uiTimer += Time.deltaTime;
        if (uiTimer > 1)
        {
            UI.text = "";
        }
    }

    void FirstPersonLook()//第一人稱鏡頭
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        robotCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Movement()//剛體移動
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalMove + transform.right * horizontalMove;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Impulse);
    }

    void GetBluePrint()//取得藍圖
    {
        if (Input.GetKeyDown("e"))
        {
            Vector3 rayOrigin = robotCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, robotCamera.transform.forward, out hit, 50f))
            {
                switch (hit.transform.gameObject.tag)
                {
                    case "FineObject"://命中狀態良好物件
                        holdingBlueprint = hit.transform.gameObject.GetComponent<MeshFilter>().mesh;
                        print("已複製" + holdingBlueprint);
                        break;
                }
            }
        }
    }

    void Repair()//修復物件
    {
        if (Input.GetMouseButtonDown(0) && holdingBlueprint != null)
        {
            Vector3 rayOrigin = robotCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, robotCamera.transform.forward, out hit, 50f))
            {
                switch (hit.transform.gameObject.tag)
                {
                    case "BrokenObject"://命中狀態良好物件
                        hit.transform.gameObject.GetComponent<MeshFilter>().mesh = holdingBlueprint;
                        break;
                }
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.transform.gameObject.tag)
        {
            case "Normal"://正常地板
                other.transform.gameObject.GetComponent<MeshFilter>().mesh = repairing_M; 
                other.transform.gameObject.tag = "Repairing";
                GetComponent<AudioSource>().PlayOneShot(bridgeFix_S);
                break;
            case "Node"://節點地板
                other.transform.gameObject.GetComponent<MeshFilter>().mesh = repairing_M;
                other.transform.gameObject.tag = "Repairing";
                break;
            case "Repaired"://已修復地板
                BridgeBreak();
                break;
            case "BackZone":
                if(normalFloor[0].tag != "Complete")
                {
                    BridgeBreak();
                }
                break;
        }
        if (other.transform.gameObject == goalFloor)//過關判定
        {
            if (isComplete())
            {
                BridgeComplete();
            }
            else
            {
                BridgeBreak();
            }
        }
    }

    private bool isComplete()
    {
        bool isComplete = true;
        for (int i = 0; i < nodeFloor.Length; i++)
        {
            if (nodeFloor[i].tag != "Repaired")
            {
                isComplete = false;
            }
            if (nodeFloor[0].tag == "Complete")
            {
                isComplete = true;
            }
        }
        return isComplete;
    }

    void OnCollision(Collision other)
    {
        switch (other.transform.gameObject.tag)
        {
            case "Repaired"://已修復地板
                BridgeBreak();
                break;
        }
    }

    void OnCollisionExit(Collision other)
    {
        switch (other.transform.gameObject.tag)
        {
            case "Repairing":
                StartCoroutine(ToRepaired(other.gameObject));
                break;
        }
    }

    IEnumerator ToRepaired(GameObject i)//修復中變為修復完成
    {
        yield return new WaitForSeconds(1);
        i.transform.gameObject.GetComponent<MeshFilter>().mesh = repaired_M;
        i.transform.gameObject.tag = "Repaired";
    }

    void BridgeComplete()//修橋成功
    {
        for (int i = 0; i < normalFloor.Length; i++)
        {
            normalFloor[i].tag = "Complete";
            normalFloor[i].GetComponent<MeshFilter>().mesh = repaired_M;
        }
        for (int i = 0; i < nodeFloor.Length; i++)
        {
            nodeFloor[i].tag = "Complete";
            nodeFloor[i].GetComponent<MeshFilter>().mesh = repaired_M;
        }
    }
 
    public void BridgeBreak()//失敗斷橋
    {
        GetComponent<AudioSource>().PlayOneShot(bridgeBreak_S);
        transform.position = new Vector3(33.28f, 1.51f, 7.5f);

        for (int i = 0; i < normalFloor.Length; i++)
        {
            normalFloor[i].tag = "Normal";
            normalFloor[i].GetComponent<MeshFilter>().mesh = normal_M;
        }
        for (int i = 0; i < nodeFloor.Length; i++)
        {
            nodeFloor[i].tag = "Complete";
            nodeFloor[i].GetComponent<MeshFilter>().mesh = node_M;
        }
    }

    public void Transferred(bool isTransferred)//附身處理
    {
        isControlling = isTransferred;
        robotCamera.gameObject.SetActive(isTransferred);
        robotUI.gameObject.SetActive(isTransferred);
        energyRobotCamera.gameObject.SetActive(!isTransferred);

        if (!isTransferred)
        {
            GameObject.Find("EnergyRobot").GetComponent<EnergyRobot3>().CancelTransfer();
        }
    }
}
