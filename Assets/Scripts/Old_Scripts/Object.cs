using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [HideInInspector] public Vector3 TopPoint;
    [HideInInspector] public Vector3 DownPoint;
    [SerializeField]  Vector3 rotationOnSetup;

    public Vector3 StartPosition;
    public bool isCouldCarry;
    public bool isOnSetup;

    public View.Objectinfo objectinfo;

    ConnectionCircle circle;
    GameObject startPointObject;
    GameObject startParent;

    // Start is called before the first frame update
    void Start()
    {
        isCouldCarry = true;
        isOnSetup = false;

        StartPosition = this.transform.position;

        // Get link
        circle = this.transform.GetChild(0).GetComponent<ConnectionCircle>();

        // Create start target
        startPointObject = new GameObject();
        startPointObject.transform.position = StartPosition;
        startPointObject.transform.rotation = this.transform.rotation;
        startPointObject.transform.localScale = Vector3.one;
        startPointObject.name = "StartPoint " + this.name ;
        startParent = this.transform.parent.gameObject;
    }

    private void Update()
    {
        CalcPoints();
    }

    private void OnMouseDown()
    {
        // KAPREM02
        if (!Crane.instance.isAnimation && isCouldCarry)
        {
            if (!isOnSetup)
            {
                if(Setup.instance.AddObject(this))
                    Crane.instance.StartAnimation(this.gameObject, Setup.instance.PenultObject.gameObject); // Put on Setup
            }
            else
            {
                if(Setup.instance.RemoveObject(this))
                    Crane.instance.StartAnimation(this.gameObject, startPointObject); // Put on start place
            }
        }
    }

    #region Set position
    public void SetPositionAfterRelease()
    {
        // Get curr object
        Object @object = this;

        if (isOnSetup)
        {
            // Set position on Setup

            // Find last object
            Object lastObj = Setup.instance.objects[Setup.instance.objects.Count - 2];

            // Calculate distance between center and down
            float distance = Vector3.Distance(@object.transform.position, @object.DownPoint);

            // Set position
            Vector3 pos = lastObj.TopPoint;
            pos.y += distance;
            @object.transform.position = pos;

            // Set rotation
            @object.transform.eulerAngles = rotationOnSetup;

            // Set parnet
            this.transform.parent = Setup.instance.transform;
        }
        else
        {
            // set positoin on start place

            //Set position
            @object.transform.position = StartPosition;

            //Set rotation on start place
            @object.transform.rotation = startPointObject.transform.rotation;

            // Set parnet
            this.transform.parent = startParent.transform;
        }
    }

    #endregion

    #region Connection Circle
    public void ShowConnectionLine()
    {
        circle.Show();
    }

    public void HideConnectionLine()
    {
        circle.Hide();
    }
    #endregion

    private void CalcPoints()
    {
        Renderer rend = this.GetComponent<Renderer>();
        Vector3 center = rend.bounds.center;

        TopPoint = center;
        TopPoint.y = rend.bounds.max.y;

        DownPoint = center;
        DownPoint.y = rend.bounds.min.y;
    }

    private void OnDrawGizmos()
    {
        Renderer rend = this.GetComponent<Renderer>();
        Vector3 center = rend.bounds.center;
        Vector3 top = center;
        top.y = rend.bounds.max.y;

        Vector3 down = center;
        down.y = rend.bounds.min.y;

        // Center point
        Gizmos.color = Color.red;
        Gizmos.DrawCube(center, new Vector3(0.1f, 0.1f, 0.1f));

        // Top point
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(top, new Vector3(0.1f, 0.1f, 0.1f));

        // Down point
        Gizmos.color = Color.green;
        Gizmos.DrawCube(down, new Vector3(0.1f, 0.1f, 0.1f));

        // Left point
        Vector3 left = center;
        left.y = rend.bounds.min.y;
        left.z = rend.bounds.min.z;

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(left, new Vector3(0.2f, 0.2f, 0.2f));

        // Right point 
        Vector3 right = center;
        right.y = rend.bounds.min.y;
        right.z = rend.bounds.max.z;

        Gizmos.color = Color.white;
        Gizmos.DrawCube(right, new Vector3(0.2f, 0.2f, 0.2f));

        // Rays
        Gizmos.color = Color.red;
        Ray ray = new Ray(transform.position, Vector3.up);
        Gizmos.DrawRay(ray);
    }
}
