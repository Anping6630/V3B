using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doormanager : MonoBehaviour
{
    Animator dooranim;
    void Start()
    {
        dooranim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            dooranim.SetBool("door_openorclose", true);

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            dooranim.SetBool("door_openorclose", false);

        }

    }
}
