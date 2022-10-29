using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigdoor2manager : MonoBehaviour
{
    Animator bigdoor2;
    void Start()
    {
        bigdoor2 = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            bigdoor2.SetBool("isEnable", true);

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            bigdoor2.SetBool("isEnable", false);

        }
    }
}
