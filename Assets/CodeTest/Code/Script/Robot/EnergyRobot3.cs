using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class EnergyRobot3 : MonoBehaviour
{
    [Header("角色控制器")]
    public CharacterController controller;
    [Header("攝影機")]
    public Camera robotCamera;
    [Header("視角靈敏度")]
    public float mouseSensitivity;
    [Header("移動速度")]
    public float moveSpeed;
    [Header("機器人UI")]
    public GameObject robotUI;
    [Header("能源發射點")]
    public Transform energyOrigin;
    [Header("能源槍模型")]
    public GameObject energyGun;
    public float gunRange = 50f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;
    [Header("灌注能源音效")]
    public AudioClip shoot_S;
    [Header("附身音效")]
    public AudioClip transfer_S;

    public GameObject target;
    public Vector2 pos;

    [Header("是否控制其他機器人中")]
    public bool isTransfering;
    //攝影機//
    float xRotation = 0f;
    //灌注能源//
    LineRenderer laserLine;
    float infuseTimer;

    void Awake()
    {
        laserLine = energyGun.GetComponent<LineRenderer>();
        laserLine.enabled = false;
    }

    void Update()
    {
        if (!isTransfering)//沒有操作其他機器人時
        {
            FirstPersonLook();
            Movement();
            Aim();
            InfuseEnergy();
            RetakeEnergy();


            if (Input.GetKeyDown("0"))
            {
                RecordPointManager.BackToRecord(this.gameObject);
            }
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

    void Movement()//自由移動
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move.y -= 98f * Time.deltaTime;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void Aim()//臨時用開鏡瞄準
    {
        if (Input.GetMouseButton(1))
        {
            if(robotCamera.fieldOfView > 30)
            {
                robotCamera.fieldOfView-=1f;
            }
        }
        else
        {
            if (robotCamera.fieldOfView < 60)
            {
                robotCamera.fieldOfView+=1f;
            }
        }
    }

    void InfuseEnergy()//灌注能源
    {
        infuseTimer += Time.deltaTime;
        laserLine.SetPosition(0, energyOrigin.position);

        if (Input.GetMouseButtonDown(0) && infuseTimer > fireRate)
        {
            infuseTimer = 0;

            Vector3 rayOrigin = robotCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, robotCamera.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);

                switch (hit.transform.gameObject.tag)
                {
                    case "Robot"://命中機器人
                        Transfer(hit.collider.gameObject);
                        break;

                    case "ChargingPort"://命中能源孔
                        hit.collider.gameObject.GetComponent<ChargingPort>().Energy(true);
                        break;
                }
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("UI"))//命中UI按鈕
                {
                    ExecuteEvents.Execute<IPointerClickHandler>(hit.collider.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (robotCamera.transform.forward * gunRange));
            }
            StartCoroutine(ShootEnergy());
        }
    }

    IEnumerator ShootEnergy()//發射能源光束
    {
        laserLine.enabled = true;
        this.GetComponent<AudioSource>().PlayOneShot(shoot_S);
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }

    void RetakeEnergy()//收回能源
    {
        if (Input.GetKeyDown("e"))
        {
            Vector3 rayOrigin = robotCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, robotCamera.transform.forward, out hit, gunRange))
            {
                switch (hit.transform.gameObject.tag)
                {
                    case "ChargingPort"://收回能源孔
                        if (hit.collider.gameObject.GetComponent<ChargingPort>().isCharge)
                        {
                            hit.collider.gameObject.GetComponent<ChargingPort>().Energy(false);
                        }
                        break;
                }
            }
        }
    }

    void Transfer(GameObject target)
    {
        isTransfering = true;
        robotUI.SetActive(false);
        target.GetComponent<Controllable>().Transfer();
        this.GetComponent<AudioSource>().PlayOneShot(transfer_S);
    }

    public void CancelTransfer()
    {
        robotUI.SetActive(true);
        isTransfering = false;
    }

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        switch (other.transform.gameObject.tag)
        {
            case "Normal"://正常地板
                transform.position = new Vector3(33.28f, 1.51f, 7.5f);
                break;
            case "BackZone"://重來區域
                RecordPointManager.BackToRecord(this.gameObject);
                break;
            case "PasswordCard"://密碼卡
                other.gameObject.GetComponent<PasswordCard>().GetPassword();
                break;
        }
    }

    public void GameStart()
    {
        robotCamera.gameObject.GetComponent<VideoPlayer>().enabled = false;
        isTransfering = false;
        robotUI.SetActive(true);
    }
}
