﻿using UnityEngine;
using System.Collections.Generic;

public class SlotControler : MethodsFromDevelopers
{    
    //All Slots
    [SerializeField] public Transform CurrentSlotHand;

    [SerializeField] public Transform ObjectInHand = null;
    [SerializeField] public Transform LastWeaponInHand = null;

    [SerializeField] public Transform SlotHandForUseLoot;
    [SerializeField] public Transform SlotHandForPistols;
    [SerializeField] public Transform SlotHandForWeapons;
    
    //
    //[SerializeField] private Transform ShoulderAnim;
    //[SerializeField] public Transform ThatTimeSlot;
    //[SerializeField] public Transform ObjectInThatTimeSlot;
    //
    [SerializeField] public Dictionary<string,Transform> AllSlots = new Dictionary<string,Transform>();
    //
    [SerializeField] public Transform SlotBack01;
    [SerializeField] public Transform SlotBack02;
    //
    [SerializeField] public Transform SlotPistol01;
    //
    [SerializeField] public Transform SlotKnife01;
    //Weapons for slots
    [SerializeField] public Transform MyWeapon01;
    [SerializeField] public Transform MyWeapon02;
    //    
    [SerializeField] public Transform MyPistol01;
    //
    [SerializeField] public Transform MyKnife01;
    //  
    [SerializeField] public Transform MyHelmet;
    [SerializeField] public Transform MyArmor;
    [SerializeField] public Transform MyBackPack;
    //
    [SerializeField] public Transform SlotHelmet;
    [SerializeField] public Transform SlotArmor;
    [SerializeField] public Transform SlotBackPack;
    //
    [SerializeField] public GetDamageScript HeadDamageScript;
    [SerializeField] public GetDamageScript BodyDamageScript;
    //
    [SerializeField] public Transform[] Shop = new Transform[3];
    //
    [SerializeField] public Transform[] SlotsShop = new Transform[3];
    //
    [SerializeField] public Transform PointForShopPistol01;
    [SerializeField] public Transform PointForShopWeapon01;
    [SerializeField] public Transform PointForShopWeapon02;
    
    //All counetrs for work Script        
    //public int Counter;
    protected int CounetrForCharge;
    [SerializeField] protected int ShopCounter;
    //
    //References for other Component
    [SerializeField] private PlayerControler ControlerPlayer;
    [SerializeField] private PickUp PickUp;
    [SerializeField] private Inventory Inventory;
    private UiControler ControlerUI;
    private DropControler ControlerDrop;
    //
    [SerializeField] List<Transform> Slots = new List<Transform>();
    //
    

    void Start()
    {
        //Setup
        ControlerPlayer = GetComponent<PlayerControler>();
        ControlerUI = ControlerPlayer.ControlerUi;
        PickUp = gameObject.GetComponent<PickUp>();
        Inventory = gameObject.GetComponent<Inventory>();
        ControlerDrop = GetComponent<DropControler>();
        
        //Use methods in strart
        UpdateTypeWeaponInHand();

        //Other
        AllSlots.Add("SlotsShop[0]", SlotsShop[0]);
        AllSlots.Add("SlotsShop[1]", SlotsShop[1]); 
        AllSlots.Add("SlotsShop[2]", SlotsShop[2]);
        
        AllSlots.Add("SlotBack01", SlotBack01);
        AllSlots.Add("SlotBack02", SlotBack02);
        AllSlots.Add("SlotPistol01", SlotPistol01);
        AllSlots.Add("SlotKnife01", SlotKnife01);

        if (MyWeapon01) PointForShopWeapon01 = MyWeapon01.GetComponent<ShootControler>().SlotForUseShop;
        if (MyWeapon02) PointForShopWeapon02 = MyWeapon02.GetComponent<ShootControler>().SlotForUseShop;
        if (MyPistol01) PointForShopPistol01 = MyPistol01.GetComponent<ShootControler>().SlotForUseShop;

    }
        
