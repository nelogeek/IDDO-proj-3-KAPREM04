using UnityEngine;
using UnityEngine.UI;

public class BuildingsGrid_KAPREM : MonoBehaviour
{
    public Button btnNPS;
    public Button btnPump;
    public Button btnCTI;
    public Button btnEj;

    public Button btnFP;
    public Button btnSDWSH;
    public Button btnCSD;
    public Button btnInj;

    public static bool npsBuild;
    public static bool pumpBuild;
    public static bool ctiBuild;
    public static bool ejBuild;
    public static bool npsKey;
    public static bool pumpKey;
    public static bool ctiKey;
    public static bool ejKey;
    public static bool clearKey;
    public static bool delKey;

    public static bool fpBuild;
    public static bool sdwshBuild;
    public static bool csdBuild;
    public static bool injBuild;
    public static bool fpKey;
    public static bool sdwshKey;
    public static bool csdKey;
    public static bool injKey;
    public static bool clearOilKey;
    public static bool delOilKey;

    public Text btnDel;
    public Text btnDelOil;

    private void Awake()
    {
        npsBuild = false;
        pumpBuild = false;
        ctiBuild = false;
        ejBuild = false;
        npsKey = false;
        pumpKey = false;
        ctiKey = false;
        ejKey = false;
        clearKey = false;
        delKey = false;

        fpBuild = false;
        sdwshBuild = false;
        csdBuild = false;
        injBuild = false;
        fpKey = false;
        sdwshKey = false;
        csdKey = false;
        injKey = false;
        clearOilKey = false;
        delOilKey = false;
    }

    private void Update()
    {
        //if (!InputManager.isPaused)
        //{
        //    if (Input.GetKeyDown(KeyCode.C))
        //    {

        //        if (Actions.person3Key)
        //        {
        //            clearKey = true;
        //        }
        //        else if (Actions.personOilKey)
        //        {
        //            clearOilKey = true;
        //        }
        //    }
        //}
    }

    public void ModeDel()
    {
        if (!npsBuild && !pumpBuild && !ctiBuild && !ejBuild)
        {
            if (!delKey)
            {
                btnDel.text = "Режим удаления: вкл";
                delKey = true;
                btnNPS.interactable = false;
                btnPump.interactable = false;
                btnCTI.interactable = false;
                btnEj.interactable = false;
            }
            else
            {
                btnDel.text = "Режим удаления: выкл";
                delKey = false;
                btnNPS.interactable = true;
                btnPump.interactable = true;
                btnCTI.interactable = true;
                btnEj.interactable = true;
            }
        }
    }

    public void ModeDelOil()
    {
        if (!fpBuild && !sdwshBuild && !csdBuild && !injBuild)
        {
            if (!delOilKey)
            {
                btnDelOil.text = "Режим удаления: вкл";
                delOilKey = true;
                btnFP.interactable = false;
                btnSDWSH.interactable = false;
                btnCSD.interactable = false;
                btnInj.interactable = false;
            }
            else
            {
                btnDelOil.text = "Режим удаления: выкл";
                delOilKey = false;
                btnFP.interactable = true;
                if (fpKey)
                {
                    btnSDWSH.interactable = true;
                    btnCSD.interactable = true;
                }
                if ((sdwshKey || csdKey) && ctiKey)
                {
                    btnInj.interactable = true;
                }
            }
        }
    }

    public void ModeClearAll()
    {
        if (Actions.person3Key)
        {
            clearKey = true;
        }
        else if (Actions.personOilKey)
        {
            clearOilKey = true;
        }
    }
}
