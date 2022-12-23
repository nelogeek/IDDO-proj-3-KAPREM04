using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationWindow : MonoBehaviour
{
    public Text textwindowConfirmation;
    public bool goMainMenu; // check which button were press

    void Update()
    {
        if (goMainMenu)
        {
            textwindowConfirmation.text = "Вы уверены что хотите выйти в главное меню? \n Результаты не сохранятся.";
        }
        else
        {
            textwindowConfirmation.text = "Вы уверены что хотите выйти? \n Результаты не сохранятся.";
        }
    }


    public void btnAgree()
    {
        if (goMainMenu)
            Application.LoadLevel(0);
        else
            Application.Quit();
    }

    public void DisAgree()
    {
        this.GetComponent<PausedMenu>().MainMenuWindow.SetActive(true);
        this.GetComponent<PausedMenu>().ConfirmationWindow.SetActive(false);
    }
}
