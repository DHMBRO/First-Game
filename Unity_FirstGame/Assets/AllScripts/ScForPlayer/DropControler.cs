using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropControler : MethodsFromDevelopers
{
    [SerializeField] Transform PointForDrop;
    [SerializeField] GameObject ObjerctToDrop;
    
    [SerializeField] SlotControler ControlerForSklots;  

    void Start()
    {
         ControlerForSklots = gameObject.GetComponent<SlotControler>();

        if (ControlerForSklots)
        {
            ObjerctToDrop = ControlerForSklots.ObjectInHand.gameObject;
        }
        
    }

    void Update()
    {        
        if(ControlerForSklots)
        {
            ObjerctToDrop = ControlerForSklots.ObjectInHand.gameObject;

            if(ObjerctToDrop && PointForDrop)
            {
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    Drop(ObjerctToDrop, PointForDrop);
                    Debug.Log("If is work");
                }
                else Debug.Log("If isnt work");
            }
        }        
    }

    void Drop(GameObject ObjerctToDrop02, Transform PointForDrop02)
    {
        Rigidbody Rigidbody = ObjerctToDrop02.GetComponent<Rigidbody>();
        Vector3 Drop = new Vector3(0.0f, 0.0f, 0.0f + 10.0f);
        if (!Rigidbody)
        {
            Rigidbody RigidbodyObjectToDrop = ObjerctToDrop02.AddComponent<Rigidbody>();
            
            PutObjects(ObjerctToDrop02.transform, PointForDrop02);
            RigidbodyObjectToDrop.AddRelativeForce(Drop, ForceMode.Force);

            SettingsRigidbody(RigidbodyObjectToDrop);
            Debug.Log("Method 1 is work ");

        }
        else
        {
            PutObjects(ObjerctToDrop02.transform, PointForDrop02);
            Rigidbody.AddRelativeForce(Drop, ForceMode.Force);

            SettingsRigidbody(Rigidbody);
            Debug.Log("Method 1 is work ");

        }

        void SettingsRigidbody(Rigidbody ReferenceFroRigidbody)
        {                        
            ReferenceFroRigidbody.drag = 0.0f;
            ReferenceFroRigidbody.angularDrag= 0.5f;
        
            ReferenceFroRigidbody.isKinematic = false;
            ReferenceFroRigidbody.useGravity = true;
            Debug.Log("Method 2 is work");
        }
    }

}
