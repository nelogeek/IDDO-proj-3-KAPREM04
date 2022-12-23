using UnityEngine;

public class BuildingsOil_FP : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private Building[,] grid;
    private Building flyingBuilding;
    public Camera mainCamera;

    public GameObject borehole;
    public GameObject fp;

    public static bool available;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];

        //mainCamera = Camera.main;

        available = false;
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        BuildingsGrid_KAPREM.fpBuild = true;
        BuildingsGrid_KAPREM.sdwshBuild = false;
        BuildingsGrid_KAPREM.csdBuild = false;
        BuildingsGrid_KAPREM.injBuild = false;
        flyingBuilding = Instantiate(buildingPrefab);
    }

    private void Update()
    {
        if (!InputManager.isPaused)
        {
            if (flyingBuilding != null && BuildingsGrid_KAPREM.clearOilKey)
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.fpBuild = false;
                BuildingsGrid_KAPREM.clearOilKey = false;
            }

            if (flyingBuilding != null && (Actions.person1Key || Actions.person3Key))
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.fpBuild = false;
                BuildingsGrid_KAPREM.clearOilKey = false;
            }

            if (flyingBuilding != null && (BuildingsGrid_KAPREM.sdwshBuild || BuildingsGrid_KAPREM.csdBuild || BuildingsGrid_KAPREM.injBuild))
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.fpBuild = false;
            }

            if (flyingBuilding != null)
            {
                var groundPlane = new Plane(Vector3.right, borehole.transform.position);
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (groundPlane.Raycast(ray, out float position))
                {
                    Vector3 worldPosition = ray.GetPoint(position);

                    int z = Mathf.RoundToInt(worldPosition.z);
                    //Debug.Log("z = " + z);

                    int y = Mathf.RoundToInt(worldPosition.y);
                    //Debug.Log("y = " + y);

                    flyingBuilding.transform.position = new Vector3(155.09f, y, z);
                    flyingBuilding.SetTransparent(available);

                    if (available && Input.GetMouseButtonDown(0))
                    {
                        PlaceFlyingBuilding();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        available = true;
    }

    private void OnTriggerExit(Collider other)
    {
        available = false;
    }

    private void PlaceFlyingBuilding()
    {
        Destroy(flyingBuilding.gameObject);
        flyingBuilding = null;
        available = false;
        BuildingsOil_SDWSH.available = false;
        BuildingsOil_CSD.available = false;
        BuildingsOil_Inj.available = false;
        BuildingsOil_Inj2.available = false;
        fp.SetActive(true);
        BuildingsGrid_KAPREM.fpBuild = false;
        BuildingsGrid_KAPREM.fpKey = true;
    }
}
