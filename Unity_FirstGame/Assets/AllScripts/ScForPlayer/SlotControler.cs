using UnityEngine;
using System.Collections.Generic;

public class SlotControler : MethodsFromDevelopers
{    

    //All Slots
    [SerializeField] public Transform CurrentSlotHand;
    [SerializeField] public Transform SlotHandForUseLoot;
    [SerializeField] public Transform SlotHandForPistols;
    [SerializeField] public Transform SlotHandForWeapons;
    
    [SerializeField] public Transform ThatTimeSlot;
    [SerializeField] public Transform ObjectInThatTimeSlot;
    //
    [SerializeField] public Dictionary<string,Transform> AllSlots = new Dictionary<string,Transform>();
    //
    [SerializeField] public Transform SlotBack01;
    [SerializeField] public Transform SlotBack02;
    [SerializeField] public Transform AdditionalSlot01;
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
    
    [SerializeField] public Transform[] Shop = new Transform[3];
    //
    [SerializeField] public Transform[] SlotsShop = new Transform[3];
    //
    [SerializeField] public Transform PointForShopPistol01;
    [SerializeField] public Transform PointForShopWeapon01;
    [SerializeField] public Transform PointForShopWeapon02;
    
    //All counetrs for work Script        
    public int Counter;
    protected int CounetrForCharge;
    [SerializeField] protected int ShopCounter;
    //
    //References for other Component
    [SerializeField] private PlayerControler ControlerPlayer;
    [SerializeField] private PickUp PickUp;
    [SerializeField] private Inventory Inventory;
    //
    [SerializeField] List<Transform> Slots = new List<Transform>();
    //
    [SerializeField] public Transform ObjectInHand = null;
    [SerializeField] public Transform LastWeaponInHand = null;

    void Start()
    {
        //Geting Refrences to other components 
        ControlerPlayer = GetComponent<PlayerControler>();
        PickUp = gameObject.GetComponent<PickUp>();
        Inventory = gameObject.GetComponent<Inventory>();
        
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

    public void PutWeapon()
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
                    UpdateTypeWeaponInHand();
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
                    
                    UpdateTypeWeaponInHand();
                }
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

    public void UpWeapon()
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

    public void GetObjectInHand()
    {
        if (ObjectInHand) PutObjects(ObjectInHand.transform,CurrentSlotHand, false);
        
    }


