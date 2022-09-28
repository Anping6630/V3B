using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRobot : MonoBehaviour
{
    [Header("頭部")]
    public GameObject robotHead;
    [Header("腿部")]
    public GameObject robotFeet;
    [Header("攝影機")]
    public GameObject robotCamera;
    [Header("視角靈敏度")]
    public float mouseSensitivity;

    public GameObject[] pathPoints;
    int nextPathPointIndex = 1;
    int lastPathPointIndex = 0;

    float xRotation = 0f;

    void Start()
    {
        pathPoints = GameObject.FindGameObjectsWithTag("PathPoint");
        transform.position = pathPoints[0].transform.position;
        transform.forward = pathPoints[nextPathPointIndex].transform.position - transform.position;
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
        robotHead.transform.Rotate(Vector3.up * mouseX);
    }

    void Movement()//軌道移動
    {
        if (Vector3.Distance(pathPoints[nextPathPointIndex].transform.position, transform.position) < 0.1f && Input.GetKey("w"))
        {
            if (nextPathPointIndex != pathPoints.Length - 1)
            {
                nextPathPointIndex++;
                lastPathPointIndex++;
            }
            if (Vector3.Distance(pathPoints[pathPoints.Length - 1].transform.position, transform.position) < 0.1f)
            {
                transform.position = pathPoints[pathPoints.Length - 1].transform.position;
                return;
            }
            transform.forward = pathPoints[nextPathPointIndex].transform.position - transform.position;
        }
        if (Vector3.Distance(pathPoints[lastPathPointIndex].transform.position, transform.position) < 0.1f && Input.GetKey("s"))
        {
            if (nextPathPointIndex != 0)
            {
                nextPathPointIndex--;
                lastPathPointIndex--;
            }
            if (Vector3.Distance(pathPoints[pathPoints.Length - 1].transform.position, transform.position) < 0.1f)
            {
                transform.position = pathPoints[pathPoints.Length - 1].transform.position;
                return;
            }
            transform.forward = -(pathPoints[lastPathPointIndex].transform.position - transform.position);
        }
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * 10 * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.back * 10 * Time.deltaTime, Space.Self);
        }
    }
}
