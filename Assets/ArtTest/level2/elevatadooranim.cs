using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatadooranim : MonoBehaviour
{
    Animator elevataanim;

    void Start()
    {
        elevataanim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            elevataanim.SetBool("isEnable", true);

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            elevataanim.SetBool("isEnable", false);

        }
    }
}

