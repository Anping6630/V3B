using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [Header("是否為節點地板")]
    public bool isNode;
    void Start()
    {
        
    }


    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        print(other.transform.gameObject.name);
        switch(other.transform.gameObject.name)
        {
            case "EnergyRobot":
                print("能源");
                break;
            case "LightingRobot":
                print("照明");
                break;
            case "RepairRobot":
                print("修復");
                break;
        }
    }
}
