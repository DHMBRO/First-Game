using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class DropControler : MethodsFromDevelopers
{
    [SerializeField] public Transform PointForDrop;
    [SerializeField] GameObject ObjectToDrop;

    [SerializeField] SlotControler ControlerSlot;
    [SerializeField] ShootControler ControlerForShoot;
    [SerializeField] PlayerControler ControlerPlayer;

    [SerializeField] public UiControler ControlerToUi;
    [SerializeField] public Sprite None;

    void Start()
    {
        ControlerSlot = gameObject.GetComponent<SlotControler>();
    }

    public void UpdateInformation(GameObject DropedObject)
    {
        AimControler ControlerAim = null;

        if (ControlerSlot.ObjectInHand && ControlerSlot.ObjectInHand.name == DropedObject.name)
        {
            ControlerSlot.ObjectInHand = null;
        }
        
        ControlerAim = GetComponent<AimControler>();
        ControlerAim.UpdateWeapoMuzzle();
    
    }

    public void DropArmorVest()
    {
        if (ControlerSlot.Shop[0]) PutObjects(ControlerSlot.Shop[0], ControlerSlot.SlotsShop[0], false);
        if (ControlerSlot.Shop[1]) PutObjects(ControlerSlot.Shop[1], ControlerSlot.SlotsShop[1], false);
        if (ControlerSlot.Shop[2]) PutObjects(ControlerSlot.Shop[2], ControlerSlot.SlotsShop[2], false);

        if (ControlerSlot.MyPistol01) PutObjects(ControlerSlot.MyPistol01, ControlerSlot.SlotPistol01, false);
        PutObjects(ControlerSlot.MyKnife01, ControlerSlot.SlotKnife01, false);

    }

    public void Drop()
    {
        if (PointForDrop && ControlerSlot.ObjectInHand)
        {
            if (ControlerSlot.ObjectInHand) ControlerForShoot = ControlerSlot.ObjectInHand.GetComponent<ShootControler>();
            else ControlerForShoot = null;

            if (ControlerForShoot)
            {
                DropObjects(ControlerSlot.ObjectInHand.transform, PointForDrop.transform, false);

                if (ControlerSlot.MyWeapon01 && ControlerSlot.ObjectInHand.gameObject == ControlerSlot.MyWeapon01.gameObject)
                {
                    ControlerSlot.MyWeapon01 = null;
                    ControlerToUi.SlotWeapon01.sprite = None;                    
                    DeleyReferenceShops();
                }

                if (ControlerSlot.MyWeapon02 && ControlerSlot.ObjectInHand.gameObject == ControlerSlot.MyWeapon02.gameObject)
                {
                    ControlerSlot.MyWeapon02 = null;
                    ControlerToUi.SlotWeapon02.sprite = None;
                    DeleyReferenceShops();
                }

                if (ControlerSlot.MyPistol01 && ControlerSlot.ObjectInHand.gameObject == ControlerSlot.MyPistol01.gameObject)
                {
                    ControlerSlot.MyPistol01 = null;
                    ControlerToUi.SlotPistol01.sprite = None;
                    DeleyReferenceShops();
                }


                void DeleyReferenceShops()
                {
                    if (ControlerForShoot.WeaponShoop)
                    {
                        if (ControlerSlot.Shop[0] && ControlerForShoot.WeaponShoop.transform == ControlerSlot.Shop[0].transform)
                        {
                            ControlerSlot.Shop[0] = null;
                            ControlerToUi.SlotShop01.sprite = None;
                        }
                        else if (ControlerSlot.Shop[1] && ControlerForShoot.WeaponShoop.transform == ControlerSlot.Shop[1].transform)
                        {
                            ControlerSlot.Shop[1]  = null;
                            ControlerToUi.SlotShop02.sprite = None;
                        }
                        else if (ControlerSlot.Shop[2]  && ControlerForShoot.WeaponShoop.transform == ControlerSlot.Shop[2] .transform)
                        {
                            ControlerSlot.Shop[2]  = null;
                            ControlerToUi.SlotShop03.sprite = None;
                        }
                    }
                }

            }
        }
    }

}