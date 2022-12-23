using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MGS.Machinery;

public class Animation : MonoBehaviour
{
    // delegates
    public delegate void OnVariableChangeDelegate();
    public event OnVariableChangeDelegate OnVariableChange;

    // References
    public GameObject Target;
    public MGS.Machinery.MechanismDriver Cabine;
    public MGS.Machinery.MechanismDriver SliderRotatin;
    public MGS.Machinery.MechanismDriver Slider;
    public MGS.Machinery.MechanismDriver Hook;
    public Transform OriginHook;

    // properties
    float angleCabine;
    float distance;
    bool isAnimating = false;

    float angleArm;
    float distanceHookArm;


    Transform HookStartPos;

    private void Awake()
    {
        //HookStartPos = Hook.transform;
    }

    void Start()
    {
        //Vector3 a = new Vector3(SliderRotatin.transform.position.x, HookStartPos.position.y, HookStartPos.position.z);
        //distanceHookArm = Vector3.Distance(HookStartPos.position, a);
        //CalculateAngleSliderArm();
        //CalculateDistanceSlider(); 

        //isAnimating = true;
        //CalculateAngleCabine();
        //CalculateAngleSliderArm();
        //CalculateDistanceSlider();

        //Vector3 rot = Quaternion.AngleAxis(-20.53717f, OriginHook.transform.position) * OriginHook.transform.position;
        //OriginHook.transform.position = rot;
        //print("Rotation = " + rot);

        //Vector2 test = rotateVector(new Vector2(OriginHook.transform.position.z, OriginHook.transform.position.x), -20.53717f);
        //print(test);
    }
    
