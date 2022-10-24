using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    /*
    public float speed = 3f;
    float horizontalinput;
    float forwardinput;
    */
    public float playerspeed = 10f;
    public float playerrotationspeed = 100f;

    void Start()
    {
        
    }

    void Update()
    {
        /*
        horizontalinput = Input.GetAxis("Horizontal");
        forwardinput= Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardinput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalinput);
        */

        float transportation = Input.GetAxis("Vertical") * playerspeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * playerrotationspeed * Time.deltaTime;

        transform.Translate(0, 0, transportation);
        transform.Rotate(0, rotation, 0);

    }
}
