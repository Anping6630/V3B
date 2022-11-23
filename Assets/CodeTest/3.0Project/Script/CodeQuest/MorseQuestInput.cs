using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorseQuestInput : MonoBehaviour
{
    [Header("顯示欄位")]
    public Text resultText;
    [Header("正確密碼")]
    public int answer;

    public void OnNumberClick(int number)//按下數字鍵
    {
            resultText.text += number.ToString();
    }

    public void OnClearClick()//按下清除鍵
    {
        resultText.text = "";
    }

    public void OnEnterClick()//Enter
    {
        if (resultText.text == answer)
        {
            resultText.text = "pass";//門會打開
            GameObject.Find("LightingRobot").GetComponent<LightingRobot>().PowerUp();
        }
    }

    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
