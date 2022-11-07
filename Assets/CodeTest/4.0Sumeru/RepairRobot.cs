using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RepairRobot: MonoBehaviour
{
    //附身機制改按鍵時記得檢查兩個機器人

    [Header("角色控制器")]
    public CharacterController controller;
    [Header("攝影機")]
    public Camera robotCamera;
    [Header("視角靈敏度")]
    public float mouseSensitivity;
    [Header("移動速度")]
    public float moveSpeed;

    //攝影機//
    float xRotation = 0f;

    void Awake()
    {

    }

    void Update()
    {
            FirstPersonLook();
            Movement();
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
        move.y -= 20f * Time.deltaTime;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other){ } // 開始接觸瞬間會呼叫一次
    void OnTriggerStay(Collider other) { } // 接觸期間會持續呼叫
    void OnTriggerExit(Collider other) { }　// 停止接觸瞬間會呼叫一次
}
