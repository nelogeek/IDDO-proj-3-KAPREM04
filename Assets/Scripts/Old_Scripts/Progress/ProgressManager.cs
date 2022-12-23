using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    struct Scheme
    {
        public string[] referenceScheme;
        public string[] buildedeScheme;
        public float mark { get; private set; }

        public void Setmark()
        {
            // Get all object on setup
            buildedeScheme = new string[Setup.instance.objects.Count - 1];

            // Check if isn't equal
            if (buildedeScheme.Length != referenceScheme.Length)
                return;

            for (int i = 0; i < buildedeScheme.Length; i++)
                buildedeScheme[i] = Setup.instance.objects[i + 1].objectinfo.name;

            // Comupte mark 
            mark = 0;
            for (int i = 0; i < referenceScheme.Length; i++)
            {
                if (referenceScheme[i].Equals(buildedeScheme[i]))
                {
                    mark += 1f / referenceScheme.Length;
                }
            }

            // Set percent
            mark *= 100;

            print("mark = " + mark);
        }
    }

    Scheme scheme1;
    Scheme scheme2;

    public Animator AnimatorTabPanel;
    public UnityEngine.UI.Text btnAnimText;

    // Start is called before the first frame update
    void Start()
    {
        scheme1.referenceScheme =  new string[] { "Комбинированный превентор",
                                                  "Универсальный превентор",
                                                  "Рейзер",
                                                  "Уплотнительное устройство с боковым люком",
                                                  "Инжектор"
                                                };

        scheme2.referenceScheme = new string[] { "Срезной глухой превентор",
                                                 "Универсальный превентор",
                                                 "Рейзер",
                                                 "Уплотнительное устройство с боковым люком",
                                                 "Инжектор"
                                                };

        Debug.developerConsoleVisible = true;
    }

    public void btnCheckScheme1()
    {
        scheme1.Setmark();
        Debug.LogError("Scheme 1 mark = " + scheme1.mark);
    }

    public void btnCheckScheme2()
    {
        scheme2.Setmark();
        Debug.LogError("Scheme 2 mark = " + scheme2.mark);
    }

    public void btnAnimation()
    {
        bool isOpen = AnimatorTabPanel.GetBool("isOpen");

        btnAnimText.text = isOpen ? ">" : "<";
        AnimatorTabPanel.SetBool("isOpen", !isOpen);
    }

    private void OnApplicationQuit()
    {
        float avgMark = (scheme1.mark + scheme2.mark) / 2f;

        // Write in file
        System.IO.File.WriteAllText(System.Environment.CurrentDirectory  + "//result.txt", avgMark.ToString());
    }
}
