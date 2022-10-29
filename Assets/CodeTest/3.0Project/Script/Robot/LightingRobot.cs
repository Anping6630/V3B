using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingRobot : MonoBehaviour
{
    [Header("角色控制器")]
    public CharacterController controller;
    [Header("攝影機")]
    public Camera robotCamera;
    [Header("視角靈敏度")]
    public float mouseSensitivity;
    [Header("移動速度")]
    public float moveSpeed;
    [Header("密碼輸入面板")]
    public GameObject inputPanel;
    [Header("摩斯密碼表")]
    public GameObject morseCodePanel;

    //是否能夠被操縱//
    bool isControllable;
    //正在操作此機器人//
    bool isControlling;
    //能源機器人攝影機//
    Camera energyRobotCamera;
    //視角//
    float xRotation = 0f;

    void Start()
    {
        isControlling = false;
        isControllable = false;
        robotCamera.gameObject.SetActive(false);
        energyRobotCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (!isControllable)
        {
            if(Vector3.Distance(energyRobotCamera.transform.position, transform.position) < 7)
            {
                inputPanel.SetActive(true);
                morseCodePanel.SetActive(true);
            }
            else
            {
                inputPanel.SetActive(false);
                morseCodePanel.SetActive(false);
            }
        }
        if (isControlling)
        {
            FirstPersonLook();
            Movement();
            if (Input.GetKeyDown("f"))//按F取消附身
            {
                Transferred(false);
            }
        }

    }

    void FirstPersonLook()//第一人稱鏡頭
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 50f);

        robotCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void Movement()//前後移動，左右自轉
    {
        float z = Input.GetAxis("Vertical");
        this.transform.Rotate(0f, Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime*20 ,0f);

        Vector3 move = transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    public void Transferred(bool isTransferred)//附身
    {
        isControlling = isTransferred;
        robotCamera.gameObject.SetActive(isTransferred);
        energyRobotCamera.gameObject.SetActive(!isTransferred);
    }

    public void PowerUp()//摩斯密碼正確，改為可以被附身
    {
        isControllable = true;
        this.tag = "Robot";
    }
}
