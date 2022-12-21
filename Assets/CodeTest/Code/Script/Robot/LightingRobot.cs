using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Header("點狀頭燈")]
    public GameObject robotSpotLight;
    [Header("機器人UI")]
    public GameObject robotUI;
    [Header("密碼輸入面板")]
    public GameObject inputPanel;
    [Header("摩斯密碼燈")]
    public GameObject morseLight;
    [Header("是否能被操控")]
    public bool isControllable;

    [Header("開關燈音效")]
    public AudioClip lightSwitch_S;
    [Header("取消附身音效")]
    public AudioClip untransfer_S;

    public Text UI;
    float uiTimer;

    //正在操作此機器人//
    bool isControlling;
    //能源機器人攝影機//
    Camera energyRobotCamera;
    //頭燈開關//
    bool isLightOpen;
    //頭燈類型//
    bool isLightSpot;
    //視角//
    float xRotation = 0f;

    void Start()//初始化(基本上就是把所有東東關掉)
    {
        robotSpotLight.SetActive(false);
        isControlling = false;
        robotCamera.gameObject.SetActive(false);
        robotUI.gameObject.SetActive(false);
        energyRobotCamera = GameObject.Find("MainCamera").GetComponent<Camera>();

        if (isControllable)
        {
            PowerUp();
        }
    }

    void Update()
    {
        if (!isControllable)
        {
            if (Vector3.Distance(energyRobotCamera.transform.position, transform.position) < 7)//玩家第一次接近
            {
                inputPanel.GetComponent<Animator>().SetBool("enter", true);
            }
        }
        if (isControlling)
        {
            FirstPersonLook();
            Movement();
            if (Input.GetKeyDown("q"))//按Q取消附身
            {
                if(Vector3.Distance(energyRobotCamera.transform.position, transform.position) < 6)
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
            if (Input.GetKeyDown("e"))//電燈開關
            {
                isLightOpen = !isLightOpen;
                robotSpotLight.SetActive(isLightOpen);
                this.GetComponent<AudioSource>().PlayOneShot(lightSwitch_S);
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
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 50f);

        robotCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        robotSpotLight.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void Movement()//前後移動，左右自轉
    {
        float z = Input.GetAxis("Vertical");
        this.transform.Rotate(0f, Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime * 20, 0f);

        Vector3 move = transform.forward * z;
        move.y -= 20f * Time.deltaTime;
        controller.Move(move * moveSpeed * Time.deltaTime);
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

    public void PowerUp()//摩斯密碼正確，改為可以被附身
    {
        isControllable = true;
        robotSpotLight.SetActive(true);
        isLightSpot = true;
        isLightOpen = true;
        this.tag = "Robot";
        inputPanel.SetActive(false);
    }

    void OnControllerColliderHit(ControllerColliderHit other)//1.5關地板
    {
        switch (other.transform.gameObject.tag)
        {
            case "Normal"://正常地板
                transform.position = new Vector3(33.28f, 1.51f, 7.5f);
                break;
            case "PasswordCard"://密碼卡
                other.gameObject.GetComponent<PasswordCard>().GetPassword();
                break;
        }
    }
}