    Vector2 rotateVector(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    // Update is called once per frame
    void Update()
    {
        //CalculateDistanceSlider();

        //Vector3 a = new Vector3(SliderRotatin.transform.position.x, Hook.transform.position.y, Hook.transform.position.z);
        //Vector3 b = new Vector3(Hook.transform.position.x, Target.transform.position.y, Hook.transform.position.z);

        //float aa = Vector3.Distance(Hook.transform.position, a); // calculate distance between hook and arm
        //float bb = Vector3.Distance(Hook.transform.position, b) + 1f; // calculate distance between hook and target

        //float angle = Mathf.Atan(bb / distanceHookArm) * Mathf.Rad2Deg; // calculate angle
        ////print("angle green red = " + angle);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //CalculateAngleCabine();
        //    //CalculateAngleSliderArm();
        //    CalculateDistanceSlider();
        //}


        //AnimationArm();

        //if (isAnimating)
        //{
        //    AnimationCabine();
        //}
    }

    void AnimationCabine()
    {
        // Rotate Cabine 
        CrankMechanism crank = Cabine.mechanism as CrankMechanism;
        float angleSlider = SliderRotatin.GetComponent<LimitCrank>().range.max + SliderRotatin.GetComponent<LimitCrank>().StartAngles.z;

        if (crank.Angle != angleCabine )
        {
            // Rotate Slider
            Cabine.DriveMechanism();
           
        }
        else
        {
            //CalculateDistanceSlider();
            isAnimating = false;          
        }
    }

    void AnimationArm()
    {
        if (SliderRotatin.GetComponent<LimitCrank>().Angle != angleArm)
        {
            SliderRotatin.DriveMechanism();
        }
        else
        {
            isAnimating = false;
        }
    }

    #region Calcute
    void CalculateAngleCabine()
    {
        // Calculate angle
        Vector3 targetDir = new Vector3(Target.transform.position.x, Cabine.transform.position.y, Target.transform.position.z) - Cabine.transform.position;
        //angleCabine = Vector3.Angle(targetDir, Cabine.transform.up);
        angleCabine = Vector3.Angle(targetDir, Cabine.transform.up);

        //Get dot for calculate direction
        Vector3 forward = Cabine.transform.TransformDirection(Cabine.transform.up);
        Vector3 toOther = Target.transform.position - Cabine.transform.position;

        float dot = Vector3.Dot(forward.normalized, toOther.normalized);

        float maxAngle = Cabine.GetComponent<LimitCrank>().range.max;
        float minAngle = Cabine.GetComponent<LimitCrank>().range.min;

        if (dot < 0)
        {
            Cabine.GetComponent<LimitCrank>().range.max = angleCabine + minAngle;
            Cabine.direction = MechanismDriver.Direction.Positive;
        } 
        else if (dot > 0)
        {
            // Set min angle2
            Cabine.GetComponent<LimitCrank>().range.min = angleCabine - maxAngle;
            Cabine.GetComponent<LimitCrank>().range.min *= -1f;
            angleCabine *= -1;
            Cabine.direction = MechanismDriver.Direction.Negative;
        }

        //print("dot = " + dot);
        //print("CabineAngle = " + angleCabine);

        //print("dir = " + targetDir);
        //print("Up = " + Cabine.transform.up);

    }

    void CalculateAngleSliderArm()
    {
        Vector3 a = new Vector3(SliderRotatin.transform.position.x, HookStartPos.position.y, HookStartPos.position.z);
        Vector3 b = new Vector3(HookStartPos.position.x, Target.transform.position.y, HookStartPos.position.z);

        float aa = Vector3.Distance(HookStartPos.position, a); // calculate distance between hook and arm
        float bb = Vector3.Distance(HookStartPos.position, b) + 1f; // calculate distance between hook and target

        float angle = Mathf.Atan(bb / distanceHookArm) * Mathf.Rad2Deg; // calculate angle
        angleArm = angle;

        float maxAngle = SliderRotatin.GetComponent<LimitCrank>().range.max;
        float minAngle = SliderRotatin.GetComponent<LimitCrank>().range.min;

        if (angle < maxAngle)
        {
            if (angle > minAngle && minAngle != 0)
            {
                SliderRotatin.GetComponent<LimitCrank>().range.max = angle;
                SliderRotatin.direction = MechanismDriver.Direction.Positive;
            }
            else
            {
                SliderRotatin.GetComponent<LimitCrank>().range.min = angle;
                SliderRotatin.direction = MechanismDriver.Direction.Negative;
            }
        }
        else
        {
            SliderRotatin.GetComponent<LimitCrank>().range.max = angle;
            SliderRotatin.direction = MechanismDriver.Direction.Positive;
        }

        //Ray ray = new Ray(Hook.transform.position, Vector3.right);
        //Vector3 pos = Target.transform.position - Hook.transform.position;
        //float angle = Vector3.Angle(pos, ray.direction);
        //angleArm = angle;

        //float maxAngle = SliderRotatin.GetComponent<LimitCrank>().range.max;
        //float minAngle = SliderRotatin.GetComponent<LimitCrank>().range.min;

        //if (angle < maxAngle)
        //{
        //    SliderRotatin.GetComponent<LimitCrank>().range.min = angle;
        //    SliderRotatin.direction = MechanismDriver.Direction.Negative;
        //}
        //else
        //{
        //    SliderRotatin.GetComponent<LimitCrank>().range.max = angle;
        //    SliderRotatin.direction = MechanismDriver.Direction.Positive;
        //}



        // calculate dot
        //Vector3 forward = Hook.transform.TransformDirection(Vector3.forward);
        //Vector3 toOther = Target.transform.position - Hook.transform.position;
        //Ray ray = new Ray(Hook.transform.position, Vector3.right);

        //float dot = Vector3.Dot(forward.normalized, toOther.normalized);


        //// Set angle and direction
        //if (dot > 0)
        //{
        //    SliderRotatin.GetComponent<LimitCrank>().range.max = angle;
        //    SliderRotatin.direction = MechanismDriver.Direction.Positive;
        //}
        //else if ( dot < 0)
        //{
        //    SliderRotatin.GetComponent<LimitCrank>().range.min = angle;
        //    SliderRotatin.direction = MechanismDriver.Direction.Negative;
        //}

        print("SliderAngle = " + angle);
        //print("dot = " + dot);
        // Calculate angle

        //Vector3 pos = new Vector3(Target.transform.position.x, Target.transform.position.y,Hook.transform.position.z);
        //Vector3 dir = pos - Hook.transform.position;
        //Ray ray = new Ray(Hook.transform.position, Vector3.right);

        //float angle = Vector3.Angle(ray.direction, dir);

        //Vector3 a = new Vector3(Target.transform.position.x, Hook.transform.position.y, Hook.transform.position.z);


        //Vector3 targetDir = new Vector3(Target.transform.position.x, Target.transform.position.y, Hook.transform.position.z) - Hook.transform.position;
        //float angle = Vector3.Angle(targetDir, Hook.transform.position);

        //Vector3 targetDir = new Vector3(Target.transform.position.x, OriginHook.transform.position.y, Target.transform.position.z) - OriginHook.transform.position;
        //float angle = Vector3.Angle(targetDir, OriginHook.transform.forward);

        //angle += 2f; // differece for hook distance and etc.
    }

    void CalculateDistanceSlider()
    {
        // Calculate distnace trought cos
        Vector3 targetPos = new Vector3(Target.transform.position.x, Hook.transform.position.y, Target.transform.position.z);
        float dist = Vector3.Distance(Hook.transform.position, targetPos);
        print("distance x = " + dist);

        float angle = SliderRotatin.GetComponent<LimitCrank>().range.max + SliderRotatin.GetComponent<LimitCrank>().StartAngles.z;

        dist = dist / Mathf.Cos(angle * Mathf.Deg2Rad);
        CalculateDistanceHook(dist, angle);

        float wholeDistance = 0;

        // set each slider distance
        for (int i = 0; i < 3; i++)
        {
            wholeDistance += Slider.GetComponent<SliderArm>().sliders[i].stroke.max;
        }

        // set each slider distance
        for (int i = 0; i < 3; i++)
        {
            print("i = " + i + " distanceSlider = " + dist);

            if (dist >= 5)
            {
                Slider.GetComponent<SliderArm>().sliders[i].stroke.max = 5;
                dist -= 5;
            }
            else
            {
                Slider.GetComponent<SliderArm>().sliders[i].stroke.max = dist;
                dist = 0;
            }
        }

        //// Calculate distnace trought cos
        //Vector3 targetPos = new Vector3(Target.transform.position.x, OriginHook.transform.position.y, Target.transform.position.z);
        //float dist = Vector3.Distance(OriginHook.transform.position, targetPos);
        //print("distance x = " + dist);

        //float angle = SliderRotatin.GetComponent<LimitCrank>().range.max + SliderRotatin.GetComponent<LimitCrank>().StartAngles.z;

        //dist = dist / Mathf.Cos(angle * Mathf.Deg2Rad);
        //CalculateDistanceHook(dist, angle);

        //dist = dist / Mathf.Cos(angle * Mathf.Deg2Rad);

        //// set each slider distance
        //for (int i = 0; i < 3; i++)
        //{
        //    print("i = " + i + " distanceSlider = " + dist);

        //    if (dist >= 5)
        //    {
        //        Slider.GetComponent<SliderArm>().sliders[i].stroke.max = 5;
        //        dist -= 5;
        //    }
        //    else
        //    {
        //        Slider.GetComponent<SliderArm>().sliders[i].stroke.max = dist;
        //        dist = 0;
        //    }
        //}
    }

    void CalculateDistanceHook(float distanceSlider, float angle)
    {
        float dist = distanceSlider * Mathf.Sin(angle * Mathf.Deg2Rad);

        dist = dist - 3f < 0 ? 0 : dist - 3f;

        Hook.GetComponent<Slider>().stroke.max = dist;

        print("Hook distance = " + dist);
    }
    #endregion


    void AnimationHook()
    {
        Hook.GetComponent<Slider>().stroke.max = 1;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(Cabine.transform.position, Target.transform.position);

        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(OriginHook.position, Target.transform.position);

        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(Cabine.transform.position, Cabine.transform.up - Cabine.transform.position);

        //Vector3 rot = Quaternion.AngleAxis(-20.53717f,Vector3.up) * Hook.transform.position;
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(OriginHook.transform.position, rot);

        //Gizmos.color = Color.red;
        //Vector3 pos = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
        //Vector3 dir = pos - Hook.transform.position;
        //Gizmos.DrawRay(Hook.transform.position, dir);

        //Gizmos.color = Color.green;
        //Ray ray = new Ray(Hook.transform.position, Vector3.right);
        //Gizmos.DrawRay(ray);

        //Gizmos.color = Color.blue;
        //Ray ray1 = new Ray(Hook.transform.position, Hook.transform.right );
        //Gizmos.DrawRay(ray1);
    }
}
