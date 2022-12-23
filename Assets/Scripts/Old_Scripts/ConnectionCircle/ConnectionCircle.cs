using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConnectionCircle : MonoBehaviour
{
    GameObject circle;
    GameObject parent;
    [SerializeField] bool isWorkinEditor;

    public float OffestCircle;
    public float OffestConnectionCablePoint;
    
    [SerializeField] Transform LeftCable;
    [SerializeField] Transform RightCable;

    void Start()
    {
        circle = this.gameObject;
        parent = this.transform.parent.gameObject;
    }

    void Update()
    {
        if(isWorkinEditor)
            SetConnectionPoints();
    }

    public void SetConnectionPoints()
    {
        Renderer rend = parent.GetComponent<Renderer>();
        Vector3 center = rend.bounds.center;
        center.y += OffestCircle;
        circle.transform.position = center;


        Vector3 leftPoint = center;
        leftPoint.y = rend.bounds.min.y + OffestConnectionCablePoint;
        leftPoint.z = rend.bounds.min.z;

        LeftCable.position = leftPoint;

        Vector3 rightPoint = center;
        rightPoint.y = rend.bounds.min.y + OffestConnectionCablePoint;
        rightPoint.z = rend.bounds.max.z;

        RightCable.position = rightPoint;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
