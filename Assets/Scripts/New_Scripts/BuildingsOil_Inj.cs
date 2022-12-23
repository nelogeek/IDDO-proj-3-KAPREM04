using UnityEngine;
using UnityEngine.UI;

public class BuildingsOil_Inj : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private Building[,] grid;
    private Building flyingBuilding;
    public Camera mainCamera;

    public GameObject borehole;
    public GameObject inj;

    public Button btnInj;

    public static bool available;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];

        //mainCamera = Camera.main;

        available = false;
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (BuildingsGrid_KAPREM.sdwshKey)
        {
            if (flyingBuilding != null)
            {
                Destroy(flyingBuilding.gameObject);
            }

            BuildingsGrid_KAPREM.fpBuild = false;
            BuildingsGrid_KAPREM.sdwshBuild = false;
            BuildingsGrid_KAPREM.csdBuild = false;
            BuildingsGrid_KAPREM.injBuild = true;
            flyingBuilding = Instantiate(buildingPrefab);
        }
    }

    private void Update()
    {
        if (!InputManager.isPaused)
        {
            if (!BuildingsGrid_KAPREM.delOilKey)
            {
                if (BuildingsGrid_KAPREM.fpKey && BuildingsGrid_KAPREM.sdwshKey && BuildingsGrid_KAPREM.ctiKey && btnInj.interactable == false)
                {
                    btnInj.interactable = true;
                }
                else if (!BuildingsGrid_KAPREM.fpKey && !BuildingsGrid_KAPREM.sdwshKey && btnInj.interactable == true || !BuildingsGrid_KAPREM.ctiKey)
                {
                    btnInj.interactable = false;
                }
            }

            if (flyingBuilding != null && BuildingsGrid_KAPREM.clearOilKey)
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.injBuild = false;
                BuildingsGrid_KAPREM.clearOilKey = false;
            }

            if (flyingBuilding != null && (Actions.person1Key || Actions.person3Key))
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.injBuild = false;
                BuildingsGrid_KAPREM.clearOilKey = false;
            }

            if (flyingBuilding != null && (BuildingsGrid_KAPREM.fpBuild || BuildingsGrid_KAPREM.sdwshBuild || BuildingsGrid_KAPREM.csdBuild))
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.injBuild = false;
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

                    flyingBuilding.transform.position = new Vector3(155.137f, y, z);
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
        if (BuildingsGrid_KAPREM.fpKey && BuildingsGrid_KAPREM.sdwshKey)
        {
            available = true;
        }
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
        BuildingsOil_FP.available = false;
        BuildingsOil_SDWSH.available = false;
        BuildingsOil_CSD.available = false;
        BuildingsOil_Inj2.available = false;
        inj.SetActive(true);
        BuildingsGrid_KAPREM.injBuild = false;
        BuildingsGrid_KAPREM.injKey = true;
    }
}
