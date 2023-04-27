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
        if (ObjerctToDrop)
        {
            //if (Input.GetKeyDown(KeyCode.Q))
            {

            }
        }
        
    }

    void Drop(GameObject ObjerctToDrop02, Transform PointForDrop02)
    {
       
    }

}