    public void PutAwayWeapon()
    {
        if (ObjectInHand)
        {
            ShootControler LocalShootControler = ObjectInHand.GetComponent<ShootControler>();
            if (LocalShootControler)
            {
                if (LocalShootControler.TheGun == TypeWeapon.Pistol)
                {
                    PutObjects(ObjectInHand.transform, SlotPistol01, false);
                    
                    LastWeaponInHand = ObjectInHand;
                    ObjectInHand = null;
                }
                else if (LocalShootControler.TheGun == TypeWeapon.Weapon)
                {
                    LastWeaponInHand = ObjectInHand;

                    if (ObjectInHand.name == MyWeapon01.name) 
                    {
                        PutObjects(ObjectInHand, SlotBack01, false);
                        ObjectInHand = null;
                    }
                    else
                    {
                        PutObjects(ObjectInHand, SlotBack02, false);
                        ObjectInHand = null;
                    }

                    
                }

                ControlerPlayer.ControlerShoot = null;
                UpdateTypeWeaponInHand();
            }
            else
            {
                ControlerPlayer.WhatPlayerHandsHave = HandsPlayerHave.Null;
                PutObjects(ObjectInHand.transform, SlotKnife01, false);

                LastWeaponInHand = ObjectInHand;
                ObjectInHand = null;
                UpdateTypeWeaponInHand();

            }
        }
        else return;
    }

    public void ReturnWeaponInHand()
    {
        if (LastWeaponInHand)
        {
            ShootControler LocalShootControler = LastWeaponInHand.GetComponent<ShootControler>();

            if (LocalShootControler)
            {
                switch (LocalShootControler.TheGun)
                {
                    case TypeWeapon.Weapon:
                        PutObjects(LastWeaponInHand, SlotHandForWeapons, false);
                        break;
                    default:
                        PutObjects(LastWeaponInHand, SlotHandForPistols, false);
                        break;
                }
            }
            else PutObjects(LastWeaponInHand, SlotHandForPistols, false);
            ObjectInHand = LastWeaponInHand;
            UpdateTypeWeaponInHand();
        }
    }

    public void UpdateTypeWeaponInHand()
    {
        if (!ObjectInHand)
        {
            ControlerPlayer.WhatPlayerHandsHave = HandsPlayerHave.Null;
            return;
        }
        ShootControler ShootControlerObjectInHand = ObjectInHand.GetComponent<ShootControler>();

        if (ShootControlerObjectInHand)
        {
            switch (ShootControlerObjectInHand.TheGun)
            {
                case TypeWeapon.Weapon:
                    ControlerPlayer.WhatPlayerHandsHave = HandsPlayerHave.Weapon;
                    CurrentSlotHand = SlotHandForWeapons;//! 
                    break;
                case TypeWeapon.Pistol:
                    ControlerPlayer.WhatPlayerHandsHave = HandsPlayerHave.Pistol;
                    CurrentSlotHand = SlotHandForPistols;//!
                    break;
            }

        }
        else
        {
            ControlerPlayer.WhatPlayerHandsHave = HandsPlayerHave.Null; 
        }
    }

    public void PutObjectInHand()
    {
        if (!ObjectInHand) return;
        Transform TargetSlot = null;
        
        if (MyWeapon01 && ObjectInHand.transform.position == MyWeapon01.position) TargetSlot = SlotBack01;
        if (MyWeapon02 && ObjectInHand.transform.position == MyWeapon02.position) TargetSlot = SlotBack02;
        if (MyPistol01 && ObjectInHand.transform.position == MyPistol01.position) TargetSlot = SlotPistol01;
        if (MyKnife01 && ObjectInHand.transform.position == MyKnife01.position) TargetSlot = SlotKnife01;

        if (TargetSlot) PutObjects(ObjectInHand.transform, TargetSlot, false);

    }

    private void UpdateImageOnUI()
    {
        for (int i = 0; i < ControlerUI.SlotShopUi.Length; i++)
        {
            if (Shop[i])
            {
                ControlerUI.SlotShopUi[i].sprite = Shop[i].GetComponent<ScrForAllLoot>().SpriteForLoot;
            }
            else
            {
                ControlerUI.SlotShopUi[i].sprite = ControlerDrop.None;
            }
        }   
    }

    public void GetObjectInHand()
    {
        if (ObjectInHand) PutObjects(ObjectInHand.transform, CurrentSlotHand, false);
        
    }


