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

        if (ControlerForSlots && ControlerForSlots.ObjectInHand)
        {
            DropObject = ControlerForSlots.ObjectInHand.gameObject;
        }
        
    }

    void Update()
    {
        if (ControlerForSlots && ControlerForSlots.ObjectInHand)
        {
            DropObject = ControlerForSlots.ObjectInHand.gameObject;
        }
        if (DropObject)
        {
            Rigidbody RigidbodyObject01 = DropObject.GetComponent<Rigidbody>();            

            if (DropObject)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (!RigidbodyObject01)
                    {
                        Rigidbody RigidbodyObject02 = DropObject.AddComponent<Rigidbody>();

                        RigidbodySettinbgs(RigidbodyObject02);
                        DropObjects(DropObject.transform, PointForDrop);
                        if (ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyPistol01.gameObject)
                        {
                            ControlerForSlots.MyPistol01 = null;
                        }
                        else if (ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyWeapon01.gameObject)
                        {
                            ControlerForSlots.MyWeapon01= null;
                        }
                        else if (ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyWeapon02.gameObject)
                        {
                            ControlerForSlots.MyWeapon02 = null;
                        }


                    }
                    else if (RigidbodyObject01)
                    {
                        RigidbodySettinbgs(RigidbodyObject01);
                        DropObjects(DropObject.transform, PointForDrop);
                    }
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
