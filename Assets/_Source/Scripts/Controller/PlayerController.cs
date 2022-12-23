using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //Camera_Contoroll
    GameObject cam;
    CharacterController controller;
    float LookUp = 0.0f;
    float LookAside = 0.0f;
    float MovementForward = 0.0f;
    float MovementAside = 0.0f;

    [Header("Control Settings")]
    public float MovementSpeed = 3.0F;
    public float MouseSensetivityX = 5.0f;
    public float MouseSensetivityY = 5.0f;

    //Othr_Variabels    
    public Texture2D Tex2D_Aim;

    void Start() {
        instance = this.GetComponent<PlayerController>();
        //Controller
        controller = GetComponent<CharacterController>();
        cam = transform.GetChild(0).gameObject;
        
    }
    public bool isTeleporting = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            MovementSpeed = 8;
        }
        else
        {
            MovementSpeed = 3;
        }

        #region Input_Controller
        ControllerMovement();
        #endregion

        //Ray mouse_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Ray aim_ray = new Ray(cam.transform.position, cam.transform.forward);
        //RaycastHit hit;

        //if (!EventSystem.current.IsPointerOverGameObject()) // For block UI raycast
        //{
        //    if ((Physics.Raycast(aim_ray, out hit, 100) || Physics.Raycast(mouse_ray, out hit, 100)))
        //    {
        //        ToogleOutLine();
        //    }
        //}

    }

    //tern_On_off_OutlineEffect
    cakeslice.Outline outlineObject;

    void ToogleOutLine()
    {
        //Mouse_Ray
        Ray mouse_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray aim_ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        //DrawLine
        //Debug.DrawLine(mouse_ray.origin, mouse_ray.direction * 1300, Color.red);

        if (!Cursor.visible)
        {
            if (Physics.Raycast(aim_ray, out hit, 100))
            {

                if (hit.collider.gameObject.GetComponent<cakeslice.Outline>() != null)
                {
                    if (outlineObject != null)
                    {
                        outlineObject.enabled = false;
                        outlineObject = hit.collider.GetComponent<cakeslice.Outline>();
                        outlineObject.enabled = true;
                    }
                    else {
                        outlineObject = hit.collider.GetComponent<cakeslice.Outline>();
                        outlineObject.enabled = true;
                    }
                    
                }
                else
                {
                    if (outlineObject != null)
                    {
                        outlineObject.enabled = false;
                        outlineObject = null;
                    }
                }
            }
            else
            {
                if (outlineObject != null)
                {
                    outlineObject.enabled = false;
                    outlineObject = null;
                }
            }
        }

        if (Cursor.visible)
        {
            if (Physics.Raycast(mouse_ray, out hit, 100))
            {

                if (hit.collider.gameObject.GetComponent<cakeslice.Outline>() != null)
                {
                    if (outlineObject != null)
                    {
                        outlineObject.enabled = false;
                        outlineObject = hit.collider.GetComponent<cakeslice.Outline>();
                        outlineObject.enabled = true;
                    }
                    else
                    {
                        outlineObject = hit.collider.GetComponent<cakeslice.Outline>();
                        outlineObject.enabled = true;
                    }
                }
                else
                {
                    if (outlineObject != null)
                    {
                        outlineObject.enabled = false;
                        outlineObject = null;
                    }
                }
            }
            else
            {
                if (outlineObject != null)
                {
                    outlineObject.enabled = false;
                    outlineObject = null;
                }
            }
        }
    }

    void ControllerMovement()
    {
        MovementForward = Input.GetAxis("Vertical");
        MovementAside = Input.GetAxis("Horizontal");
        LookAside = Input.GetAxis("Mouse X") * MouseSensetivityX;
        LookUp = Input.GetAxis("Mouse Y") * MouseSensetivityY;

        
        if (!InputManager.isStopedController && !Actions.person3Key && !Actions.personOilKey)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            transform.Rotate(0, LookAside, 0);
            cam.transform.localRotation *= Quaternion.AngleAxis(LookUp, Vector3.left);
            cam.transform.localRotation = ClampRotationAroundXAxis(cam.transform.localRotation);
            Vector3 vector = transform.TransformDirection((MovementForward * Vector3.forward + MovementAside * Vector3.right) * MovementSpeed);
            controller.SimpleMove(vector);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cam.transform.localRotation = cam.transform.localRotation;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, -85, 75);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    float deltaTime = 0.0f;
    private void OnGUI()
    {
        if (!Cursor.visible)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2f, Screen.height / 2f, 10, 10), Tex2D_Aim);
        }


#if UNITY_EDITOR
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.white;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
#endif
    }
}
