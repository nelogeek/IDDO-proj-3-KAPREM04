using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperWindow : MonoBehaviour
{
    public void btnClose()
    {
        this.GetComponent<Menu>().DeveloperWindow.SetActive(false);
    }
}
