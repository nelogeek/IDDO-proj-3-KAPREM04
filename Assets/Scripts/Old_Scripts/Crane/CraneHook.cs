using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MGS.Machinery;

public class CraneHook : MonoBehaviour, IAnimation
{
    MechanismDriver mechanismDriver;
    Slider slider;

    GameObject target;
    GameObject takenObject;
    bool isTakenObject;

    public float Speed = 1f;

    void Start()
    {
        // Get link
        mechanismDriver = this.GetComponent<MechanismDriver>();
        slider = this.GetComponent<Slider>();

        // Set some settings
        mechanismDriver.velocity = Speed;
    }

    void Update()
    {
        if (this.transform.childCount != 2)
            isTakenObject = false;
        else
            isTakenObject = true;
    }

    void IAnimation.Calculate(GameObject target)
    {
        this.target = target;
        float dist = 0;

        if (!isTakenObject)
        {
            dist = Vector3.Distance(this.transform.position, target.transform.GetChild(0).transform.position);// target.GetComponent<Object>().TopPoint);
            //dist -= 0.43f;
            //dist = dist - 1f < 0 ? 0 : dist - 1f;
        }
        else
        {
            if(target.GetComponent<Object>() != null)
                dist = Vector3.Distance(takenObject.GetComponent<Object>().DownPoint, target.GetComponent<Object>().TopPoint);
            else
            {
                //dist = takenObject.GetComponent<Object>().StartTransform.position.y -  takenObject.transform.position.y;
                dist = takenObject.GetComponent<Object>().StartPosition.y - takenObject.transform.position.y;
                dist = Mathf.Abs(dist);
            }
        }

        dist /= Crane.instance.transform.localScale.x;

        slider.stroke.max = dist;
        mechanismDriver.direction = MechanismDriver.Direction.Positive;
    }

    void IAnimation.Play()
    {
        print("Play anim Hook");

        // Trigger if we release our hook
        if (slider.State == TelescopicState.Maximum) 
        {
            if (!isTakenObject)
            {
                target.GetComponent<Object>().ShowConnectionLine();

                // Take object
                takenObject = target;
                takenObject.transform.parent = this.transform;
            }
            else
            {

                takenObject.GetComponent<Object>().HideConnectionLine();
                
                // Release object
                takenObject.GetComponent<Object>().SetPositionAfterRelease();
            }

            mechanismDriver.direction = MechanismDriver.Direction.Negative;
        }

        // Drive mechanism
        mechanismDriver.DriveMechanism();
    }

    bool IAnimation.Trigger()
    {
        bool isReached = false;

        if (slider.State == TelescopicState.Minimum && mechanismDriver.direction == MechanismDriver.Direction.Negative)
        { 
            isReached = true;
        }

        return isReached;
    }
}
