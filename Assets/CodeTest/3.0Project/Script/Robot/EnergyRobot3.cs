using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnergyRobot3 : MonoBehaviour
{
    //附身機制改按鍵時記得檢查兩個機器人

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

    //玩家是否操作其他機器人中(測試中功能)//
    public GameObject ControllingRobot = null;

    [Header("能源發射點")]
    public Transform energyOrigin;
    [Header("能源槍模型")]
    public GameObject energyGun;
    public float gunRange = 50f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;

    //攝影機//
    float xRotation = 0f;
    //灌注能源//
    LineRenderer laserLine;
    float infuseTimer;

    void Awake()
    {
        laserLine = energyGun.GetComponent<LineRenderer>();
        laserLine.enabled = false;
    }

    void Update()
    {
        if (ControllingRobot == null)//沒有操作其他機器人時
        {
            FirstPersonLook();
            Movement();
            Aim();
            InfuseEnergy();
        }
        else
        {
            if (Input.GetKeyDown("q"))//按Q取消附身
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
        move.y -= 20f * Time.deltaTime;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void Aim()//臨時用開鏡瞄準
    {
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
    }

    void InfuseEnergy()//灌注能源
    {
        infuseTimer += Time.deltaTime;
        laserLine.SetPosition(0, energyOrigin.position);

        if (Input.GetMouseButtonDown(0) && infuseTimer > fireRate)
        {
            infuseTimer = 0;

            Vector3 rayOrigin = robotCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, robotCamera.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);

                switch (hit.transform.gameObject.tag)
                {
                    case "Robot"://命中機器人
                        Transfer(hit.collider.gameObject);
                        aimPoint.SetActive(false);
                        break;

                    case "ChargingPort"://命中能源孔
                        hit.collider.gameObject.GetComponent<ChargingPort>().Charge();
                        break;
                }
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("UI"))//按下按鈕
                {
                    ExecuteEvents.Execute<IPointerClickHandler>(hit.collider.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
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
