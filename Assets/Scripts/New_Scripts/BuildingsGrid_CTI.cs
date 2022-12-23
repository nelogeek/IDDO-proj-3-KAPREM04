﻿using UnityEngine;

public class BuildingsGrid_CTI : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private Building[,] grid;
    private Building flyingBuilding;
    public Camera mainCamera;

    public GameObject cti;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];

        //mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        BuildingsGrid_KAPREM.npsBuild = false;
        BuildingsGrid_KAPREM.pumpBuild = false;
        BuildingsGrid_KAPREM.ctiBuild = true;
        BuildingsGrid_KAPREM.ejBuild = false;
        flyingBuilding = Instantiate(buildingPrefab);
    }

    private void Update()
    {
        if (!InputManager.isPaused)
        {
            if (flyingBuilding != null && BuildingsGrid_KAPREM.clearKey)
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.ctiBuild = false;
                BuildingsGrid_KAPREM.clearKey = false;
            }

            if (flyingBuilding != null && (Actions.person1Key || Actions.personOilKey))
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.ctiBuild = false;
                BuildingsGrid_KAPREM.clearKey = false;
            }

            if (flyingBuilding != null && (BuildingsGrid_KAPREM.npsBuild || BuildingsGrid_KAPREM.pumpBuild || BuildingsGrid_KAPREM.ejBuild))
            {
                Destroy(flyingBuilding.gameObject);
                flyingBuilding = null;
                BuildingsGrid_KAPREM.ctiBuild = false;
            }

            if (flyingBuilding != null)
            {
                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (groundPlane.Raycast(ray, out float position))
                {
                    Vector3 worldPosition = ray.GetPoint(position);

                    int x = Mathf.RoundToInt(worldPosition.x);
                    int y = Mathf.RoundToInt(worldPosition.z);

                    bool available = true;

                    if (x < 140 || x > GridSize.x - flyingBuilding.Size.x)
                    {
                        available = false;
                        //Debug.Log("Debug1: " + available + "; x = " + x + "; Grid = " + (GridSize.x - flyingBuilding.Size.x));
                    }
                    if (y < -22 || y > (GridSize.y - flyingBuilding.Size.y) * (-1))
                    {
                        //Debug.Log("Debug2: " + available + "; y = " + y + "; Grid = " + (GridSize.y - flyingBuilding.Size.y));
                        available = false;
                    }

                    flyingBuilding.transform.position = new Vector3(x, 0.79f, y);
                    flyingBuilding.SetTransparent(available);

                    if (available && Input.GetMouseButtonDown(0))
                    {
                        PlaceFlyingBuilding(x, y);
                    }
                }
            }
        }
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        Destroy(flyingBuilding.gameObject);
        cti.SetActive(true);
        flyingBuilding = null;
        BuildingsGrid_KAPREM.ctiBuild = false;
        BuildingsGrid_KAPREM.ctiKey = true;
    }
}
