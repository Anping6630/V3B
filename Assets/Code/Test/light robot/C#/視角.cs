using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 視角 : MonoBehaviour
{
    //水平速度
    public float HorizontalSpeed = 2.0F;
    //垂直速度
    public float VerticalSpeed = 2.0F;
    [Header("攝影機")]
    public Camera robotCamera;
    [Header("視角靈敏度")]
    public float mouseSensitivity;
    //攝影機角度//
    float xRotation = 0f;
    float yRotation = 0f;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FirstPersonLook();
        void FirstPersonLook()//第一人稱鏡頭
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            yRotation += mouseX;

            //角度限制//
            xRotation = Mathf.Clamp(xRotation, -50f, 50f);
            yRotation = Mathf.Clamp(yRotation, -50f, 50f);
            yRotation = Mathf.Clamp(yRotation, -50f, 50f);

            robotCamera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            //transform.localRotation = Quaternion.Euler(0f, yRotation, 0f); 這段等模型出來重修
        }
        Mathf.Clamp(xRotation, -90f, 90f);
    }
}
