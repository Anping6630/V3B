using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPointManager : MonoBehaviour
{
    private static Vector3 playerRecordPos;

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            RecordPointManager.playerRecordPos = new Vector3(-0.8f, 0.5f, -20f);
        }
        if (Input.GetKeyDown("2"))
        {
            RecordPointManager.playerRecordPos = new Vector3(29.9f, 0.5f, 7.63f);
        }
        if (Input.GetKeyDown("3"))
        {
            RecordPointManager.playerRecordPos = new Vector3(62.82f, 0.5f, 3.51f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Robot")
        {
            playerRecordPos = transform.position;
        }
    }

    public static void BackToRecord(GameObject target)
    {
        target.gameObject.transform.position = playerRecordPos;
    }
}
