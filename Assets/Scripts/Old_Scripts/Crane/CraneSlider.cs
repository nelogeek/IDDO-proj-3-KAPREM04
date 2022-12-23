using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MGS.Machinery;

public class CraneSlider : MonoBehaviour, IAnimation
{
    SliderArm sliderArm;
    MechanismDriver mechanismDriver;

    public GameObject OriginHookPosition;
    public float Speed = 1f;

    void Start()
    {
        // Get Link
        sliderArm = this.GetComponent<SliderArm>();
        mechanismDriver = this.GetComponent<MechanismDriver>();

        // Set some settings
        mechanismDriver.velocity = Speed;
        mechanismDriver.direction = MechanismDriver.Direction.Negative;
    }

    void IAnimation.Calculate(GameObject target)
    {
        //if (mechanismDriver.direction == MechanismDriver.Direction.Negative)
        //{
        //    mechanismDriver.direction = MechanismDriver.Direction.Positive;
        //}

        float angle = 0;

        // Calculate distnace between target and hook
        Vector3 targetPos = new Vector3(target.transform.position.x, OriginHookPosition.transform.position.y, target.transform.position.z);
        float dist = Vector3.Distance(OriginHookPosition.transform.position, targetPos);

        // Get current angle of arm
        if (Crane.instance.Arm.GetComponent<MechanismDriver>().direction == MechanismDriver.Direction.Positive)
            angle = Crane.instance.Arm.GetComponent<LimitCrank>().range.max + Crane.instance.Arm.GetComponent<LimitCrank>().StartAngles.z;
        else if (Crane.instance.Arm.GetComponent<MechanismDriver>().direction == MechanismDriver.Direction.Negative)
            angle = Crane.instance.Arm.GetComponent<LimitCrank>().range.min + Crane.instance.Arm.GetComponent<LimitCrank>().StartAngles.z;

        // Calculate distance
        dist = dist / Mathf.Cos(angle * Mathf.Deg2Rad);
        dist /= Crane.instance.transform.localScale.x;

        // Get Full distance
        //float fullDistance = 0;
        //for (int i = 0; i < 3; i++)
        //{
        //    fullDistance += sliderArm.sliders[i].Displacement;
        //}

        // Set direction
        //if (fullDistance <= dist)
        //    mechanismDriver.direction = MechanismDriver.Direction.Positive;
        //else
        //    mechanismDriver.direction = MechanismDriver.Direction.Negative;

        // Set each slider distance
        for (int i = 0; i < 3; i++)
        {
            //print("i = " + i + " distanceSlider = " + dist);

            //if (mechanismDriver.direction == MechanismDriver.Direction.Positive)
                sliderArm.sliders[i].stroke.max = dist >= 5 ? 5 : dist;
            //else if (mechanismDriver.direction == MechanismDriver.Direction.Negative)
            //    sliderArm.sliders[i].stroke.min = dist >= 5 ? 5 : dist;

            if (dist >= 5)
                dist -= 5;
            else
                dist = 0;
        }
    }

    void IAnimation.Play()
    {
        //print("Play anim Slider");
        mechanismDriver.DriveMechanism();
    }

    bool IAnimation.Trigger()
    {
        bool isReached = false;

        if (mechanismDriver.direction == MechanismDriver.Direction.Positive)
        {
            if (sliderArm.sliders[0].Displacement == sliderArm.sliders[0].stroke.max 
                && sliderArm.sliders[1].Displacement == sliderArm.sliders[1].stroke.max
                && sliderArm.sliders[2].Displacement == sliderArm.sliders[2].stroke.max)
            {
                mechanismDriver.direction = MechanismDriver.Direction.Negative;
                isReached = true;
            }
        }
        else if (mechanismDriver.direction == MechanismDriver.Direction.Negative)
        {
            if (sliderArm.sliders[0].Displacement == sliderArm.sliders[0].stroke.min
                && sliderArm.sliders[1].Displacement == sliderArm.sliders[1].stroke.min
                && sliderArm.sliders[2].Displacement == sliderArm.sliders[2].stroke.min)
            {
                mechanismDriver.direction = MechanismDriver.Direction.Positive;
                isReached = true;
            }
                    
        }

        return isReached;
    }
}