    public void MovingGunForSlots()
    {
         
        List<ShopControler> ShopControler = new List<ShopControler>();
        List<ShopControler> ShopToCanUse = new List<ShopControler>();
        ShootControler ControlerForShoot;

        if (ObjectInHand && Input.GetKeyDown(KeyCode.R))
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
                    Recharge(ShopToCanUse[0].gameObject);                    
                }
                else if (ShopToCanUse.Count == 2)
                {
                    GameObject ShopToRecharge;

                    for (int i = 0;i < ShopToCanUse.Count; i++)
                    {
                        for (int j = 1;j < ShopToCanUse.Count; j++)
                        {
                            //Debug.Log("1");
                            if (i < ShopToCanUse.Count && ShopToCanUse[i].CurrentAmmo > ShopToCanUse[j].CurrentAmmo || ShopToCanUse[i].CurrentAmmo == ShopToCanUse[j].CurrentAmmo)
                            {
                                //Debug.Log("2");
                                ShopToRecharge = ShopToCanUse[i].gameObject;
                                Recharge(ShopToCanUse[i].gameObject);
                                return;
                            }
                            else if (j < ShopToCanUse.Count && ShopToCanUse[j].CurrentAmmo > ShopToCanUse[i].CurrentAmmo)
                            {
                                //Debug.Log("3");
                                ShopToRecharge = ShopToCanUse[j].gameObject;
                                Recharge(ShopToCanUse[j].gameObject);
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
                            //Debug.Log("4");
                            Recharge(ShopToCanUse[2].gameObject);
                            return;
                        }
                        else if(ShopToCanUse[2].CurrentAmmo < ShopToUse.CurrentAmmo)
                        {
                            //Debug.Log("5");
                            Recharge(ShopToUse.gameObject);
                            return;
                        }
                    }


                }

                void Recharge(GameObject ShopToRecharge)
                {
                    //Debug.Log("");
                    if (!ControlerForShoot.WeaponShoop)
                    {
                        RechargeShop(ShopToRecharge);
                        DeleyReferences(ShopToRecharge);
                    }
                    else if(ControlerForShoot.WeaponShoop)
                    {
                        ShopControler ShopFromWeapon;

                        //Debug.Log("1");
                        for (int i = 0;i < Shop.Length;i++)
                        {
                            if (Shop[i] != null && ShopToRecharge.transform.position == SlotsShop[i].position)
                            {
                                DisRechargeShop(SlotsShop[i]);
                                ShopFromWeapon = ControlerForShoot.WeaponShoop;
                                
                                for (int j = 0;j < Shop.Length;j++)
                                {
                                    if (Shop[j] != null && ShopToRecharge.transform.position == Shop[j].position)
                                    {
                                        Shop[j] = ShopFromWeapon.transform;
                                        RechargeShop(ShopToRecharge);
                                        Debug.Log(Shop[j]);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        return;
                    }
                    
                    void DeleyReferences(GameObject ShopToUse)
                    {
                        for (int i = 0;i < Shop.Length;i++)
                        {
                            if (Shop[i] != null && ShopToUse.transform.position == Shop[i].transform.position)
                            {
                                Shop[i] = null;
                            }
                        }


                    }


                    void RechargeShop(GameObject ShopToRecharge)
                    {
                        ControlerForShoot.WeaponShoop = ShopToRecharge.GetComponent<ShopControler>();
                        PutObjects(ControlerForShoot.WeaponShoop.transform, ControlerForShoot.SlotForUseShop, false);

                    }

                    void DisRechargeShop(Transform PointToShop)
                    {
                        PutObjects(ControlerForShoot.WeaponShoop.transform, PointToShop, false);
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
       
        
    }


    public void ChangingSlots()
    {
        //Rferences
        Dictionary<int, Transform> AllWeapons = new Dictionary<int, Transform>();
        Dictionary<string, Transform> AllSlotsForWeapon = new Dictionary<string, Transform>();
        
        Transform SelectedWeapon = null;
        Transform SlotHand = null;
        ShootControler ShootControlerWeapon = null;

        //Setup References
        int Count = 0;
        if (MyKnife01)
        {
            AllWeapons.Add(Count, MyKnife01.transform);
            AllSlotsForWeapon.Add(MyKnife01.name, SlotKnife01.transform);
        }
        if (MyPistol01)
        {
            Count++;
            AllWeapons.Add(Count, MyPistol01.transform);
            AllSlotsForWeapon.Add(MyPistol01.name, SlotPistol01.transform);
        }
        if (MyWeapon01) 
        {
            Count++;
            AllWeapons.Add(Count, MyWeapon01.transform);
            AllSlotsForWeapon.Add(MyWeapon01.name, SlotBack01.transform);
        }
        if (MyWeapon02) 
        {
            Count++;
            AllWeapons.Add(Count, MyWeapon02.transform);
            AllSlotsForWeapon.Add(MyWeapon02.name, SlotBack02.transform);
        }


        if (ObjectInHand)
        {
            for (int i = 0;i < AllWeapons.Count;i++)
            {
                if (AllWeapons[i].name == ObjectInHand.name)
                {
                    if (i == AllWeapons.Count - 1)
                    {
                        SelectedWeapon = AllWeapons[0];
                    }
                    else
                    {
                        SelectedWeapon = AllWeapons[i + 1];
                    }
                    break;
                }
            }

            RemoveWeapon();
            SelectSlotHand();
            ClotheWeapon();

        }
        else
        {
            SelectedWeapon = AllWeapons[0];
            SelectSlotHand();
            ClotheWeapon();
        }

        void SelectSlotHand()
        {
            ShootControlerWeapon = SelectedWeapon.GetComponent<ShootControler>();
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

        void ClotheWeapon()
        {
            PutObjects(SelectedWeapon, SlotHand, false);
            ObjectInHand = SelectedWeapon;
        }

        void RemoveWeapon()
        {
            PutObjects(ObjectInHand.transform, AllSlotsForWeapon[ObjectInHand.name], false);
        }

       
    }

}

