using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyRobot3 : MonoBehaviour
{
    [Header("角色控制器")]
    public CharacterController controller;
    [Header("攝影機")]
    public Camera robotCamera;
    [Header("視角靈敏度")]
    public float mouseSensitivity;
    [Header("移動速度")]
    public float moveSpeed;
    [Header("準心UI")]
    public GameObject aimPoint;

    //玩家是否操作其他機器人中//
    public GameObject ControllingRobot = null;

    //攝影機角度//
    float xRotation = 0f;

    [Header("能源發射點")]
    public Transform energyOrigin;
    [Header("能源槍模型")]
    public GameObject energyGun;
    public float gunRange = 50f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;

    LineRenderer laserLine;
    float fireTimer;



    void Awake()
    {
        laserLine = energyGun.GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        if (ControllingRobot == null)//沒有操作其他機器人時
        {
            FirstPersonLook();
            Movement();
            AimPoint();
            InfuseEnergy();
        }
        else
        {
            if (Input.GetKeyDown("f"))//按F取消附身
            {
                ControllingRobot = null;
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
        transform.Rotate(Vector3.up * mouseX);
    }

    void Movement()//自由移動
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void AimPoint()//準心偵測
    {
        //臨時用開鏡瞄準//
        if (Input.GetMouseButton(1))
        {
            aimPoint.SetActive(true);
            if(robotCamera.fieldOfView > 30)
            {
                robotCamera.fieldOfView-=0.5f;
            }
        }
        else
        {
            aimPoint.SetActive(false);
            if (robotCamera.fieldOfView < 60)
            {
                robotCamera.fieldOfView+=0.5f;
            }
        }


        Ray ray = robotCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "Robot":
                    if (Input.GetMouseButtonDown(0))
                    {
                        Transfer(hit.collider.gameObject);
                    }
                    break;
                case "ChargingPort":
                    if (Input.GetMouseButtonDown(0))
                    {
                        print("開門");
                    }
                    break;
            }
        }
    }

    void InfuseEnergy()//灌注能源
    {
        fireTimer += Time.deltaTime;

        if (Input.GetMouseButtonUp(1) && fireTimer > fireRate)
        {
            fireTimer = 0;

            laserLine.SetPosition(0, energyOrigin.position);
            Vector3 rayOrigin = robotCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, robotCamera.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                print(hit.transform.gameObject);
                switch (hit.transform.gameObject.tag)
                {
                    case "Robot":
                        Transfer(hit.collider.gameObject);
                        break;

                    case "ChargingPort":
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                        break;
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (robotCamera.transform.forward * gunRange));
            }
            StartCoroutine(ShootEnergy());
        }
    }

    IEnumerator ShootEnergy()//發射能源光束
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }


    void Transfer(GameObject target)//附身
    {
        target.GetComponent<Controllable>().Transfer();
        ControllingRobot = target;
    }
}
