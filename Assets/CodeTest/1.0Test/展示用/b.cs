using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b : MonoBehaviour
{
    public GameObject aa;
    public GameObject[] bb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            aa.SetActive(false);
            for(int i = 0; i < bb.Length; i++)
            {
                bb[i].SetActive(true);
            }
        }
    }
}
