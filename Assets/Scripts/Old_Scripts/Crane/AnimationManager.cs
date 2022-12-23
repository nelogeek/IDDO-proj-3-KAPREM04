using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;

    public float Speed;

    // Link
    public GameObject Targer;
    public GameObject Hook;
    public GameObject Slider;
    public GameObject Cabine;
    public GameObject Arm;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
