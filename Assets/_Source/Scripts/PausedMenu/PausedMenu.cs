using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenu : MonoBehaviour
{
    public GameObject PausedManuUI;
    public GameObject MainMenuWindow;
    public GameObject ConfirmationWindow;
    public GameObject ManualWindow;
    
    // Update is called once per frame
    void Update()
    {
        if (InputManager.isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }       
    }

    void Resume()
    {
        Time.timeScale = 1f;
        PausedManuUI.SetActive(false);
    }

    void Pause()
    {
        Time.timeScale = 0;
        PausedManuUI.SetActive(true);
    }

    public void btnContinue()
    {
        InputManager.isPaused = false;
        InputManager.isStopedController = InputManager.lastStayController;
    }

    public void btnMainMenu()
    {
        this.GetComponent<ConfirmationWindow>().goMainMenu = true;

        MainMenuWindow.SetActive(false);
        ConfirmationWindow.SetActive(true);
    }

    public void btnManual()
    {
        MainMenuWindow.SetActive(false);
        ManualWindow.SetActive(true);
    }

    public void btnExit()
    {
        this.GetComponent<ConfirmationWindow>().goMainMenu = false;

        MainMenuWindow.SetActive(false);
        ConfirmationWindow.SetActive(true);
    }
}
