using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DropControler : MethodsFromDevelopers
{
    [SerializeField] public Transform PointForDrop;
    [SerializeField] GameObject ObjectToDrop;

    [SerializeField] SlotControler ControlerForSlots;
    [SerializeField] ShootControler ControlerForShoot;
    [SerializeField] PlayerControler ControlerPlayer;

    [SerializeField] public UiControler ControlerToUi;
    [SerializeField] Sprite None;

    void Start()
    {
        ControlerForSlots = gameObject.GetComponent<SlotControler>();

    }

    
    public void Drop()
    {
        //Debug.Log("1");
        if (PointForDrop && ControlerForSlots.ObjectInHand)
        {
            //Debug.Log("2");
            if (ControlerForSlots.ObjectInHand) ControlerForShoot = ControlerForSlots.ObjectInHand.GetComponent<ShootControler>();
            else ControlerForShoot = null;

            if (ControlerForShoot)
            {
                //Debug.Log("3");
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
                        if (ControlerForSlots.Shop[0] && ControlerForShoot.WeaponShoop.transform == ControlerForSlots.Shop[0].transform)
                        {
                            ControlerForSlots.Shop[0] = null;
                            ControlerToUi.SlotShop01.sprite = None;
                        }
                        else if (ControlerForSlots.Shop[1] && ControlerForShoot.WeaponShoop.transform == ControlerForSlots.Shop[1].transform)
                        {
                            ControlerForSlots.Shop[1]  = null;
                            ControlerToUi.SlotShop02.sprite = None;
                        }
                        else if (ControlerForSlots.Shop[2]  && ControlerForShoot.WeaponShoop.transform == ControlerForSlots.Shop[2] .transform)
                        {
                            ControlerForSlots.Shop[2]  = null;
                            ControlerToUi.SlotShop03.sprite = None;
                        }
                    }
                }

            }
        }
    }

}