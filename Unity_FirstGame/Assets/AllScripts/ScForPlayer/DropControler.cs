using UnityEngine;

public class DropControler : MethodsFromDevelopers, IDrop
{
    [SerializeField] Transform PointForDrop;
    [SerializeField] GameObject ObjectToDrop;

    [SerializeField] SlotControler ControlerForSlots;

    void Start()
    {
        ControlerForSlots = gameObject.GetComponent<SlotControler>();



    }

    void Update()
    {

        if (PointForDrop && ControlerForSlots.ObjectInHand && Input.GetKeyDown(KeyCode.Q))
        {
            if (!ControlerForSlots.ObjectInHand.CompareTag("Knife"))
            {
                DropObjects(ControlerForSlots.ObjectInHand.transform, PointForDrop.transform);

                if (ControlerForSlots.MyWeapon01 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyWeapon01.gameObject)
                {
                    ControlerForSlots.MyWeapon01 = null;
                    
                }

                if (ControlerForSlots.MyWeapon02 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyWeapon02.gameObject)
                {
                    ControlerForSlots.MyWeapon02 = null;
                    
                }

                if (ControlerForSlots.MyPistol01 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyPistol01.gameObject)
                {
                    ControlerForSlots.MyPistol01 = null;
                    
                }

            }
        }
    }

    public void Drop(string WhatWeaponDrop)
    {
        if (WhatWeaponDrop == "weapon01")
        {
            ControlerForSlots.MyWeapon01 = null;
            
        }

        if (WhatWeaponDrop == "weapon02")
        {
            ControlerForSlots.MyWeapon02 = null;
            
        }

        if (WhatWeaponDrop == "pistol")
        {
            ControlerForSlots.MyPistol01 = null;
            
        }
    }

}