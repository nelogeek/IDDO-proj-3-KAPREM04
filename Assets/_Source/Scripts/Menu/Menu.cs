using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject MainMenuWindow;
    public GameObject DeveloperWindow;
    public GameObject ManualWindow;
    public GameObject LoadWindow;

    public void btnDeveloper()
    {
        DeveloperWindow.SetActive(!DeveloperWindow.activeSelf);
    }

    public void btnStart()
    {
        // Open Load Here
        LoadWindow.SetActive(true);
        MainMenuWindow.SetActive(false);
        this.GetComponent<LevelLoader>().LoadLevel(1);
    }

    public void btnExit()
    {
        Application.Quit();
    }

    public void btnManual()
    {
        MainMenuWindow.SetActive(false);
        DeveloperWindow.SetActive(false);
        ManualWindow.SetActive(true);
    }

}

