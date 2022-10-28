using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPanel : MonoBehaviour
{
    public Transform camera;//主相機

    void Start()
    {
        
    }


    void Update()
    {
        transform.LookAt(new Vector3(-camera.position.x + transform.position.x * 2, transform.position.y, -camera.position.z + transform.position.z * 2));//始終面對玩家
    }

    public void Ahoy()
    {
        print("Ahoy♡");
    }
}
