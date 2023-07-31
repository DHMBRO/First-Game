using UnityEngine;
using System.Collections.Generic;

public class SlotControler : MethodsFromDevelopers
{    
    //All Slots
    [SerializeField] public Transform SlotHand;
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
    //
    [SerializeField] public Transform[] Shop = new Transform[3];
    //
    [SerializeField] public Transform[] SlotsShop = new Transform[3];
    //
    [SerializeField] public Transform PointForShopPistol01;
    [SerializeField] public Transform PointForShopWeapon01;
    [SerializeField] public Transform PointForShopWeapon02;
    
    //All counetrs for work Script        
    protected int Counter;
    protected int CounetrForCharge;
    [SerializeField] protected int ShopCounter;    
    //
    //References for other Component
    [SerializeField] private PickUp PickUp;
    [SerializeField] private Inventory Inventory;
    //
    
    //
    [SerializeField] List<Transform> Slots = new List<Transform>();
    //
    [SerializeField] public GameObject ObjectInHand = null;

    void Start()
    {
        PickUp = gameObject.GetComponent<PickUp>();
        Inventory = gameObject.GetComponent<Inventory>();

        AllSlots.Add("SlotsShop[0]", SlotsShop[0]);
        AllSlots.Add("SlotsShop[1]", SlotsShop[1]);
        AllSlots.Add("SlotsShop[2]", SlotsShop[2]);
        
        AllSlots.Add("SlotBack01", SlotBack01);
        AllSlots.Add("SlotBack02", SlotBack02);
        AllSlots.Add("SlotPistol01", SlotPistol01);
        AllSlots.Add("SlotKnife01", SlotKnife01); 
    }

