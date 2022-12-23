using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

public class PDF : MonoBehaviour {

    public List<Sprite> ListImageManualPDF; // Создаем Лист картинок
    public List<Sprite> ListImageInsctructionPDF; // Создаем Лист картинок
    public Image InstanceIMG;// Исходник картинки 
    public GameObject ContentManual;// Куда кладем картинку
    public GameObject ContentInstruction;

	// Use this for initialization
	void Start ()
    {
        int countSprite = Resources.FindObjectsOfTypeAll(typeof(Sprite)).Length;

        // Add in list what we have in folder Resources
        for( int j = 0; j < 3; j++)
        {
            for (int i = 0; i < countSprite; i++)
            {
                // Add Manual
                Sprite go = Resources.Load<Sprite>("PDF/PDF-" + j + "" + i) as Sprite;
                ListImageManualPDF.Add(go);
                ListImageManualPDF.Remove(null);
            }
        }
        
        for (int i = 0; i < countSprite; i++)
        {
            // Add Instruction
            Sprite go = Resources.Load<Sprite>("PDFInstruction/PDF-" + i) as Sprite;
            ListImageInsctructionPDF.Add(go);
            ListImageInsctructionPDF.Remove(null);
        }

        // Put in ContentManual
        for (int i = 0; i < ListImageManualPDF.Count; i++)
        {
            Image clone = Instantiate(InstanceIMG);
            clone.name = i.ToString();
            clone.transform.parent = ContentManual.transform;
            clone.GetComponent<Image>().sprite = ListImageManualPDF[i];
            clone.transform.localScale = new Vector3(1f, 1f, 1f);
            clone.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        // Put in ContentManual
        for (int i = 0; i < ListImageInsctructionPDF.Count; i++)
        {
            Image clone = Instantiate(InstanceIMG);
            clone.name = i.ToString();
            clone.transform.parent = ContentInstruction.transform;
            clone.GetComponent<Image>().sprite = ListImageInsctructionPDF[i];
            clone.transform.localScale = new Vector3(1f, 1f, 1f);
            clone.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        InstanceIMG.gameObject.SetActive(false);
    }
}
