using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Hook : MonoBehaviour
{
    public Transform target;
    public Transform Arm;

    // Update is called once per frame
    void Update()
    {

        //Vector3 direction = target.forward + new Vector3(target.up.x, target.forward.y, target.up.z);
        //Vector3 from = Vector3.up;
        //Vector3 to = target.position - this.transform.position;
        //float angle = Vector3.Angle(from, to);

        // Rotation 
        Vector3 rot = new Vector3(-Arm.transform.localEulerAngles.z - 90, 0, -90);
        this.transform.localEulerAngles = rot;

        // Position
        Ray ray = new Ray(target.position, Vector3.down * 100f);
        Vector3 pos = ray.origin - ray.direction;
        pos.y -= 1.5f;

        this.transform.position = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, target.position);
        Gizmos.DrawLine(this.transform.position, target.position);
        //Gizmos.color = Color.red;
        //Ray ray = new Ray(target.position, Vector3.down * 100f);
        //Gizmos.DrawRay(ray);

        //Gizmos.color = Color.blue;
        //Ray ray1 = new Ray(transform.position, transform.forward);
        //Gizmos.DrawRay(ray1);

    }
}
