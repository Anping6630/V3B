using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//���J�禡�w

public class GameManager : MonoBehaviour
{
    public GameObject escMenu;

    bool isMenuOpen;

    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Awake()
    {
        //鎖滑鼠//
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isMenuOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            OpenCloseEscMenu();
        }
    }

    public void OpenCloseEscMenu()
    {
        isMenuOpen = !isMenuOpen;
        escMenu.SetActive(isMenuOpen);
        if (isMenuOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            //鎖滑鼠//
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
