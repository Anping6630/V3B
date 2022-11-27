using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//載入函式庫

public class Gamebutton : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(1);//開始遊戲
    }

    public void QuitGame()
    {
        Application.Quit();//結束遊戲
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
