using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotmove : MonoBehaviour
{
    public float speed=3.0f;
    public float rotatespeed = 1.0f;
    public float speed_x_constraction;
    private Transform tran;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        tran = gameObject.GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Rotate(0, h * rotatespeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * v;
       


        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
