using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //玩家是否操作其他機器人中//
    public GameObject ControllingRobot = null;

    //攝影機角度//
    float xRotation = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (ControllingRobot == null)//沒有操作其他機器人時
        {
            FirstPersonLook();
            Movement();
            AimPoint();
        }
        else
        {
            if (Input.GetKeyDown("f"))//按F取消附身
            {
                ControllingRobot = null;
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
                        Transfer(hit.collider.gameObject);
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

    void Transfer(GameObject target)//附身
    {
        target.GetComponent<Controllable>().Transfer();
        ControllingRobot = target;
    }
}
