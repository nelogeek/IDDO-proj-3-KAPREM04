using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [System.Serializable]
    public struct Objectinfo {
        public string name;
        public string info;
        public List<Sprite> images;
        public Vector3 previewScale;
        public Vector3 previewRotation;
    }

    public static View instance;
    [HideInInspector] public bool isActive;
    
    [SerializeField] GameObject ViewCanvas;
    [SerializeField] Transform positionInFrontOfTheCamera;
    GameObject viewingObject;

    [Header("UI elements")]
    [SerializeField] Text nameText;
    [SerializeField] Text infoText;
    [SerializeField] Image image;
    //Sprite backgroundSprite;
    [SerializeField] GameObject renderTexture;
    List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //backgroundSprite = image.sprite;
        cam = this.GetComponent<Camera>();
    }

    int indexImg;

    // Rotation object
    Camera cam;
    Vector3 mPosDelta = Vector3.zero;
    Vector3 mPrevPos = Vector3.zero;

    private void Update()
    {
        // Set if we alrady have open panel
        if (ViewCanvas.activeSelf)
            isActive = true;
        else
            isActive = false;


        //mPosDelta = Input.mousePosition;
        if (isActive)
        {
            // Scrool wheel
            //if(Input.mouseScrollDelta.y != 0)
            //{
            //    Vector3 pos = viewingObject.transform.localPosition;
            //    pos.z += Input.mouseScrollDelta.y;

            //    viewingObject.transform.localPosition = pos;
            //}

            if (Input.GetMouseButton(0))
            {
                RotateViewingObject();

                //mPosDelta = Input.mousePosition - mPrevPos;

                //if (Vector3.Dot(viewingObject.transform.up, Vector3.up) >= 0)
                //{
                //    viewingObject.transform.Rotate(viewingObject.transform.up, -Vector3.Dot(mPosDelta, cam.transform.right), Space.World);
                //}
                //else
                //{
                //    viewingObject.transform.Rotate(viewingObject.transform.up, Vector3.Dot(mPosDelta, cam.transform.right), Space.World);
                //}

                //viewingObject.transform.Rotate(cam.transform.right, Vector3.Dot(mPosDelta, cam.transform.up), Space.World);
            }
            mPrevPos = Input.mousePosition;
        }
    }

    float SpeedRotation = 500;
    void RotateViewingObject()
    {
        float rotX = Input.GetAxis("Mouse X") * SpeedRotation * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * SpeedRotation * Mathf.Deg2Rad;

        viewingObject.transform.Rotate(Vector3.up, -rotX, Space.World);
        viewingObject.transform.Rotate(Vector3.right, rotY, Space.World);
        //viewingObject.transform.RotateAround(Vector3.up, -rotX);
        //viewingObject.transform.RotateAround(Vector3.right, rotY);
    }

    public void SetViewObject(GameObject obj)
    {
        // Show Canvas
        ViewCanvas.SetActive(true);

        // Stop Controller
        InputManager.isStopedController = true;

        // Destroy object is need
        if(this.transform.childCount != 0 )
            Destroy(this.transform.GetChild(0).gameObject);

        // Create instance and place in front of the camera
        GameObject instanceObject = Instantiate(obj,this.transform);
        Objectinfo objInfo = instanceObject.GetComponent<Object>().objectinfo;

        instanceObject.transform.position = positionInFrontOfTheCamera.transform.position;
        instanceObject.transform.localEulerAngles = objInfo.previewRotation;
        instanceObject.transform.localScale = objInfo.previewScale; //new Vector3(2.5f, 2.5f, 2.5f);

        viewingObject = instanceObject;
        
        // Write all inforamation about object in UI 
        nameText.text = objInfo.name;
        infoText.text = objInfo.info;
        sprites = objInfo.images;
        
        // Set first look image
        indexImg = sprites.Count;
        image.sprite = sprites[indexImg];
    }

    
    // UI methods
    public void btnNext()
    {
        // Increese index
        indexImg++;

        // Reboot index
        if (indexImg > sprites.Count)
            indexImg = 0;

        // Show render texture
        if (indexImg == sprites.Count)
        {
            renderTexture.SetActive(true);
        }
        else
        {
            renderTexture.SetActive(false);

            // Set image
            image.sprite = sprites[indexImg];
        }
    }

    public void btnPrev()
    {
        // Reduce index
        indexImg--;

        // Reboot index
        if (indexImg < 0)
            indexImg = sprites.Count;

        // Show render texture
        if (indexImg == sprites.Count)
        {
            renderTexture.SetActive(true);
        }
        else
        {
            renderTexture.SetActive(false);

            // Set image
            image.sprite = sprites[indexImg];
        }
    }

    public void btnClose()
    {
        renderTexture.SetActive(true);
        ViewCanvas.SetActive(false);

        InputManager.isStopedController = false;
    }
}
