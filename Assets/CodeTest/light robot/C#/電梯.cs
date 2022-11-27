using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 電梯 : MonoBehaviour
{
    public float move;//移動範圍
    public float speed;//移動速度
    private void Update()
    {
        transform.position = new Vector3(0, Mathf.Sin(Time.time * speed) * move, 0);
    }

}