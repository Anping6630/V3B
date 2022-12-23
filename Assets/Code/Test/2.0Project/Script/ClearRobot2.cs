using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRobot : MonoBehaviour
{
    [Header("模型頭部")]
    public GameObject robotHead;
    [Header("模型腿部")]
    public GameObject robotFeet;
    [Header("攝影機")]
    public GameObject robotCamera;
    [Header("視角靈敏度")]
    public float mouseSensitivity;
    [Header("移動速度")]
    public float moveSpeed;

    public bool isAct;

    //軌道節點//
    GameObject[] pathPoints;
    GameObject nextPoint;
    GameObject lastPoint;

    //清道機器人//
    bool isLookingForward = true;

    //攝影機角度//
    float xRotation = 0f;

    void Start()
    {
        pathPoints = GameObject.FindGameObjectsWithTag("PathPoint");//抓取所有節點

        int Ahoy = Random.Range(0, pathPoints.Length - 1);//扔上隨機起點(記得刪掉嘿
        transform.position = pathPoints[Ahoy].transform.position;
        nextPoint = pathPoints[Ahoy];
        lastPoint = pathPoints[Ahoy].GetComponent<PathPoint>().connectingPoints[0];
    }

    void Update()
    {
        if (isAct)
        {
            FirstPersonLook();
            Movement();

            if (Input.GetKeyDown("q"))
            {
                isLookingForward = !isLookingForward;
            }
        }

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
        FindPath(true);
        if (isLookingForward)
        {
            transform.Translate((nextPoint.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate((lastPoint.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime, Space.World);
        }
        float a = Vector3.Dot(robotCamera.transform.forward.normalized, (nextPoint.transform.position - transform.position).normalized);//視野方向與下個節點的夾角
        float b = Vector3.Dot(robotCamera.transform.forward.normalized, (lastPoint.transform.position - transform.position).normalized);//視野方向與上個節點的夾角

        if (Input.GetKey("a") && moveSpeed<15)
        {
            moveSpeed += 1;
        }
        if (moveSpeed > 0.1f)
        {
            moveSpeed -= 0.05f;
        }


        if (Input.GetKey("w"))
        {
            if (a > b)//向鏡頭朝向的節點移動
            {
                FindPath(true);
                transform.Translate((nextPoint.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                FindPath(true);
                transform.Translate((lastPoint.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        if (Input.GetKey("s"))
        {
            if (a < b)//向鏡頭背對的節點移動
            {
                FindPath(false);
                transform.Translate((nextPoint.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                FindPath(false);
                transform.Translate((lastPoint.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    void FindPath(bool isForward)//尋找下一個節點
    {
        if (Vector3.Distance(nextPoint.transform.position, transform.position) < 0.1f)
        {
            lastPoint = nextPoint;
            GameObject maxTarget = nextPoint.GetComponent<PathPoint>().connectingPoints[0];
            GameObject minTarget = maxTarget;
            float max = Vector3.Dot(robotCamera.transform.forward.normalized, (nextPoint.GetComponent<PathPoint>().connectingPoints[0].transform.position - transform.position).normalized);
            float min = max;
            for (int i = 0; i < nextPoint.GetComponent<PathPoint>().connectingPoints.Length; i++)
            {
                if (Vector3.Dot(robotCamera.transform.forward.normalized, (nextPoint.GetComponent<PathPoint>().connectingPoints[i].transform.position - transform.position).normalized) >= max)
                {
                    max = Vector3.Dot(robotCamera.transform.forward.normalized, (nextPoint.GetComponent<PathPoint>().connectingPoints[i].transform.position - transform.position).normalized);
                    maxTarget = nextPoint.GetComponent<PathPoint>().connectingPoints[i];
                }
                if (Vector3.Dot(robotCamera.transform.forward.normalized, (nextPoint.GetComponent<PathPoint>().connectingPoints[i].transform.position - transform.position).normalized) <= min)
                {
                    min = Vector3.Dot(robotCamera.transform.forward.normalized, (nextPoint.GetComponent<PathPoint>().connectingPoints[i].transform.position - transform.position).normalized);
                    minTarget = nextPoint.GetComponent<PathPoint>().connectingPoints[i];
                }
            }
            if (isForward)
            {
                nextPoint = maxTarget;
            }
            else
            {
                nextPoint = minTarget;
            }
        }
        if (Vector3.Distance(lastPoint.transform.position, transform.position) < 0.1f)
        {
            nextPoint = lastPoint;
            GameObject maxTarget = lastPoint.GetComponent<PathPoint>().connectingPoints[0];
            GameObject minTarget = maxTarget;
            float max = Vector3.Dot(robotCamera.transform.forward.normalized, (lastPoint.GetComponent<PathPoint>().connectingPoints[0].transform.position - transform.position).normalized);
            float min = max;
            for (int i = 0; i < lastPoint.GetComponent<PathPoint>().connectingPoints.Length; i++)
            {
                if (Vector3.Dot(robotCamera.transform.forward.normalized, (lastPoint.GetComponent<PathPoint>().connectingPoints[i].transform.position - transform.position).normalized) >= max)
                {
                    max = Vector3.Dot(robotCamera.transform.forward.normalized, (lastPoint.GetComponent<PathPoint>().connectingPoints[i].transform.position - transform.position).normalized);
                    maxTarget = lastPoint.GetComponent<PathPoint>().connectingPoints[i];
                }
                if (Vector3.Dot(robotCamera.transform.forward.normalized, (lastPoint.GetComponent<PathPoint>().connectingPoints[i].transform.position - transform.position).normalized) <= min)
                {
                    min = Vector3.Dot(robotCamera.transform.forward.normalized, (lastPoint.GetComponent<PathPoint>().connectingPoints[i].transform.position - transform.position).normalized);
                    minTarget = lastPoint.GetComponent<PathPoint>().connectingPoints[i];
                }
            }
            if (isForward)
            {
                lastPoint = maxTarget;
            }
            else
            {
                lastPoint = minTarget;
            }
        }
    }
}