    public void MovingGunForSlots()
    {
       
        ChangingSlots();
        
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
                        if (ShopControler[i].CaliberToShop == ControlerForShoot.CaliberToWeapon)
                        {
                            ShopToCanUse.Add(ShopControler[i]);
                        }
                    }
                    

                }
                Debug.Log(ShopToCanUse.Count);
                
                if (ShopToCanUse.Count == 0)
                {

                    return;
                }
                else if (ShopToCanUse.Count == 1)
                {
                    Recharge(ShopToCanUse[0].gameObject);
                    for (int i = 0; i < Shop.Length; i++)
                    {
                        if (Shop[i] != null)
                        {
                            for (int j = 0; j < ShopToCanUse.Count; j++)
                            {
                                if (Shop[i].transform.position == ShopToCanUse[j].transform.position)
                                {
                                    Shop[i] = null;
                                    ShopToCanUse.RemoveAt(j);
                                }
                            }
                        }
                    }
                }
                else if (ShopToCanUse.Count == 2)
                {
                    if (ShopToCanUse[0].CurrentAmmo > ShopToCanUse[1].CurrentAmmo || ShopToCanUse[0].CurrentAmmo == ShopToCanUse[1].CurrentAmmo)
                    {
                        Recharge(ShopToCanUse[0].gameObject);
                        DeleyReferences(0);
                    }
                    else if (ShopToCanUse[1].CurrentAmmo > ShopToCanUse[0].CurrentAmmo)
                    {
                        Recharge(ShopToCanUse[1].gameObject);
                        DeleyReferences(1);
                    }
                    
                    void DeleyReferences(int index)
                    {
                        for (int i = 0; i < Shop.Length; i++)
                        {
                            Debug.Log(ShopToCanUse.Count);
                            if (ShopToCanUse[index].transform.position == Shop[i].transform.position)
                            {
                                Shop[i] = null;
                                ShopToCanUse.RemoveAt(index);
                                break;
                            }
                        }
                    }


                }


                void Recharge(GameObject ShopToRecharge)
                {
                    if (!ControlerForShoot.WeaponShoop)
                    {
                        RechargeShop(ShopToRecharge);
                    }
                    else
                    {
                        GameObject ShopFromWeapon = ControlerForShoot.WeaponShoop;
                    
                        for (int i = 0;i < Shop.Length;i++)
                        {
                            if (Shop[i] != null && Shop[i].position == SlotsShop[i].position)
                            {
                                DisRechargeShop(SlotsShop[i]);
                                RechargeShop(ShopToRecharge);
                                for (int j = 0;j < Shop.Length;j++)
                                {
                                    if (ShopToRecharge.transform.position == Shop[i].position)
                                    {
                                        Shop[i] = ShopFromWeapon.transform;
                                    }
                                }
                            }
                        }

                    }
                    
                    void RechargeShop(GameObject ShopToRecharge)
                    {
                        ControlerForShoot.WeaponShoop = ShopToRecharge;
                        PutObjects(ControlerForShoot.WeaponShoop.transform, ControlerForShoot.SlotForUseShop);

                    }

                    void DisRechargeShop(Transform PointToShop)
                    {
                        PutObjects(ControlerForShoot.WeaponShoop.transform, PointToShop);

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
            PutObjects(MyWeapon01, SlotBack01);

            MyWeapon02 = null;
        }
       
        
    }

    

    


    void Charge(Transform ShopForCharge, Transform PointForRecharge, ShootControler ReferenseForWeapon)
    {
        PutObjects(ShopForCharge, PointForRecharge);
        ReferenseForWeapon.WeaponShoop = ShopForCharge.gameObject;
        CounetrForCharge = 1;
    }

    void DisChargingShops(Transform ShopForDisCharge, Transform SlotForChopDisCharge)
    {
        PutObjects(ShopForDisCharge, SlotForChopDisCharge);
        CounetrForCharge = 0;
    }

    void ChangingSlots()
    {
        
        bool ObjectInHand01 = MyKnife01 && ObjectInHand == MyKnife01.gameObject;
        bool ObjectInHand02 = MyPistol01 && ObjectInHand == MyPistol01.gameObject;
        bool ObjectInHand03 = MyWeapon01 && ObjectInHand == MyWeapon01.gameObject;
        bool ObjectInHand04 = MyWeapon02 && ObjectInHand == MyWeapon02.gameObject;

        bool InputSlots = Input.GetKeyDown("1");

        if (Counter == 1 && Input.GetKeyUp("1")) Counter = 0;
        
        if (SlotHand && InputSlots)
        {    
            if (MyKnife01 && ObjectInHand == null && Counter == 0)
            {                
                PutObjects(MyKnife01, SlotHand);
                
                ObjectInHand = MyKnife01.gameObject;               
                Counter = 1;                          
            }
            else if (MyKnife01 && SlotKnife01 && ObjectInHand01 && Counter == 0)
            {
                if (MyKnife01)
                {
                    PutObjects(MyKnife01, SlotKnife01);
                    ObjectInHand = null;
                }   
                if (MyPistol01)
                {
                    PutObjects(MyPistol01, SlotHand);
                    ObjectInHand = MyPistol01.gameObject;                    
                    Counter = 1;
                    
                }
                else if (!ObjectInHand02 && MyWeapon01)
                {
                    PutObjects(MyWeapon01, SlotHand);
                    ObjectInHand = MyWeapon01.gameObject;
                    Counter = 1;                    
                }
                else if (!ObjectInHand02 && !ObjectInHand03 && MyWeapon02)
                {
                    
                    PutObjects(MyWeapon02, SlotHand);
                    ObjectInHand = MyWeapon02.gameObject;
                    Counter = 1;                    
                }
                
            }
            else if (MyWeapon01 && MyPistol01 && SlotBack01 && ObjectInHand02 && Counter == 0)
            {
                
                if (ObjectInHand02 && MyPistol01)
                {
                    PutObjects(MyPistol01, SlotPistol01);
                    ObjectInHand = null;                    
                }
                if (!ObjectInHand && MyWeapon01 && Counter == 0)
                {
                    PutObjects(MyWeapon01, SlotHand);
                    ObjectInHand = MyWeapon01.gameObject;
                    Counter = 1;                    
                }                
            }                        
            else if (MyWeapon01 && MyWeapon02 && SlotBack01 && SlotBack02 && Counter == 0)
            {                
                if (MyWeapon01 && ObjectInHand03)
                {                    
                    PutObjects(MyWeapon01, SlotBack01);
                    ObjectInHand = null;                
                }
                if (MyWeapon02 && !ObjectInHand && Counter == 0)
                {
                    PutObjects(MyWeapon02, SlotHand);
                    ObjectInHand = MyWeapon02.gameObject;
                    Counter = 1;                
                }                
                
            }
            if (MyPistol01 && !MyWeapon01 && !MyWeapon02 && Counter == 0)
            {
                PutObjects(MyPistol01, SlotPistol01);
                ObjectInHand = null;
                                       
            }
            else if (MyWeapon01 && !MyWeapon02 && Counter == 0)
            {
                PutObjects(MyWeapon01, SlotBack01);
                ObjectInHand = null;
                          
            }            
            else if (MyWeapon02 && ObjectInHand04 && Counter == 0)
            {
                PutObjects(MyWeapon02, SlotBack02);
                ObjectInHand = null;   
            }
            
        }        
        
    }

   

        
}

