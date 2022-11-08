using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RepairRobot: MonoBehaviour
{
    [Header("角色控制器")]
    public CharacterController controller;
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

    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        FirstPersonLook();
        //Movement();
        Movementt();
    }

    void FirstPersonLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        robotCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move.y -= 20f * Time.deltaTime;

        controller.Move(move * moveSpeed * Time.deltaTime);
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

    void Movementt()//移動
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 moveDirection= transform.forward * verticalMove + transform.right * horizontalMove;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Impulse);
    }
}
