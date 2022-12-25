using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//���J�禡�w

public class Gamebutton : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(1);//�}�l�C��
    }

    public void QuitGame()
    {
        Application.Quit();//����C��
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ToCredit()
    {
        SceneManager.LoadScene(2);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
