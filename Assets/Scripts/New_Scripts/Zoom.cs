using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float wheel_speed = 10f; // скорость зума

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw >= 0.1 && GetComponent<Camera>().fieldOfView > 10)
        {
            Debug.Log("mw1 = " + mw);
            GetComponent<Camera>().fieldOfView -= mw * wheel_speed;

            if (GetComponent<Camera>().fieldOfView < 10)
            {
                GetComponent<Camera>().fieldOfView = 10;
            }
        }
        if (mw <= -0.1 && GetComponent<Camera>().fieldOfView < 60)
        {
            Debug.Log("mw2 = " + mw);
            GetComponent<Camera>().fieldOfView += mw * (-1) * wheel_speed;

            if (GetComponent<Camera>().fieldOfView > 60)
            {
                GetComponent<Camera>().fieldOfView = 60;
            }
        }
    }
}
