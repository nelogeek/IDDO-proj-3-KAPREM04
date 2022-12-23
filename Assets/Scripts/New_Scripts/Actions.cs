using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
    #region MyRegion
    RaycastHit hit;
    Ray ray;

    public GameObject controller;
    public GameObject cam1person;
    public GameObject cam3person;
    public GameObject camOilperson;

    public GameObject planeNPS;
    public GameObject planePump;
    public GameObject planeCTI;
    public GameObject planeEj;

    public GameObject nps;
    public GameObject pump;
    public GameObject cti;
    public GameObject ej;

    public Button btnSDWSH;
    public Button btnCSD;
    public Button btnInj;

    public GameObject fp;
    public GameObject sdwsh;
    public GameObject csd;
    public GameObject inj;

    public GameObject connection;

    public static int step_count;
    public static int next_count;
    public static bool even;
    public static bool person1Key;
    public static bool person3Key;
    public static bool personOilKey;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        btnSDWSH.interactable = false;
        btnCSD.interactable = false;
        btnInj.interactable = false;

        step_count = 1;
        next_count = 2;
        even = false;
        person1Key = true;
        person3Key = false;
        personOilKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!InputManager.isPaused)
        {
            if (BuildingsGrid_KAPREM.npsKey && BuildingsGrid_KAPREM.pumpKey && BuildingsGrid_KAPREM.ctiKey && BuildingsGrid_KAPREM.ejKey)
            {
                connection.SetActive(true);
            }
            else
            {
                connection.SetActive(false);
            }

            //if (Input.GetKeyDown(KeyCode.V))
            //{
            //    if (person1Key || personOilKey)
            //    {
            //        controller.transform.localPosition = new Vector3(131.1f, 1.81f, -22.5f);
            //        cam3person.SetActive(true);
            //        cam1person.SetActive(false);
            //        camOilperson.SetActive(false);
            //        planeNPS.GetComponent<MeshRenderer>().enabled = true;
            //        planePump.GetComponent<MeshRenderer>().enabled = true;
            //        planeCTI.GetComponent<MeshRenderer>().enabled = true;
            //        planeEj.GetComponent<MeshRenderer>().enabled = true;
            //        person1Key = false;
            //        personOilKey = false;
            //        person3Key = true;
            //    }
            //}

            //if (Input.GetKeyDown(KeyCode.N))
            //{
            //    if (person3Key || personOilKey)
            //    {
            //        cam1person.SetActive(true);
            //        cam3person.SetActive(false);
            //        camOilperson.SetActive(false);
            //        planeNPS.GetComponent<MeshRenderer>().enabled = false;
            //        planePump.GetComponent<MeshRenderer>().enabled = false;
            //        planeCTI.GetComponent<MeshRenderer>().enabled = false;
            //        planeEj.GetComponent<MeshRenderer>().enabled = false;
            //        person3Key = false;
            //        personOilKey = false;
            //        person1Key = true;
            //    }
            //}

            //if (Input.GetKeyDown(KeyCode.B))
            //{
            //    if (person3Key || person1Key)
            //    {
            //        controller.transform.localPosition = new Vector3(131.1f, 1.81f, -22.5f);
            //        camOilperson.SetActive(true);
            //        cam1person.SetActive(false);
            //        cam3person.SetActive(false);
            //        planeNPS.GetComponent<MeshRenderer>().enabled = false;
            //        planePump.GetComponent<MeshRenderer>().enabled = false;
            //        planeCTI.GetComponent<MeshRenderer>().enabled = false;
            //        planeEj.GetComponent<MeshRenderer>().enabled = false;
            //        personOilKey = true;
            //        person1Key = false;
            //        person3Key = false;
            //    }
            //}

            if (BuildingsGrid_KAPREM.delKey)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.collider.gameObject.name.Equals("Nitrogen_Pumping_Station"))
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            nps.SetActive(false);
                            BuildingsGrid_KAPREM.npsKey = false;
                        }
                    }

                    if (hit.collider.gameObject.name.Equals("Pump_Unit"))
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            pump.SetActive(false);
                            BuildingsGrid_KAPREM.pumpKey = false;
                        }
                    }

                    if (hit.collider.gameObject.name.Equals("Coiled_Tubing_Installation"))
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            cti.SetActive(false);
                            BuildingsGrid_KAPREM.ctiKey = false;
                            inj.SetActive(false);
                            BuildingsGrid_KAPREM.injKey = false;
                            btnInj.interactable = false;
                        }
                    }

                    if (hit.collider.gameObject.name.Equals("Ejector"))
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            ej.SetActive(false);
                            BuildingsGrid_KAPREM.ejKey = false;
                        }
                    }
                }
            }

            if (BuildingsGrid_KAPREM.delOilKey)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.collider.gameObject.name.Equals("Four-Plash"))
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            if (!BuildingsGrid_KAPREM.sdwshKey && !BuildingsGrid_KAPREM.csdKey && !BuildingsGrid_KAPREM.injKey)
                            {
                                fp.SetActive(false);
                                BuildingsGrid_KAPREM.fpKey = false;
                                btnSDWSH.interactable = false;
                                btnCSD.interactable = false;
                            }
                        }
                    }

                    if (hit.collider.gameObject.name.Equals("Sealing_Device_With_Side_Hatch"))
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            if (!BuildingsGrid_KAPREM.injKey)
                            {
                                sdwsh.SetActive(false);
                                BuildingsGrid_KAPREM.sdwshKey = false;
                                btnInj.interactable = false;
                            }
                        }
                    }

                    if (hit.collider.gameObject.name.Equals("Conventional_Sealing_Device"))
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            if (!BuildingsGrid_KAPREM.injKey)
                            {
                                csd.SetActive(false);
                                BuildingsGrid_KAPREM.csdKey = false;
                                btnInj.interactable = false;
                            }
                        }
                    }

                    if (hit.collider.gameObject.name.Equals("Injector"))
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            inj.SetActive(false);
                            BuildingsGrid_KAPREM.injKey = false;
                        }
                    }
                }
            }
        }
    }

    public void Cam3D()
    {
        if (person3Key || personOilKey)
        {
            cam1person.SetActive(true);
            cam3person.SetActive(false);
            camOilperson.SetActive(false);
            planeNPS.GetComponent<MeshRenderer>().enabled = false;
            planePump.GetComponent<MeshRenderer>().enabled = false;
            planeCTI.GetComponent<MeshRenderer>().enabled = false;
            planeEj.GetComponent<MeshRenderer>().enabled = false;
            person3Key = false;
            personOilKey = false;
            person1Key = true;
        }
    }

    public void CamUp()
    {
        if (person1Key || personOilKey)
        {
            controller.transform.localPosition = new Vector3(131.1f, 1.81f, -22.5f);
            cam3person.SetActive(true);
            cam1person.SetActive(false);
            camOilperson.SetActive(false);
            planeNPS.GetComponent<MeshRenderer>().enabled = true;
            planePump.GetComponent<MeshRenderer>().enabled = true;
            planeCTI.GetComponent<MeshRenderer>().enabled = true;
            planeEj.GetComponent<MeshRenderer>().enabled = true;
            person1Key = false;
            personOilKey = false;
            person3Key = true;
        }
    }

    public void CamBorehole()
    {
        if (person3Key || person1Key)
        {
            controller.transform.localPosition = new Vector3(131.1f, 1.81f, -22.5f);
            camOilperson.SetActive(true);
            cam1person.SetActive(false);
            cam3person.SetActive(false);
            planeNPS.GetComponent<MeshRenderer>().enabled = false;
            planePump.GetComponent<MeshRenderer>().enabled = false;
            planeCTI.GetComponent<MeshRenderer>().enabled = false;
            planeEj.GetComponent<MeshRenderer>().enabled = false;
            personOilKey = true;
            person1Key = false;
            person3Key = false;
        }
    }

    public void Again()
    {
        Application.LoadLevel(1);
    }
}
