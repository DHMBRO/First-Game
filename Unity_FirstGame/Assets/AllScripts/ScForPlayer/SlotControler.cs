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
    [SerializeField] public Transform[] Shops = new Transform[3];
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
    [SerializeField] private ShopControler[] ShopControlerToMyShops = new ShopControler[3];
    
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

                if (MyShope01) ShopControlerToMyShops[0] = MyShope01.gameObject.GetComponent<ShopControler>();
                if (MyShope02) ShopControlerToMyShops[1] = MyShope02.gameObject.GetComponent<ShopControler>();
                if (MyShope03) ShopControlerToMyShops[2] = MyShope03.gameObject.GetComponent<ShopControler>();

                if (ShopControlerToMyShops[0] && ShopControlerToMyShops[0].CaliberToShop != ControlerForShoot.CaliberToWeapon) ShopControlerToMyShops[0] = null;
                if (ShopControlerToMyShops[1] && ShopControlerToMyShops[1].CaliberToShop != ControlerForShoot.CaliberToWeapon) ShopControlerToMyShops[1] = null;
                if (ShopControlerToMyShops[2] && ShopControlerToMyShops[2].CaliberToShop != ControlerForShoot.CaliberToWeapon) ShopControlerToMyShops[2] = null;

                if (!ControlerForShoot.WeaponShoop)
                {
                    for (int i = 0; i < ShopControlerToMyShops.Length; i++) 
                    {
                        if(ShopControlerToMyShops[i] == null)
                        {
                            Debug.Log(ShopControlerToMyShops[i] == null);
                            
                        }
                        else if(ShopControlerToMyShops[i] != null)
                        {
                            Debug.Log(ShopControlerToMyShops[i] != null);


                        }
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

