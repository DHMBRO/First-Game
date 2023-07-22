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
    [SerializeField] private ShopControler ShopControlerToMyShop01;
    [SerializeField] private ShopControler ShopControlerToMyShop02;
    [SerializeField] private ShopControler ShopControlerToMyShop03;
    //
    [SerializeField] public GameObject ObjectInHand = null;

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

    public void MovingGunForSlots()
    {
       
        ChangingSlots();

        ShootControler ControlerForShoot;

        
        if (ObjectInHand && ObjectInHand.tag != "Knife" && Input.GetKeyDown(KeyCode.R))
        {
            ControlerForShoot = ObjectInHand.gameObject.GetComponent<ShootControler>();


            if (ControlerForShoot)
            {

                if (MyShope01) ShopControlerToMyShop01 = MyShope01.gameObject.GetComponent<ShopControler>();
                if (MyShope02) ShopControlerToMyShop02 = MyShope02.gameObject.GetComponent<ShopControler>();
                if (MyShope03) ShopControlerToMyShop03 = MyShope03.gameObject.GetComponent<ShopControler>();

                if (ShopControlerToMyShop01 && ShopControlerToMyShop01.CaliberToShop != ControlerForShoot.CaliberToWeapon) ShopControlerToMyShop01 = null;
                if (ShopControlerToMyShop02 && ShopControlerToMyShop02.CaliberToShop != ControlerForShoot.CaliberToWeapon) ShopControlerToMyShop02 = null;
                if (ShopControlerToMyShop03 && ShopControlerToMyShop03.CaliberToShop != ControlerForShoot.CaliberToWeapon) ShopControlerToMyShop03 = null;

                //
                if (ShopControlerToMyShop01 && !ShopControlerToMyShop02 && !ShopControlerToMyShop03)
                {
                    if (CounetrForCharge == 0) LoadShop01();
                }//+
                else if (!ShopControlerToMyShop01 && ShopControlerToMyShop02 && !ShopControlerToMyShop03)
                {
                    if (CounetrForCharge == 0) LoadShop02();
                }//+
                else if (!ShopControlerToMyShop01 && !ShopControlerToMyShop02 && ShopControlerToMyShop03)
                {
                    if (CounetrForCharge == 0) LoadShop03();
                }//+

                //
                else if (ShopControlerToMyShop01 && ShopControlerToMyShop02 && !ShopControlerToMyShop03)
                {

                    if (CounetrForCharge == 0 && !ShopControlerToMyShop01.IsUsing) LoadShop01();
                    else if (ShopCounter == 1 && CounetrForCharge == 0) LoadShop02();
                    else if (ShopCounter == 2 && CounetrForCharge == 0) LoadShop01();
                }//+ 
                else if (!ShopControlerToMyShop01 && ShopControlerToMyShop02 && ShopControlerToMyShop03)
                {
                    if (CounetrForCharge == 0 && !ShopControlerToMyShop02.IsUsing) LoadShop02();
                    else if (ShopCounter == 2 && CounetrForCharge == 0) LoadShop03();
                    else if (ShopCounter == 3 && CounetrForCharge == 0) LoadShop02();
                }///+
                else if (ShopControlerToMyShop01 && !ShopControlerToMyShop02 && ShopControlerToMyShop03)
                {
                    if (CounetrForCharge == 0 && !ShopControlerToMyShop01.IsUsing) LoadShop01();
                    else if (ShopCounter == 1 && CounetrForCharge == 0) LoadShop03();
                    else if (ShopCounter == 3 && CounetrForCharge == 0) LoadShop01();
                    Debug.Log("1");
                }///+
                //
                else if (ShopControlerToMyShop01 && ShopControlerToMyShop02 && ShopControlerToMyShop03)
                {
                    if (ShopCounter == 0 && CounetrForCharge == 0) LoadShop01();
                    else if (ShopCounter == 1 && CounetrForCharge == 0) LoadShop02();
                    else if (ShopCounter == 2 && CounetrForCharge == 0) LoadShop03();
                    else if (ShopCounter == 3 && CounetrForCharge == 0) LoadShop01();

                }//+

                void LoadShop01()
                {

                    if (!ShopControlerToMyShop01.IsUsing && !ControlerForShoot.WeaponShoop)
                    {
                        Charge(MyShope01, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                        ShopCounter = 1;
                    }
                    else if (ShopControlerToMyShop02 && ShopControlerToMyShop02.IsUsing && ControlerForShoot.WeaponShoop && ControlerForShoot.WeaponShoop.transform.localPosition == MyShope02.localPosition)
                    {
                        DisChargingShops(MyShope02, SlotShpo02);
                        Charge(MyShope01, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                        ShopCounter = 1;
                    }
                    else if (ShopControlerToMyShop03 && ShopControlerToMyShop03.IsUsing && ControlerForShoot.WeaponShoop && ControlerForShoot.WeaponShoop.transform.localPosition == MyShope03.localPosition)
                    {
                        DisChargingShops(MyShope03, SlotShpo03);
                        Charge(MyShope01, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                        ShopCounter = 1;
                    }
                }

                void LoadShop02()
                {
                    if (!ShopControlerToMyShop02.IsUsing && !ControlerForShoot.WeaponShoop)
                    {
                        Charge(MyShope02, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                        ShopCounter = 2;
                    }
                    else if (ShopControlerToMyShop01 && ShopControlerToMyShop01.IsUsing && ControlerForShoot.WeaponShoop && ControlerForShoot.WeaponShoop.transform.localPosition == MyShope01.localPosition)
                    {
                        DisChargingShops(MyShope01, SlotShpo01);
                        Charge(MyShope02, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                        ShopCounter = 2;
                    }
                    else if (ShopControlerToMyShop03 && ShopControlerToMyShop03.IsUsing && ControlerForShoot.WeaponShoop && ControlerForShoot.WeaponShoop.transform.localPosition == MyShope03.localPosition)
                    {
                        DisChargingShops(MyShope03, SlotShpo03);
                        Charge(MyShope02, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                        ShopCounter = 2;
                    }
                }

                void LoadShop03()
                {
                    if (!ShopControlerToMyShop03.IsUsing && !ControlerForShoot.WeaponShoop)
                    {
                        Charge(MyShope03, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                        ShopCounter = 3;
                    }
                    else if (ShopControlerToMyShop02 && ShopControlerToMyShop02.IsUsing && ControlerForShoot.WeaponShoop && ControlerForShoot.WeaponShoop.transform.localPosition == MyShope02.localPosition)
                    {
                        DisChargingShops(MyShope02, SlotShpo02);
                        Charge(MyShope03, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                        ShopCounter = 3;
                    }
                    else if (ShopControlerToMyShop01 && ShopControlerToMyShop01.IsUsing && ControlerForShoot.WeaponShoop && ControlerForShoot.WeaponShoop.transform.localPosition == MyShope01.localPosition)
                    {
                        DisChargingShops(MyShope01, SlotShpo01);
                        Charge(MyShope03, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                        ShopCounter = 3;
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

