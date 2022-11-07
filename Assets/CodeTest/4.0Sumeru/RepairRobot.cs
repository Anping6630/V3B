using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RepairRobot: MonoBehaviour
{
    //������������ɰO�o�ˬd��Ӿ����H

    [Header("���ⱱ�")]
    public CharacterController controller;
    [Header("��v��")]
    public Camera robotCamera;
    [Header("�����F�ӫ�")]
    public float mouseSensitivity;
    [Header("���ʳt��")]
    public float moveSpeed;

    //��v��//
    float xRotation = 0f;

    void Awake()
    {

    }

    void Update()
    {
            FirstPersonLook();
            Movement();
    }

    void FirstPersonLook()//�Ĥ@�H�����Y
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        robotCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Movement()//�ۥѲ���
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move.y -= 20f * Time.deltaTime;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other){ } // �}�l��Ĳ�����|�I�s�@��
    void OnTriggerStay(Collider other) { } // ��Ĳ�����|����I�s
    void OnTriggerExit(Collider other) { }�@// ���Ĳ�����|�I�s�@��
}
