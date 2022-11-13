using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingPort : MonoBehaviour
{
    [Header("能源孔控制的物件")]
    public Animator relatedObject;
    [Header("是否能被注入能源")]
    public bool isInfuseAble;
    [Header("未注入能源材質")]
    public Material notInfused_M;
    [Header("已注入能源材質")]
    public Material Infused_M;
    [Header("是否已被注入能源")]
    public bool isCharge;

    void Awake()
    {
        isCharge = false;
    }

    public void Energy(bool isInfuse)//被注入或回收能源
    {
        if (isInfuseAble && isCharge != isInfuse)//觸發動畫
        {
            relatedObject.SetBool("isEnable", isInfuse);
            isCharge = isInfuse;
        }

        if (isCharge)//改變材質
        {
            GetComponent<MeshRenderer>().material = Infused_M;
        }
        else
        {
            GetComponent<MeshRenderer>().material = notInfused_M;
        }
    }
}
