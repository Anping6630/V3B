using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseLightControll : MonoBehaviour
{
    public GameObject morseLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MorseStart()
    {
        morseLight.SetActive(true);
        morseLight.GetComponent<Animator>().SetBool("enter", true);
    }
}
