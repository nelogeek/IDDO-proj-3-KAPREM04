using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSetup : MonoBehaviour
{
    public Animator animtatorSetup;

    private void Start()
    {
        GameObject gameObject = Setup.instance.gameObject;
        animtatorSetup = Setup.instance.AnimatorSetup;
    }

    private void OnMouseDown()
    {
        PlayAnimation();
    }

    void PlayAnimation()
    {
        bool isOpen = animtatorSetup.GetBool("isOpen");

        animtatorSetup.SetBool("isOpen", !isOpen);
    }
}
