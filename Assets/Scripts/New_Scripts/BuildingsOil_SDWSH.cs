using UnityEngine;
using UnityEngine.UI;

public class BuildingsOil_SDWSH : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private Building[,] grid;
    private Building flyingBuilding;
    public Camera mainCamera;

    public GameObject borehole;
    public GameObject sdwsh;
    public GameObject csd;

    public Button btnSDWSH;

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

        BuildingsGrid_KAPREM.fpBuild = false;
        BuildingsGrid_KAPREM.sdwshBuild = true;
        BuildingsGrid_KAPREM.csdBuild = false;
        BuildingsGrid_KAPREM.injBuild = false;
        flyingBuilding = Instantiate(buildingPrefab);
    }

    private void Update()
    {
        if (!InputManager.isPaused)
        {
            if (!BuildingsGrid_KAPREM.delOilKey)
            {
                if (BuildingsGrid_KAPREM.fpKey && btnSDWSH.interactable == false)
                {
                    btnSDWSH.interactable = true;
                }
                else if (!BuildingsGrid_KAPREM.fpKey && btnSDWSH.interactable == true)
                {
                    btnSDWSH.interactable = false;
                }
            }

            if (flyingBuilding != null && BuildingsGrid_KAPREM.clearOilKey)
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.sdwshBuild = false;
                BuildingsGrid_KAPREM.clearOilKey = false;
            }

            if (flyingBuilding != null && (Actions.person1Key || Actions.person3Key))
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.sdwshBuild = false;
                BuildingsGrid_KAPREM.clearOilKey = false;
            }

            if (flyingBuilding != null && (BuildingsGrid_KAPREM.fpBuild || BuildingsGrid_KAPREM.csdBuild || BuildingsGrid_KAPREM.injBuild))
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.sdwshBuild = false;
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

                    flyingBuilding.transform.position = new Vector3(155.136f, y, z);
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
        if (BuildingsGrid_KAPREM.fpKey)
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
        BuildingsOil_CSD.available = false;
        BuildingsOil_Inj.available = false;
        BuildingsOil_Inj2.available = false;
        sdwsh.SetActive(true);
        csd.SetActive(false);
        BuildingsGrid_KAPREM.csdKey = false;
        BuildingsGrid_KAPREM.sdwshBuild = false;
        BuildingsGrid_KAPREM.sdwshKey = true;
    }
}
