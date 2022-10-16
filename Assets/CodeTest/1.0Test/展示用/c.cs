using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c : MonoBehaviour
{
    public GameObject p;
    public GameObject pp;
    public GameObject p2;
    public GameObject p3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            p.GetComponent<PathPoint>().connectingPoints[1] = pp;
            p2.GetComponent<PathPoint>().connectingPoints[0] = null;
            p3.GetComponent<PathPoint>().connectingPoints[0] = p;
        }
    }
}
