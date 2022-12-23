using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isStopedController = false;
    public static bool lastStayController;// memorize last stay of controller

    public KeyCode KeyContinue = KeyCode.Escape;
    public KeyCode KeyManual = KeyCode.F1;
    public KeyCode KeyExit = KeyCode.F10;
    public KeyCode KeyStopController = KeyCode.Mouse1;

    // Update is called once per frame
    void Update()
    {
        // Hide or Show Menu
        if (Input.GetKeyDown(KeyContinue))
        {
            if (isPaused)
            {
                isPaused = false;
                isStopedController = lastStayController;
            }
            else
            {
                lastStayController = isStopedController;
                isPaused = true;
                isStopedController = true;
            }
        }

        // Stop Controller
        if (!isPaused && !Actions.person3Key && !Actions.personOilKey)
        {
            if (Input.GetKeyDown(KeyStopController))
            {
                if (isStopedController)
                {
                    isStopedController = false;
                }
                else
                {
                    isStopedController = true;
                }
            }
        }

        // Open Manual
        if (Input.GetKeyDown(KeyManual))
        {

        }
    }
}
