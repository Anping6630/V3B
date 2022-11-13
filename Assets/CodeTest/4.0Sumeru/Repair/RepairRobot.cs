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
    [Header("修復中材質")]
    public Material repairing_M;
    [Header("修復完成材質")]
    public Material repaired_M;

    public GameObject[] List;

    Rigidbody rb;
    float xRotation = 0f;


    Mesh holdingBlueprint;

    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        FirstPersonLook();
        Movement();
        GetBluePrint();
        Repair();
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

    void GetBluePrint()
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

    void Repair()
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        switch(body.transform.gameObject.tag)
        {
            case "Normal":
                break;
            case "Fixing":
                break;
            //case "Fixed"
            //    break;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.transform.gameObject.tag)
        {
            case "Normal":
                other.transform.gameObject.GetComponent<MeshRenderer>().material = repairing_M;
                other.transform.gameObject.tag = "Repairing";
                break;
            case "Node":
                other.transform.gameObject.GetComponent<MeshRenderer>().material = repairing_M;
                other.transform.gameObject.tag = "Repairing";
                break;
            case "Repaired":
                for(int i = 0; i < List.Length; i++)
                {
                    List[i].GetComponent<Rigidbody>().useGravity = true;
                    List[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
                break;
        }
    }
    
    void OnCollisionExit(Collision other)
    {
        switch (other.transform.gameObject.tag)
        {
            case "Repairing":
                other.transform.gameObject.GetComponent<MeshRenderer>().material = repaired_M;
                other.transform.gameObject.tag = "Repaired";
                break;
        }
    }
}
