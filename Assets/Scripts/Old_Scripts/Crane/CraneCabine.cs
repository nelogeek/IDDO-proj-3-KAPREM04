using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MGS.Machinery;

public class CraneCabine : MonoBehaviour, IAnimation
{
    LimitCrank limitCrank;
    MechanismDriver mechanismDriver;

    // public 
    public float Speed;
    void Start()
    {
        // Get link
        mechanismDriver = this.GetComponent<MechanismDriver>();
        limitCrank = this.GetComponent<LimitCrank>();

        // Set some settings
        mechanismDriver.velocity = Speed; 
    }

    void IAnimation.Calculate(GameObject target)
    {
        //Target = target;
        // Calculate angle
        Vector3 targetDir = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z) - this.transform.position;
        float angleCabine = Vector3.Angle(targetDir, this.transform.up);
        //Get dot for calculate direction
        //Vector3 forward = this.transform.TransformDirection(this.transform.up);
        //Vector3 forward = this.transform.up;
        //Vector3 toOther = target.transform.position - this.transform.position;
        Vector3 forward = this.transform.TransformDirection(Vector3.right);
        Vector3 toOther = targetDir;// target.transform.position - this.transform.position;

        // DOT !?!?!?!??!?!
        float dot = Vector3.Dot(forward.normalized, toOther.normalized);

        // Get current limits
        float maxAngle = limitCrank.range.max;
        float minAngle = limitCrank.range.min;

        //Set angle and direction
        if (dot < 0)
        {
            limitCrank.range.max = limitCrank.Angle + angleCabine; 
            mechanismDriver.direction = MechanismDriver.Direction.Positive;
        }
        else if (dot > 0)
        {
            limitCrank.range.min = angleCabine - limitCrank.Angle;
            limitCrank.range.min *= -1f;
            mechanismDriver.direction = MechanismDriver.Direction.Negative;
        }
        print("angle Cabine = " + angleCabine);

        print("dot Cabine = " + dot);
        //-1.670563
    }

    void IAnimation.Play()
    {
        mechanismDriver.DriveMechanism();
        print("Play anim Cabine");
    }
    //public GameObject Target;

    private void OnDrawGizmos()
    {
        //Vector3 forward = Vector3.forward;
        ////Vector3 toOther = Target.transform.position - this.transform.position;
        //Vector3 toOther = new Vector3(Target.transform.position.x, this.transform.position.y, Target.transform.position.z) - this.transform.position;

        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(new Ray(this.transform.position, forward));

        //Gizmos.color = Color.blue;
        //Gizmos.DrawRay(new Ray(this.transform.position, toOther));

        //float dot = Vector3.Dot(forward.normalized, toOther.normalized);

        ////print("dot = " + dot);
        //var targetDir = new Vector3(Target.transform.position.x, this.transform.position.y, Target.transform.position.z) - this.transform.position;
        //print( Vector3.Angle(targetDir, Vector3.up));
    }

    bool IAnimation.Trigger()
    {
        // Rotate Cabine 
        float calculatetedAngle = 0;

        if(mechanismDriver.direction == MechanismDriver.Direction.Positive)
        {
            calculatetedAngle = limitCrank.range.max;

        }else if(mechanismDriver.direction == MechanismDriver.Direction.Negative)
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
