using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 終點大門密碼 : MonoBehaviour
{
    public Text resultText;//名稱自訂
    public Button EnterButton;
    public int correctAnswer = 2649;
    // Start is called before the first frame update
    public void OnNumberClick(int number)//OnNumberClick名稱自訂
    {
        resultText.text += number.ToString();//ToString將數字轉為字串
    }
    public void OnClearClick()//AC
    {
        resultText.text = "";//字串
    }
    public void OnEnterClick()//Enter
    {
        if(resultText.text=="2649")
        {
            resultText.text = "pass";//門打開
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
