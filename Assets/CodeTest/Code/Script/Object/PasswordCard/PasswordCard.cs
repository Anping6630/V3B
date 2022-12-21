using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCard : MonoBehaviour
{
    [Header("此物件控制的物件")]
    public Animator relatedObject;
    [Header("模型")]
    public GameObject mesh;
    [Header("提示UI")]
    public Text UI;
    [Header("開門音效")]
    public AudioClip doorOpen_S;
    float uiTimer;

    void Awake()
    {
 
    }

    void Update()
    {
        uiTimer += Time.deltaTime;
        if (uiTimer > 1)
        {
            UI.text = "";
        }
    }

    public void GetPassword()
    {
        relatedObject.SetBool("isEnable", true);
        mesh.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(doorOpen_S);

        UI.text = "取得密碼卡，房間門已開啟";
        uiTimer = 0;
    }
}
