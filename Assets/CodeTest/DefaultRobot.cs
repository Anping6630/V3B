using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultRobot : MonoBehaviour
{
    [Header("角色控制器")]
    public CharacterController controller;
    [Header("攝影機")]
    public GameObject robotCamera;
    [Header("視角靈敏度")]
    public float mouseSensitivity;
    [Header("移動速度")]
    public float speed;
    [Header("操作中")]
    public bool isControlling;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (isControlling)
        {
            LookFor();
            Movement();
        }
        robotCamera.SetActive(isControlling);
    }

    void LookFor()
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

        controller.Move(move * speed * Time.deltaTime);
    }
}
