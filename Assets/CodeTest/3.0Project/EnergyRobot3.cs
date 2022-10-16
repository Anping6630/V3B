using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRobot3 : MonoBehaviour
{
    [Header("角色控制器")]
    public CharacterController controller;
    [Header("模型頭部")]
    public GameObject robotHead;
    [Header("模型腿部")]
    public GameObject robotFeet;
    [Header("攝影機")]
    public Camera robotCamera;
    [Header("視角靈敏度")]
    public float mouseSensitivity;
    [Header("移動速度")]
    public float moveSpeed;

    public bool isAct;

    //攝影機角度//
    float xRotation = 0f;

    void Start()
    {

    }

    void Update()
    {
        if (isAct)
        {
            FirstPersonLook();
            Movement();
            AimPoint();
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

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void AimPoint()//準心偵測
    {
        Ray ray = robotCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "Robot":
                    if (Input.GetMouseButtonDown(0))
                    {
                        print("附身");
                    }
                    break;
                case "ChargingPort":
                    if (Input.GetMouseButtonDown(0))
                    {
                        print("開門");
                    }
                    break;
            }
        }
    }
}
