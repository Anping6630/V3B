using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackZone : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Robot")
        {
             RecordPointManager.BackToRecord(other.gameObject);
        }
    }
}
