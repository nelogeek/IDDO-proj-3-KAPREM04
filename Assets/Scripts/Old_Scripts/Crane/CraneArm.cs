using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MGS.Machinery;

public class CraneArm : MonoBehaviour, IAnimation
{
    LimitCrank limitCrank;
    MechanismDriver mechanismDriver;
    Vector3 hookPos;
    float distArmHook;

    public float Speed;
    void Start()
    {
        // Get link
        mechanismDriver = this.GetComponent<MechanismDriver>();
        limitCrank = this.GetComponent<LimitCrank>();
        hookPos = Crane.instance.Hook.transform.position;

        // Calculate distnace for angle
        Vector3 posArm = new Vector3(this.transform.position.x, hookPos.y, hookPos.z);
        distArmHook = Vector3.Distance(hookPos, posArm);
        
        // Set some settings
        mechanismDriver.velocity = Speed;
        
    }

    void IAnimation.Calculate(GameObject target)
    {
        // Calculace distance
        //Vector3 posArm = new Vector3(this.transform.position.x, hook.position.y, hook.position.z);
        //Vector3 posTarget = new Vector3(hookPos.x, target.GetComponent<Object>().TopPoint.y, hookPos.z);
        Vector3 posTarget;

        if (target.transform.childCount != 0)
            posTarget = new Vector3(hookPos.x, target.transform.GetChild(0).position.y, hookPos.z);
        else
            posTarget = new Vector3(hookPos.x, target.transform.position.y, hookPos.z);

        //float distArmHook = Vector3.Distance(hook.position, posArm); // calculate distance between hook and arm
        float distHookTarget = Vector3.Distance(hookPos, posTarget) + 1f; // calculate distance between hook and target

        // Calculate angle
        float angle = Mathf.Atan(distHookTarget / distArmHook) * Mathf.Rad2Deg; 

        // Get current limits
        float maxAngle = limitCrank.range.max;
        float minAngle = limitCrank.range.min;

        // Set angle and direction
        if (angle < maxAngle)
        {
            if (angle > minAngle && minAngle != 0)
            {
                limitCrank.range.max = angle;
                mechanismDriver.direction = MechanismDriver.Direction.Positive;
            }
            else
            {
                limitCrank.range.min = angle;
                mechanismDriver.direction = MechanismDriver.Direction.Negative;
            }
        }
        else
        {
            limitCrank.range.max = angle;
            mechanismDriver.direction = MechanismDriver.Direction.Positive;
        }

        //print("SliderAngle = " + angle);
    }

    void IAnimation.Play()
    {
        mechanismDriver.DriveMechanism();
        //print("Play anim Arm");
    }

    bool IAnimation.Trigger()
    {
        // Rotate Cabine 
        float calculatetedAngle = 0;

        if (mechanismDriver.direction == MechanismDriver.Direction.Positive)
        {
            calculatetedAngle = limitCrank.range.max;
        }
        else if (mechanismDriver.direction == MechanismDriver.Direction.Negative)
        {
            calculatetedAngle = limitCrank.range.min;
        }

        if (limitCrank.Angle != calculatetedAngle)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
