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
        Rigidbody RigidbodyObject01 = DropObject.GetComponent<Rigidbody>();
        

        if (DropObject)
        {                        
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(!RigidbodyObject01)
                {
                    Rigidbody RigidbodyObject02 = DropObjerct.GetComponent<Rigidbody>();
                
                    RigidbodySettinbgs(RigidbodyObject02);       
                    DropObjects(DropObjerct.transform PointForDrop);  
                } 
                else if(RigidbodyObject01)
                {                
                    RigidbodySettinbgs(RigidbodyObject01);       
                    DropObjects(DropObjerct.transform PointForDrop);                  
                }
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
