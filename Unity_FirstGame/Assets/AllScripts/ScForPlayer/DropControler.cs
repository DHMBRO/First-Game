using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropControler : MethodsFromDevelopers
{
    [SerializeField] Transform PointForDrop;
    [SerializeField] GameObject DropObjerct;
    
    [SerializeField] SlotControler ControlerForSlots;  

    void Start()
    {
        ControlerForSlots = gameObject.GetComponent<SlotControler>();

        if (ControlerForSlots)
        {
            DropObjerct = ControlerForSlots.ObjectInHand.gameObject;
        }
        
    }

    void Update()
    {
        if (DropObjerct)
        {            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Rigidbody RigidbodyObject = DropObjerct.GetComponent<Rigidbody>();
                RigidbodySettinbgs(RigidbodyObject);       
                DropObjects(DropObjerct.transform PointForDrop);   
            }

        }
        
    }

    void RigidbodySettinbgs(Rigidbody RigidbodyObject)
    {
        RigidbodyObject.isKinematic = false;
        RigidbodyObject.useGrsvity = true;

        RigidbodyObject.angularDrag = 0.05f;
        RigidbodyObject.Drag = 0.0f;

    }

}
