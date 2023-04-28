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
    [SerializeField] public Transform MyShope01;
    [SerializeField] public Transform MyShope02;
    [SerializeField] public Transform MyShope03;
    //
    [SerializeField] public Transform SlotShpo01;
    [SerializeField] public Transform SlotShpo02;
    [SerializeField] public Transform SlotShpo03;
    //
    [SerializeField] public Transform PointForShopPistol01;
    [SerializeField] public Transform PointForShopWeapon01;
    [SerializeField] public Transform PointForShopWeapon02;
    
    //All counet for work Script        
    protected int Counter;
    protected int CounetrForCharge;
    [SerializeField] private int SlotCounter;    
    //
    [SerializeField] public GameObject ObjectInHand = null;    
    

    //Fiset for other Scripts 
    [SerializeField] private PickUp PickUp;
    [SerializeField] private Inventory Inventory;
    //
    

    void Start()
    {
        PickUp = gameObject.GetComponent<PickUp>();
        Inventory = gameObject.GetComponent<Inventory>();

        AllSlots.Add("SlotShpo01", SlotShpo01);
        AllSlots.Add("SlotShpo02", SlotShpo02);
        AllSlots.Add("SlotShpo03", SlotShpo03);
        
        AllSlots.Add("SlotBack01", SlotBack01);
        AllSlots.Add("SlotBack02", SlotBack02);
        AllSlots.Add("SlotPistol01", SlotPistol01);
        AllSlots.Add("SlotKnife01", SlotKnife01);        
    }

    void Update()
    {
        
    }

    public void MovingGunForSlots()
    {
        AppropriationReferenceForUseShop();
        
        ChangingSlots();
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (MyPistol01 && ObjectInHand == MyPistol01.gameObject && MyPistol01.gameObject.tag == "Glok")
            {
                Charge(MyPistol01.gameObject, PointForShopPistol01, "Glok", "ShopGlok");
                if (CounetrForCharge == 0) ChangingShops(MyPistol01.gameObject, PointForShopPistol01, "Glok", "ShopGlok");
            }
            else if (MyWeapon01 && ObjectInHand == MyWeapon01.gameObject && MyWeapon01.gameObject.tag == "M4")
            {
                Charge(MyWeapon01.gameObject, PointForShopWeapon01, "M4", "ShopM4");                
                if(CounetrForCharge == 0) ChangingShops(MyWeapon01.gameObject, PointForShopWeapon01, "M4", "ShopM4" );            
            }
            else if (MyWeapon01 && ObjectInHand == MyWeapon01.gameObject && MyWeapon01.gameObject.tag == "AK47")
            {
                Charge(MyWeapon01.gameObject, PointForShopWeapon01, "AK47", "ShopAK47");
                if(CounetrForCharge == 0) ChangingShops(MyWeapon01.gameObject, PointForShopWeapon01, "M4", "ShopM4" );
            }            
            else if (MyWeapon02 && ObjectInHand == MyWeapon02.gameObject && MyWeapon02.gameObject.tag == "M4")
            {
                Charge(MyWeapon02.gameObject, PointForShopWeapon02, "M4", "ShopM4");                            
                if(CounetrForCharge == 0) ChangingShops(MyWeapon02.gameObject, PointForShopWeapon02, "M4", "ShopM4" );
            }
            
        }
                
        if (!MyWeapon01 && MyWeapon02)
        {
            MyWeapon01 = MyWeapon02;
            PutObjects(MyWeapon01, SlotBack01);

            MyWeapon02 = null;
        }
        else if (CounetrForCharge == 1 && Input.GetKeyUp(KeyCode.R))
        {
           CounetrForCharge = 0;
        }
        else if (Counter == 1 && Input.GetKeyUp("1"))
        {
            Counter = 0;
        }
    }

    void Charge(GameObject Weapon, Transform PointForRecharge, string MyWeapon, string MyShops)
    {
        ShopControler ShopControler01;
        ShopControler ShopControler02;
        ShopControler ShopControler03;

        ShootControler ReferenseForWeapon = Weapon.gameObject.GetComponent<ShootControler>();
        
        if (Weapon.tag == MyWeapon)
        {                                               
            if (Input.GetKeyDown(KeyCode.R) && ReferenseForWeapon)
            {                        
                if (MyShope01)
                {
                    ShopControler01 = MyShope01.GetComponent<ShopControler>();
                    
                    if (!ShopControler01.IsUsing && !ReferenseForWeapon.WeaponShoop && MyShope01 && MyShope01.gameObject.tag == MyShops && CounetrForCharge == 0)
                    {                                                
                        PutObjects(MyShope01, PointForRecharge);                        
                        ReferenseForWeapon.WeaponShoop = MyShope01.gameObject;
                        
                        Debug.Log(!ShopControler01.IsUsing);
                        CounetrForCharge = 1;                        
                    }                    
                }
                if(MyShope02)
                {
                    ShopControler02 = MyShope02.GetComponent<ShopControler>();
                    if (!ShopControler02.IsUsing && !ReferenseForWeapon.WeaponShoop && MyShope02 && MyShope02.gameObject.tag == MyShops && CounetrForCharge == 0)
                    {
                        PutObjects(MyShope02, PointForRecharge);
                        ReferenseForWeapon.WeaponShoop = MyShope02.gameObject;
                        
                        CounetrForCharge = 1;        
                    }
                    
                }
                if (MyShope03)
                {
                    ShopControler03 = MyShope03.GetComponent<ShopControler>();
                    if (!ShopControler03.IsUsing && !ReferenseForWeapon.WeaponShoop && MyShope03 && MyShope03.gameObject.tag == MyShops && CounetrForCharge == 0)
                    {
                        PutObjects(MyShope03, PointForRecharge);
                        ReferenseForWeapon.WeaponShoop = MyShope03.gameObject;
                        
                        CounetrForCharge = 1;                        
                    }

                }
                
             
            }
        }
        
    }
    
    void ChangingShops(GameObject Weapon, Transform PointForCharge, string MyWeaponTag, string MyShopTag)
    {
        
        ShopControler ShopControler01; 
        ShopControler ShopControler02;
        ShopControler ShopControler03;

        ShootControler ShootControlerWeapon = Weapon.gameObject.GetComponent<ShootControler>();

        if(Weapon.tag == MyWeaponTag)            
        {
            if(Input.GetKeyDown(KeyCode.R) && ShootControlerWeapon)
            {                                                                
                if (MyShope01 && ShootControlerWeapon.WeaponShoop == MyShope01.gameObject)
                {                                        
                    if (MyShope02)
                    {
                        ShopControler02 = MyShope02.gameObject.GetComponent<ShopControler>();
                        
                        if (ShopControler02 && ShopControler02 .CurrentAmmo > 0 && !ShopControler02.IsUsing && 
                        MyShope02.gameObject.tag == MyShopTag && CounetrForCharge == 0)
                        {
                            PutObjects(MyShope01, SlotShpo01);
                            PutObjects(MyShope02, PointForCharge);
    
                            ShootControlerWeapon.WeaponShoop = MyShope02.gameObject;
                            CounetrForCharge = 1;
                        }
                    }
                    else if (MyShope03)
                    {
                        ShopControler03 = MyShope03.gameObject.GetComponent<ShopControler>();
                        
                        if (!ShopControler03.IsUsing &&  ShopControler03.CurrentAmmo > 0  &&MyShope03.gameObject.tag == MyShopTag && CounetrForCharge == 0)
                        {
                            PutObjects(MyShope02, SlotShpo02);
                            PutObjects(MyShope03, PointForCharge);

                            ShootControlerWeapon.WeaponShoop = MyShope03.gameObject;
                            CounetrForCharge = 1;
                        }
                    }
                                        
                }

                
                if (MyShope02 && ShootControlerWeapon.WeaponShoop == MyShope02.gameObject)
                {                                                            
                    if (MyShope01)
                    {
                        ShopControler01 = MyShope01.gameObject.GetComponent<ShopControler>();
                        if (!ShopControler01.IsUsing && ShopControler01.CurrentAmmo > 0 && MyShope01.gameObject.tag == MyShopTag && CounetrForCharge == 0)
                        {
                            PutObjects(MyShope02, SlotShpo02);
                            PutObjects(MyShope01, PointForCharge);

                            ShootControlerWeapon.WeaponShoop = MyShope01.gameObject;
                            CounetrForCharge = 1;
                        }
                    }
                    else if (MyShope03)
                    {
                        ShopControler03 = MyShope03.gameObject.GetComponent<ShopControler>();
                        if (!ShopControler03.IsUsing && ShopControler03.CurrentAmmo > 0 && MyShope03.gameObject.tag == MyShopTag && CounetrForCharge == 0)
                        {
                            PutObjects(MyShope02, SlotShpo02);
                            PutObjects(MyShope03, PointForCharge);

                            ShootControlerWeapon.WeaponShoop = MyShope03.gameObject;
                            CounetrForCharge = 1;
                        }
                    }
                    
                }
                                
                if (MyShope03 && ShootControlerWeapon.WeaponShoop == MyShope03.gameObject)
                {                    
                    if (MyShope01)
                    {
                        ShopControler01 = MyShope01.gameObject.GetComponent<ShopControler>();
                        if (!ShopControler01.IsUsing && ShopControler01.CurrentAmmo > 0 && MyShope01.gameObject.tag == MyShopTag && CounetrForCharge == 0)
                        {
                            PutObjects(MyShope02, SlotShpo02);
                            PutObjects(MyShope01, PointForCharge);

                            ShootControlerWeapon.WeaponShoop = MyShope01.gameObject;
                            CounetrForCharge = 1;
                        }
                    }
                    else if (MyShope02)
                    {
                        ShopControler02 = MyShope02.gameObject.GetComponent<ShopControler>();
                        if (!ShopControler02.IsUsing && ShopControler02.CurrentAmmo > 0 &&MyShope02.gameObject.tag == MyShopTag && CounetrForCharge == 0)
                        {
                            PutObjects(MyShope01, SlotShpo01);
                            PutObjects(MyShope02, PointForCharge);

                            ShootControlerWeapon.WeaponShoop = MyShope02.gameObject;
                            CounetrForCharge = 1;
                        }
                    }
                    
                }
                
            }
        }        
        
        //void CompeltingTheLinkForShops()
        {
            
        }
    }
    
    void ChangingSlots()
    {
        
        bool ObjectInHand01 = MyKnife01 && ObjectInHand == MyKnife01.gameObject;
        bool ObjectInHand02 = MyPistol01 && ObjectInHand == MyPistol01.gameObject;
        bool ObjectInHand03 = MyWeapon01 && ObjectInHand == MyWeapon01.gameObject;
        bool ObjectInHand04 = MyWeapon02 && ObjectInHand == MyWeapon02.gameObject;

        bool InputSlots = Input.GetKeyDown("1");

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
                if (MyKnife01)
                {
                    PutObjects(MyKnife01, SlotHand);
                    Counter = 1;
                }                            
            }
            else if (MyWeapon01 && !MyWeapon02 && Counter == 0)
            {
                PutObjects(MyWeapon01, SlotBack01);
                ObjectInHand = null;
                if (MyKnife01)
                {
                    PutObjects(MyKnife01, SlotHand);
                    Counter = 1;
                }                
            }            
            else if (MyWeapon02 && ObjectInHand04 && Counter == 0)
            {
            
                PutObjects(MyWeapon02, SlotBack02);
                ObjectInHand = null;
                
                if (MyKnife01)
                {
                    PutObjects(MyKnife01, SlotHand);
                    Counter = 1;
                }               
            }
            

        }        
        
    }

    void AppropriationReferenceForUseShop()
    {
        ShootControler ShootControler;

        if (MyPistol01)
        {
            ShootControler = MyPistol01.GetComponent<ShootControler>();
            PointForShopPistol01 = ShootControler.SlotForUseShop;
        }
        if (MyWeapon01)
        {
            ShootControler = MyWeapon01.GetComponent<ShootControler>();
            PointForShopWeapon01 = ShootControler.SlotForUseShop;
        }
        if (MyWeapon02)
        {
            ShootControler = MyWeapon02.GetComponent<ShootControler>();
            PointForShopWeapon02 = ShootControler.SlotForUseShop;
        }

    }
}

