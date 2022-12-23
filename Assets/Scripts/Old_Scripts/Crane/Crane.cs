using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour
{
    public static Crane instance;

    public float Speed;
    [HideInInspector] public bool isAnimation;

    // Link
    public GameObject Target;
    public GameObject Hook;
    public GameObject Slider;
    public GameObject Cabine;
    public GameObject Arm;

    IAnimation[] animations; // animation list 
    int indexPlayAnimation;

    [Header("Animation")]
    public GameObject FirstPointMove; // object for carry out
    public GameObject SecondPointMove; // end of carry out
  
    void Awake()
    {
        instance = this;

        animations = new IAnimation[5];
        animations[0] = Slider.GetComponent<IAnimation>();// 0 - move slider to minimum;
        animations[1] = Cabine.GetComponent<IAnimation>();// 1 - rotate cabine;
        animations[2] = Arm.GetComponent<IAnimation>();   // 2 - rotate arm;
        animations[3] = Slider.GetComponent<IAnimation>();// 3 - move slider;
        animations[4] = Hook.GetComponent<IAnimation>();  // 4 - down hook and take target; 5 - up hook with target; end

        isAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAnimation)
           PlayAnimation();
    }

    public void StartAnimation(GameObject fObject, GameObject sObject)
    {
        FirstPointMove = fObject;
        SecondPointMove = sObject;

        Target = FirstPointMove;

        isAnimation = true;
        isChangedTarget = false;
        indexPlayAnimation = 0;
    }

    bool isChangedTarget = false;
    void OnChangeTarget()
    {
        // Reload all 
        Target = SecondPointMove;
        indexPlayAnimation = -1;
        isChangedTarget = true;
        isAnimation = true;

    }

    void PlayAnimation()
    {
        // Check trigger if isn't enter play anim else play next
        if (!animations[indexPlayAnimation].Trigger())
        {
            animations[indexPlayAnimation].Play();
        }
        else
        {
            if(indexPlayAnimation == animations.Length - 1)
            {
                if(!isChangedTarget)
                    OnChangeTarget();
                else
                {
                    isAnimation = false;
                    return;
                }
            }

            animations[indexPlayAnimation + 1].Calculate(Target);
            indexPlayAnimation++;
        }
    }
}
