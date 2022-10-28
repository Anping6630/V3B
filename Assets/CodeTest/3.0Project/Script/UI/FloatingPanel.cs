using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPanel : MonoBehaviour
{
    public Camera energyRobotCamera;//能源機器人相機

    void Awake()
    {
        energyRobotCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        //始終面對玩家//
        transform.LookAt(new Vector3(-energyRobotCamera.transform.position.x + transform.position.x * 2, transform.position.y, -energyRobotCamera.transform.position.z + transform.position.z * 2));
    }

    public void Ahoy()
    {
        print("Ahoy♡");
    }
}
