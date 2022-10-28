using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingPort : MonoBehaviour
{
    [Header("能源孔控制的物件")]
    public Animator relatedObject;

    public void Charge()//被注入能源
    {
        relatedObject.SetBool("isEnable", true);
    }
}
