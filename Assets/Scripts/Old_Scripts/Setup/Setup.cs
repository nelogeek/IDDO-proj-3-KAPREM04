using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    public static Setup instance;
    public GameObject FirstObject; // First object on Setup
    public Object LastObject; // Last object on Setup
    public Object PenultObject;

    public List<Object> objects;
    public Animator AnimatorSetup;

    private void Start()
    {
        // Initialize singelton
        instance = this;

        AnimatorSetup = this.GetComponent<Animator>();

        // Create setup
        objects = new List<Object>();
        AddObject(FirstObject.GetComponent<Object>());
        PenultObject = objects[0];
    }

    public bool AddObject(Object @object)
    {
        // if no injector last
        foreach (Object obj in objects)
        {
            if (obj.objectinfo.name == "Инжектор")
            {
                return false;
            }
        }

        // Add object to List
        objects.Add(@object);
        LastObject = objects[objects.Count - 1];

        if (objects.Count >= 2)
        {
            PenultObject = objects[objects.Count - 2];
            PenultObject.isCouldCarry = false;
        }


        // Set settings object
        //for (int i = 0; i < objects.Count - 1; i++)
        //{
        //    objects[i].isCouldCarry = false;
        //}

        //LastObject.isCouldCarry = true;
        @object.isOnSetup = true;

        return true;
    }

    public bool RemoveObject(Object @object)
    {
        // Remove from List
        objects.Remove(@object);
        PenultObject = objects[objects.Count - 1];

        // Set settings to object
        PenultObject.isCouldCarry = true;
        @object.isOnSetup = false;

        return true;
    }

    //public void AddObject(Object @object)
    //{
    //    // Add object to List
    //    objects.Add(@object);
    //    LastObject = objects[objects.Count - 1];

    //    // Set settings object
    //    for (int i = 0; i < objects.Count - 1; i++)
    //        objects[i].isCouldCarry = false;

    //    LastObject.isCouldCarry = true;
    //    @object.isOnSetup = true;
    //}

    //public void RemoveObject(Object @object)
    //{
    //    // Remove from List
    //    objects.Remove(@object);
    //    LastObject = objects[objects.Count - 1];

    //    // Set settings to object
    //    LastObject.isCouldCarry = true;
    //    @object.isOnSetup = false;
    //}

    public void PlayAnimtionCircle()
    {
        bool isOpen = AnimatorSetup.GetBool("isOpen");

        AnimatorSetup.SetBool("isOpen", !isOpen);
    }

}
