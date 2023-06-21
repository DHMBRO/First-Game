using UnityEngine;
using System.Collections.Generic;

public class DropControler : MethodsFromDevelopers, IDrop
{
    [SerializeField] Transform PointForDrop;
    [SerializeField] GameObject ObjectToDrop;

    [SerializeField] SlotControler ControlerForSlots;
    [SerializeField] ShootControler ControlerForShoot;

    
    
    void Start()
    {
        ControlerForSlots = gameObject.GetComponent<SlotControler>();

    }

    void Update()
    {

        if (PointForDrop && ControlerForSlots.ObjectInHand && Input.GetKeyDown(KeyCode.Q))
        {
            ControlerForShoot = ControlerForSlots.ObjectInHand.GetComponent<ShootControler>();
            if (ControlerForShoot)
            {
                DropObjects(ControlerForSlots.ObjectInHand.transform, PointForDrop.transform);

                

                if (ControlerForSlots.MyWeapon01 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyWeapon01.gameObject)
                {
                    ControlerForSlots.MyWeapon01 = null;
                    DeleyReferenceShops();
                    Debug.Log("1");
                }

                if (ControlerForSlots.MyWeapon02 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyWeapon02.gameObject)
                {
                    ControlerForSlots.MyWeapon02 = null;
                    DeleyReferenceShops();
                }

                if (ControlerForSlots.MyPistol01 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyPistol01.gameObject)
                {
                    ControlerForSlots.MyPistol01 = null;
                    DeleyReferenceShops();
                }

                
                void DeleyReferenceShops()
                {
                    if (ControlerForShoot.WeaponShoop.transform == ControlerForSlots.MyShope01.transform) ControlerForSlots.MyShope01 = null;
                    else if (ControlerForShoot.WeaponShoop.transform == ControlerForSlots.MyShope02.transform) ControlerForSlots.MyShope02 = null;
                    else if (ControlerForShoot.WeaponShoop.transform == ControlerForSlots.MyShope03.transform) ControlerForSlots.MyShope03 = null;
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