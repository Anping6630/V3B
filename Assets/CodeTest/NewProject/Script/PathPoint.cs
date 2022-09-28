using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    [Header("連接著的點")]
    public GameObject[] connectingPoints;

    void Start()
    {

    }

    void Update()
    {
        for (int i = 0; i < connectingPoints.Length; i++)//顯示線
        {
            Debug.DrawLine(transform.position, connectingPoints[i].transform.position, Color.white);
        }
    }
}
