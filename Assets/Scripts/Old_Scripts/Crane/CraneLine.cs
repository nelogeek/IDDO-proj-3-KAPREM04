using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
//[ExecuteAlways]
[ExecuteInEditMode]
public class CraneLine : MonoBehaviour
{
    LineRenderer line;

    // The Start of the cable will be the transform of the Gameobject that has this component.
    // The Transform of the Gameobject where the End of the cable is. This needs to be assigned in the inspector.
    [SerializeField] Transform endPointTransform;
    [SerializeField] Transform startPointTransform;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, startPointTransform.position);
        line.SetPosition(1, endPointTransform.position);

    }
}
