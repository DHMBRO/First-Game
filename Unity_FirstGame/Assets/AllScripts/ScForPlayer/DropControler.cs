using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropControler : MethodsFromDevelopers
{
    [SerializeField] Transform PointForDrop;
    [SerializeField] GameObject DropObject;
    
    [SerializeField] SlotControler ControlerForSlots;  

    void Start()
    {
        ControlerForSlots = gameObject.GetComponent<SlotControler>();

        if (ControlerForSlots)
        {
            DropObject = ControlerForSlots.ObjectInHand.gameObject;
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
                    Rigidbody RigidbodyObject02 = DropObject.GetComponent<Rigidbody>();
                
                    RigidbodySettinbgs(RigidbodyObject02);       
                    DropObjects(DropObject.transform, PointForDrop);  
                } 
                else if(RigidbodyObject01)
                {                
                    RigidbodySettinbgs(RigidbodyObject01);       
                    DropObjects(DropObject.transform, PointForDrop);                  
                }
            }

        }   
        
    }

    void RigidbodySettinbgs(Rigidbody RigidbodyObject)
    {
        RigidbodyObject.isKinematic= false;
        RigidbodyObject.useGravity = true;

        RigidbodyObject.angularDrag = 0.05f;
        RigidbodyObject.drag = 0.0f;

    }

}
