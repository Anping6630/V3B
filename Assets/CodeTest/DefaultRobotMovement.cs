using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultRobotMovement : MonoBehaviour
{
    [Header("角色控制器")]
    public CharacterController controller;
    [Header("移動速度")]
    public float speed;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move*speed*Time.deltaTime);
    }
}
