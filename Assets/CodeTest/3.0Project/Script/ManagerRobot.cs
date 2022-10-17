using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerRobot : MonoBehaviour
{
    [Header("攝影機")]
    public Camera robotCamera;
    [Header("視角靈敏度")]
    public float mouseSensitivity;

    //正在操作此機器人//
    bool isControlling;
    //能源機器人攝影機//
    Camera energyRobotCamera;
    //攝影機角度//
    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        isControlling = false;
        robotCamera.gameObject.SetActive(false);
        energyRobotCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (isControlling)
        {
            FirstPersonLook();

            if (Input.GetKeyDown("f"))//按F取消附身
            {
                Transferred(false);
            }
        }
    }

    void FirstPersonLook()//第一人稱鏡頭
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        yRotation += mouseX;

        //角度限制//
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        robotCamera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //transform.localRotation = Quaternion.Euler(0f, yRotation, 0f); 這段等模型出來重修
    }

    public void Transferred(bool isTransferred)//附身
    {
        isControlling = isTransferred;
        robotCamera.gameObject.SetActive(isTransferred);
        energyRobotCamera.gameObject.SetActive(!isTransferred);
    }
}
