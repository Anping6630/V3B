using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class password : MonoBehaviour
{
    public InputField playerAnswerUI;
    public int playerAnswer;
    public int correctAnswer;
    public Button EnterButton;
    public Text hintMessage;

    // Start is called before the first frame update
    void Start()
    {
        updatehintMessage("Enter Password");
    }
    void updatehintMessage(string message)
    {
        hintMessage.text = message;
    }
    void NewQuestion()
    {
        correctAnswer = 4591;
    }
    public void CompareNumbers()
    {
        playerAnswer = int.Parse(playerAnswerUI.text);
        if(playerAnswer==correctAnswer)
        {
            updatehintMessage("PASS!");
        }
        if (playerAnswer != correctAnswer)
        {
            updatehintMessage("Incorrect");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
