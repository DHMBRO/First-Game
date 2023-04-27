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
        if(ControlerForSklots)
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
        Rigidbody RigidbodyObjectToDrop = ObjerctToDrop02.GetComponent<Rigidbody>();        
        PutObjects(ObjerctToDrop02, PointForDrop02);
        
        
        Debug.Log("Method 1 is work ");

        void SettingsRigidbody(Rigidbody ReferenceFroRigidbody)
        {                        
            ReferenceFroRigidbody.drag = 0.0f;
            ReferenceFroRigidbody.AngularDrag = 0.5f;
        
            ReferenceFroRigidbody.IsKinematic = false;
            ReferenceFroRigidbody.UseGravity = true;
            Debug.log("Method 2 is work");
        }
    }

}
