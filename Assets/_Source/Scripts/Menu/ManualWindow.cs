using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualWindow : MonoBehaviour
{
    public RectTransform ContentManual;
    public RectTransform ContentInstruction;
    public Image BtnContentManual;
    public Image BtnContentInstruction;
    public ScrollRect scrollRect;
    public Sprite blueTexture;
    public Sprite whiteTexture;
    public Text labelWindow;

    public void btnClose()
    {
        this.GetComponent<Menu>().MainMenuWindow.SetActive(true);
        this.GetComponent<Menu>().ManualWindow.SetActive(false);
    }

    public void btnContentManual()
    {
        // Change Texture of buttons
        BtnContentInstruction.sprite = blueTexture;
        BtnContentManual.sprite = whiteTexture;

        // Change Color Text on Buttons
        BtnContentInstruction.transform.GetComponentInChildren<Text>().color = Color.white;
        BtnContentManual.transform.GetComponentInChildren<Text>().color = Color.black;

        // Change Content for scrolling
        scrollRect.content = ContentManual;

        // Hide or Show what is need
        ContentManual.gameObject.SetActive(true);
        ContentInstruction.gameObject.SetActive(false);

        //Set WindowName
        labelWindow.text = BtnContentManual.transform.GetChild(0).GetComponent<Text>().text;
    }

    public void btnContentInstruction()
    {
        BtnContentInstruction.sprite = whiteTexture;
        BtnContentManual.sprite = blueTexture;

        BtnContentInstruction.transform.GetComponentInChildren<Text>().color = Color.black;
        BtnContentManual.transform.GetComponentInChildren<Text>().color = Color.white;

        scrollRect.content = ContentInstruction;

        ContentManual.gameObject.SetActive(false);
        ContentInstruction.gameObject.SetActive(true);
        labelWindow.text = BtnContentInstruction.transform.GetChild(0).GetComponent<Text>().text;
    }
}
