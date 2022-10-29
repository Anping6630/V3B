using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCard : MonoBehaviour
{
    public Animator lockedDoor;//反鎖大門
    GameObject[] cameras;//所有攝影機

    void Awake()
    {
        cameras = GameObject.FindGameObjectsWithTag("Camera");
    }

    void Update()
    {
        for(int i =0;i< cameras.Length; i++)
        {
            if (Vector3.Distance(cameras[i].transform.position, transform.position) < 5)
            {
                lockedDoor.SetBool("isEnable", true);
            }
        }
    }
}
