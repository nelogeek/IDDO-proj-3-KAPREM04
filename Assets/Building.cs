using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;

    public void SetTransparent(bool available)
    {
        if (available)
        {
            if (MainRenderer.materials.Length > 1)
            {
                foreach (Material mat in MainRenderer.materials)
                {
                    mat.color = Color.green;
                }
            }
            else
            {
                MainRenderer.material.color = Color.green;
            }

            foreach (Transform child in MainRenderer.GetComponentsInChildren<Transform>())
            {
                if (child.GetComponent<MeshRenderer>().materials.Length > 1)
                {
                    foreach (Material mat in child.GetComponent<MeshRenderer>().materials)
                    {
                        mat.color = Color.green;
                    }
                }
                else
                {
                    child.GetComponent<MeshRenderer>().material.color = Color.green;
                }
            }
        }
        else
        {
            if (MainRenderer.materials.Length > 1)
            {
                foreach (Material mat in MainRenderer.materials)
                {
                    mat.color = Color.red;
                }
            }
            else
            {
                MainRenderer.material.color = Color.red;
            }

            foreach (Transform child in MainRenderer.GetComponentsInChildren<Transform>())
            {
                if (child.GetComponent<MeshRenderer>().materials.Length > 1)
                {
                    Debug.Log(child.GetComponent<MeshRenderer>().materials.Length);
                    foreach (Material mat in child.GetComponent<MeshRenderer>().materials)
                    {
                        mat.color = Color.red;
                    }
                }
                else
                {
                    child.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
    }

    public void SetNormal()
    {
        if (MainRenderer.materials.Length > 1)
        {
            foreach (Material mat in MainRenderer.materials)
            {
                mat.color = Color.white;
            }
        }
        else
        {
            MainRenderer.material.color = Color.white;
        }

        foreach (Transform child in MainRenderer.GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<MeshRenderer>().materials.Length > 1)
            {
                foreach (Material mat in child.GetComponent<MeshRenderer>().materials)
                {
                    mat.color = Color.white;
                }
            }
            else
            {
                child.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}