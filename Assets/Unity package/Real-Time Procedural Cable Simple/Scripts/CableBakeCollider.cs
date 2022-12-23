using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CableBakeCollider : MonoBehaviour
{
    LineRenderer line;
    // Update is called once per frame
    void Update()
    {
        line = this.GetComponent<LineRenderer>();
        BakeMesh();
    }

    void BakeMesh()
    {
        foreach (MeshCollider mesh1 in line.GetComponents<MeshCollider>())
        {
            DestroyImmediate(mesh1);
        }

        //Destroy(line.gameObject.AddComponent<MeshCollider>());
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        MeshCollider meshCollider = line.gameObject.AddComponent<MeshCollider>();

        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        meshCollider.sharedMesh = mesh;

        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

}
