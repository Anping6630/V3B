using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest1manager : MonoBehaviour
{
    Animator chestanim;
    void Start()
    {
        chestanim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            chestanim.SetBool("chestopenorclose", true);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            chestanim.SetBool("chestopenorclose", false);
        }
    }
}
