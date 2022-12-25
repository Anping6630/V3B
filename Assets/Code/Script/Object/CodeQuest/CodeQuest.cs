using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodeQuest : MonoBehaviour
{
    [Header("顯示欄位")]
    public TextMeshProUGUI resultText;
    [Header("謎題名稱")]
    public string questName;
    [Header("正確密碼")]
    public string answer;
    [Header("關聯物件")]
    public GameObject relatedObject;

    GameObject FinalBlock;

    public void OnNumberClick(int number)//按下數字鍵
    {
        if(resultText.text.Length < 4)
        {
            resultText.text += number.ToString();
        }
    }

    public void OnClearClick()//按下Delete鍵
    {
        resultText.text = resultText.text.Remove(resultText.text.Length - 1,1);
    }

    public void OnEnterClick()//按下Enter鍵
    {
        if (resultText.text == answer)//正解
        {   
            resultText.text = "PASS";

            switch (questName)
            {
                case "Morse":
                    relatedObject.GetComponent<LightingRobot>().PowerUp();
                    break;
                case "BigScreen":
                    relatedObject.GetComponent<Animator>().SetBool("isEnable", true);
                    break;
                case "Final":
                    resultText.text = "Error";
                    relatedObject.GetComponent<Animator>().SetBool("isEnable", false);
                    FinalBlock.SetActive(true);
                    GameObject.Find("FinalRedLight").GetComponent<Animator>().SetBool("isEnable", true);
                    GameObject.Find("Black").GetComponent<Animator>().SetBool("isEnable", true);
                    break;
            }
        }
        else
        {
            resultText.text = "";

            switch (questName)
            {
                case "Morse"://物件改名時會出錯
                    GameObject.Find("MorseLight").GetComponent<Animator>().SetTrigger("error");
                    break;
            }
        }
    }

    void Awake()
    {
        switch (questName)
        {
            case "Final":
                relatedObject.GetComponent<Animator>().SetBool("isEnable", true);
                FinalBlock = GameObject.Find("FinalBlock");
                FinalBlock.SetActive(false);
                break;
        }
    }
}
