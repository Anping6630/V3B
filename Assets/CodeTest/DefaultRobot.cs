using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultRobot : MonoBehaviour
{
    [Header("角色控制器")]
    public CharacterController controller;
    [Header("攝影機")]
    public GameObject robotCamera;
    [Header("UI介面")]
    public GameObject hackMark; 
    [Header("視角靈敏度")]
    public float mouseSensitivity;
    [Header("移動速度")]
    public float speed;
    [Header("玩家操作中")]
    public bool isControlling;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        robotCamera.SetActive(isControlling);
        if (isControlling)
        {
            FirstPersonLook();
            Movement();
            Transfer();
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

    void Movement()//移動
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    void Transfer()//附身
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.tag == "Robot")
            {
                hackMark.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    isControlling = false;
                    hackMark.SetActive(false);
                    hit.transform.parent.gameObject.GetComponent<DefaultRobot>().isControlling = true;
                }
            }
            else
            {
                hackMark.SetActive(false);
            }
        }
        else
        {
            hackMark.SetActive(false);
        }
    }
}
