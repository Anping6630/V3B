using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCard : MonoBehaviour
{
    [Header("此物件控制的物件")]
    public Animator relatedObject;
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
                relatedObject.SetBool("isEnable", true);
            }
        }
    }
}
