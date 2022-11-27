using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorseQuestInput : MonoBehaviour
{
    public Text resultText;//resultText名稱自訂
    public Button EnterButton;
    public int correctAnswer=4591;
    public void OnNumberClick(int number)//OnNumberClick名稱自訂
    {
            resultText.text += number.ToString();//ToString將數字轉為字串
    }
    public void OnClearClick()//AC
    {
        resultText.text = "";//將字串
    }
    public void OnEnterClick()//Enter
    {
        if (resultText.text == "4591")
        {
            resultText.text = "pass";//門會打開
            GameObject.Find("LightingRobot").GetComponent<LightingRobot>().PowerUp();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
