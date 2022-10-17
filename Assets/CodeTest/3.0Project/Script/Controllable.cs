using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    [Header("請輸入機器人名稱")]
    public string robotName;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Transfer()//被玩家操作
    {
        switch (robotName)
        {
            case "管理機器人":
                this.gameObject.GetComponent<ManagerRobot>().Transferred(true);
                break;
            case "照明機器人":
                break;
            case "修復機器人":
                break;
        }
    }
}
