using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DropControler : MethodsFromDevelopers
{
    [SerializeField] public Transform PointForDrop;
    [SerializeField] GameObject ObjectToDrop;

    [SerializeField] SlotControler ControlerForSlots;
    [SerializeField] ShootControler ControlerForShoot;


    [SerializeField] public UiControler ControlerToUi;
    [SerializeField] Sprite None;

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
                    ControlerToUi.SlotWeapon01.sprite = None;
                    DeleyReferenceShops();
                    //Debug.Log("2");
                }

                if (ControlerForSlots.MyWeapon02 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyWeapon02.gameObject)
                {
                    ControlerForSlots.MyWeapon02 = null;
                    ControlerToUi.SlotWeapon02.sprite = None;
                    DeleyReferenceShops();
                }

                if (ControlerForSlots.MyPistol01 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyPistol01.gameObject)
                {
                    ControlerForSlots.MyPistol01 = null;
                    ControlerToUi.SlotPistol01.sprite = None;
                    DeleyReferenceShops();
                }

                
                void DeleyReferenceShops()
                {
                    if (ControlerForShoot.WeaponShoop)
                    {
                        if (ControlerForSlots.MyShope01 && ControlerForShoot.WeaponShoop.transform == ControlerForSlots.MyShope01.transform)
                        {
                            ControlerForSlots.MyShope01 = null;
                            ControlerToUi.SlotShop01.sprite = None;
                        }
                        else if (ControlerForSlots.MyShope02 && ControlerForShoot.WeaponShoop.transform == ControlerForSlots.MyShope02.transform)
                        {
                            ControlerForSlots.MyShope02 = null;
                            ControlerToUi.SlotShop02.sprite = None;
                        }
                        else if (ControlerForSlots.MyShope03 && ControlerForShoot.WeaponShoop.transform == ControlerForSlots.MyShope03.transform)
                        {
                            ControlerForSlots.MyShope03 = null;
                            ControlerToUi.SlotShop03.sprite = None;
                        }
                    }
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