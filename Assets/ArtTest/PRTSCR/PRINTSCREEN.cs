using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class PRINTSCREEN : MonoBehaviour
{
    public string dirpath;
    string correctpath;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (File.Exists(@"C:\thesecurityplan\"))
        {
            dirpath = @"C:\thesecurityplan\";
        }
        else
        {
            Directory.CreateDirectory(@"C:\thesecurityplan");
            dirpath = @"C:\thesecurityplan\";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            correctpath=dirpath+DateTime.Now.Month+DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".jpg";
            ScreenCapture.CaptureScreenshot(correctpath, 0);
        }
    }
}
