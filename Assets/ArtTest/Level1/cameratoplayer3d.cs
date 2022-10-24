using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameratoplayer3d : MonoBehaviour
{
    public Transform targetobject;

    public Vector3 cameraoffset;

    public float smoothfactor = 0.5f;

    public bool lookattarget = false;

    void Start()
    {
        cameraoffset = transform.position - targetobject.transform.position;
    }

    void LateUpdate()
    {
        Vector3 newposition = targetobject.transform.position + cameraoffset;
        transform.position = Vector3.Slerp(transform.position, newposition, smoothfactor);

        if (lookattarget)
        {
            transform.LookAt(targetobject);
        }
    }
    
}