    public void Recharge()
    {
         
        List<ShopControler> ShopControler = new List<ShopControler>();
        List<ShopControler> ShopToCanUse = new List<ShopControler>();
        ShootControler ControlerForShoot;

        if (ObjectInHand)
        {
            ControlerForShoot = ObjectInHand.gameObject.GetComponent<ShootControler>();
            if (ControlerForShoot)
            {
                for (int i = 0;i < Shop.Length;i++)
                {
                    if (Shop[i] != null)
                    {
                        ShopControler.Add(Shop[i].GetComponent<ShopControler>());
                        //Debug.Log(Shop[i].name + " i: " + i);
                        
                    }
                }

                for (int i = 0;i < ShopControler.Count;i++)
                {
                    if (ShopControler[i] != null && ShopControler[i].CaliberToShop == ControlerForShoot.CaliberToWeapon)
                    {
                        ShopToCanUse.Add(ShopControler[i]);
                        //Debug.Log("ShopToCanUse.Count: " + ShopToCanUse.Count);
                    }
                }

                if (ShopToCanUse.Count == 0)
                {

                    return;
                }
                else if (ShopToCanUse.Count == 1)
                {
                    Reload(ShopToCanUse[0]);                    
                }
                else if (ShopToCanUse.Count == 2)
                {
                    GameObject ShopToRecharge;

                    for (int i = 0;i < ShopToCanUse.Count; i++)
                    {
                        for (int j = 1;j < ShopToCanUse.Count; j++)
                        {
                            if (i < ShopToCanUse.Count && ShopToCanUse[i].CurrentAmmo > ShopToCanUse[j].CurrentAmmo || ShopToCanUse[i].CurrentAmmo == ShopToCanUse[j].CurrentAmmo)
                            {
                                ShopToRecharge = ShopToCanUse[i].gameObject;
                                Reload(ShopToCanUse[i]);
                                return;
                            }
                            else if (j < ShopToCanUse.Count && ShopToCanUse[j].CurrentAmmo > ShopToCanUse[i].CurrentAmmo)
                            {
                                ShopToRecharge = ShopToCanUse[j].gameObject;
                                Reload(ShopToCanUse[j]);
                                return;
                            }
                            
                            Debug.Log(ShopToCanUse[0].CurrentAmmo > ShopToCanUse[1].CurrentAmmo);
                        }
                    }
                }
                else if (ShopToCanUse.Count == 3)
                {
                    if (ShopToCanUse[0].CurrentAmmo > ShopToCanUse[1].CurrentAmmo || ShopToCanUse[0].CurrentAmmo == ShopToCanUse[1].CurrentAmmo)
                    {
                        Apropriation(ShopToCanUse[0]);
                    }
                    else if (ShopToCanUse[1].CurrentAmmo > ShopToCanUse[0].CurrentAmmo)
                    {
                        Apropriation(ShopToCanUse[1]);
                    }

                    void Apropriation(ShopControler ShopToUse)
                    {
                        if (ShopToCanUse[2].CurrentAmmo > ShopToUse.CurrentAmmo || ShopToCanUse[2].CurrentAmmo == ShopToUse.CurrentAmmo)
                        {
                            Reload(ShopToCanUse[2]);
                            return;
                        }
                        else if(ShopToCanUse[2].CurrentAmmo < ShopToUse.CurrentAmmo)
                        {
                            Reload(ShopToUse);
                            return;
                        }
                    }


                }

                void Reload(ShopControler ShopToRecharge)
                {
                    if (!ControlerForShoot.WeaponShoop)
                    {
                        ReloadShop(ShopToRecharge.GetComponent<ShopControler>());
                        DeleyReferences(ShopToRecharge.transform);
                    }
                    else if(ControlerForShoot.WeaponShoop)
                    {
                        ShopControler ShopFromWeapon;

                        for (int i = 0;i < Shop.Length;i++)
                        {
                            if (Shop[i] != null && ShopToRecharge.transform.position == SlotsShop[i].position)
                            {
                                DisReloadShop(SlotsShop[i].transform);
                                ShopFromWeapon = ControlerForShoot.WeaponShoop;
                                
                                for (int j = 0;j < Shop.Length;j++)
                                {
                                    if (Shop[j] != null && ShopToRecharge.transform.position == Shop[j].position)
                                    {
                                        Shop[j] = ShopFromWeapon.transform;
                                        ReloadShop(ShopToRecharge);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        return;
                    }
                    
                    void DeleyReferences(Transform ShopToUse)
                    {
                        for (int i = 0;i < Shop.Length;i++)
                        {
                            if (Shop[i] != null && ShopToUse.transform.position == Shop[i].transform.position)
                            {
                                Shop[i] = null;
                            }
                        }


                    }


                    void ReloadShop(ShopControler ShopToRecharge)
                    {
                        ControlerForShoot.WeaponShoop = ShopToRecharge;
                        PutObjects(ControlerForShoot.WeaponShoop.transform, ControlerForShoot.SlotForUseShop, false);

                    }

                    void DisReloadShop(Transform PointToShop)
                    {
                        PutObjects(ControlerForShoot.WeaponShoop.transform, PointToShop.transform, false);
                    }

                   

                }


            }
        }
        else if (CounetrForCharge == 1 && Input.GetKeyUp(KeyCode.R))
        {
            CounetrForCharge = 0;
        }
        
        if (!MyWeapon01 && MyWeapon02)
        {
            MyWeapon01 = MyWeapon02;
            PutObjects(MyWeapon01, SlotBack01, false);

            MyWeapon02 = null;
        }

        UpdateImageOnUI();
    }


    public void ChangingSlots()
    {
        //Rferences
        //Dictionary<int, Transform> AllWeapons = new Dictionary<int, Transform>();
        Dictionary<string, Transform> AllSlotsForWeapon = new Dictionary<string, Transform>();
        
        Transform SelectedWeapon = null;
        Transform SlotHand = null;
        ShootControler ShootControlerWeapon = null;

        //Setup References

        //int Count = 0;

        /*
        if (MyKnife01)
        {
            AllWeapons.Add(Count, MyKnife01.transform);
            AllSlotsForWeapon.Add(MyKnife01.name, SlotKnife01.transform);
        }
        */

        if (MyWeapon01 && !AllSlotsForWeapon.ContainsKey(MyWeapon01.name))
        {
            if(Input.GetKeyDown("1")) SelectedWeapon = MyWeapon01;
            AllSlotsForWeapon.Add(MyWeapon01.name, SlotBack01);
        }
        if (MyWeapon02 && !AllSlotsForWeapon.ContainsKey(MyWeapon02.name)) 
        {
            if(Input.GetKeyDown("2")) SelectedWeapon = MyWeapon02;
            AllSlotsForWeapon.Add(MyWeapon02.name, SlotBack02);
        }
        if (MyPistol01 && !AllSlotsForWeapon.ContainsKey(MyPistol01.name))
        {
            if(Input.GetKeyDown("3")) SelectedWeapon = MyPistol01;
            AllSlotsForWeapon.Add(MyPistol01.name, SlotPistol01);
        }
        
        if (Input.GetKeyDown("`"))
        {
            RemoveWeapon();
        }


        if (SelectedWeapon)
        {
            ShootControlerWeapon = SelectedWeapon.GetComponent<ShootControler>();
        }
        else return;

        
        if (ObjectInHand)
        {   
            RemoveWeapon();
            SelectSlotHand();
            PutWeapon();
        }
        else 
        {
            SelectSlotHand();
            PutWeapon();
        }

        void SelectSlotHand()
        {
            if (ShootControlerWeapon)
            {
                switch (ShootControlerWeapon.TheGun)
                {
                    case TypeWeapon.Weapon:
                        SlotHand = SlotHandForWeapons;
                        break;
                    default:
                        SlotHand = SlotHandForPistols;
                        break;
                }
            }
            else SlotHand = SlotHandForPistols;
        }

        void PutWeapon()
        {
            PutObjects(SelectedWeapon, SlotHand, false);

            if (ShootControlerWeapon && ShootControlerWeapon.UseShoulderOffSet)
            {
                SelectedWeapon.localPosition += ShootControlerWeapon.HandOffSet;
            }
            
            ObjectInHand = SelectedWeapon;
        }

        void RemoveWeapon()
        {
            if (!ObjectInHand) return;
            
            PutObjects(ObjectInHand.transform, AllSlotsForWeapon[ObjectInHand.name], false);
            ObjectInHand = null;
            ControlerPlayer.ControlerShoot = null;
        }

       
    }

}

