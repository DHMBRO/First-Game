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
    [SerializeField] private int SlotCounter;    
    //
    //References for other Component
    [SerializeField] private PickUp PickUp;
    [SerializeField] private Inventory Inventory;
    //
    [SerializeField] Sprite Sakr47;
    [SerializeField] Sprite Sm4;
    [SerializeField] Sprite Sglock;
    [SerializeField] Sprite Sknife;
    [SerializeField] GameObject weapon01;
    [SerializeField] GameObject weapon02;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject knife;
    //
    [SerializeField] List<Transform> Slots = new List<Transform>();
    //
    [SerializeField] private List<string> ListForAllWeapon = new List<string>();
    
    [SerializeField] private List<string> ListForAllShop = new List<string>();
    [SerializeField] private Dictionary<string, string> DictionaryForAllShop = new Dictionary<string, string>(); 

    [SerializeField] private List<ShopControler> ListForAllContrShop = new List<ShopControler>();
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
        //
        //
        //
        for (int i = 0;i < 5;i++)
        {
            DictionaryForAllShop.Add(ListForAllWeapon[i], ListForAllShop[i]);
            Debug.Log("ListForAllWeapon[i]: " + ListForAllWeapon[i] + " Dictionary: " + DictionaryForAllShop[ListForAllWeapon[i]]);
        }

    }

    void Update()
    {
        
        
    }

    public void MovingGunForSlots()
    {
        AppropriationReferenceForUseShop();
        
        ChangingSlots();


        if (ObjectInHand && Input.GetKeyDown(KeyCode.R))
        {

            if (MyShope01) ListForAllContrShop.Add(MyShope01.GetComponent<ShopControler>());
            if (MyShope02) ListForAllContrShop.Add(MyShope02.GetComponent<ShopControler>());
            if (MyShope03) ListForAllContrShop.Add(MyShope03.GetComponent<ShopControler>());
            
            ShootControler ControlerForShoot;
            
            ControlerForShoot = ObjectInHand.gameObject.GetComponent<ShootControler>();

            if (ControlerForShoot)
            {
                for (int i = 0; i < ListForAllWeapon.Count; i++)
                {
                    for (int j = 0; j < ListForAllShop.Count; j++)
                    {
                        if (ObjectInHand.gameObject.tag == ListForAllWeapon[i])
                        {
                            
                            if (MyShope01.gameObject.tag == DictionaryForAllShop[ListForAllWeapon[i]] && !ListForAllContrShop[0].IsUsing)
                            {
                                if (ControlerForShoot.SlotForUseShop == MyShope02 && ListForAllContrShop[2])
                                {
                                    DisChargingShops(MyShope02, SlotShpo02);
                                    Charge(MyShope01, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                                }
                                else if (ControlerForShoot.SlotForUseShop == MyShope03 && ListForAllContrShop[3])
                                {
                                    DisChargingShops(MyShope02, SlotShpo02);
                                    Charge(MyShope01, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                                }
                                else
                                {
                                    Charge(MyShope01, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                                }
                            }
                            else if (MyShope02.gameObject.tag == DictionaryForAllShop[ListForAllWeapon[i]] && !ListForAllContrShop[1].IsUsing)
                            {
                                if (ControlerForShoot.SlotForUseShop == MyShope03 && ListForAllContrShop[3])
                                {
                                    DisChargingShops(MyShope02, SlotShpo02);
                                    Charge(MyShope01, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                                }
                                else
                                {
                                    Charge(MyShope02, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                                }
                            }
                            else if (MyShope03.gameObject.tag == DictionaryForAllShop[ListForAllWeapon[i]] && !ListForAllContrShop[2].IsUsing)
                            {
                                Charge(MyShope02, ControlerForShoot.SlotForUseShop, ControlerForShoot);
                            }
                            
                            
                            
                            
                        }
                    }
                }
            }
            
            /*
            if (MyPistol01 && ObjectInHand == MyPistol01.gameObject && MyPistol01.gameObject.tag == "Glok")
            {
                Charge(MyPistol01.gameObject, PointForShopPistol01, "Glok", "ShopGlok");
                if (CounetrForCharge == 0) ChangingShops(MyPistol01.gameObject, PointForShopPistol01, "Glok", "ShopGlok");
            }
            else if (MyPistol01 && ObjectInHand == MyPistol01.gameObject && MyPistol01.gameObject.tag == "M1911")
            {
                Charge(MyPistol01.gameObject, PointForShopPistol01, "M1911", "ShopM1911");
                if (CounetrForCharge == 0) ChangingShops(MyPistol01.gameObject, PointForShopPistol01, "M1911", "ShopM1911");
            }


            else if (MyWeapon01 && ObjectInHand == MyWeapon01.gameObject && MyWeapon01.gameObject.tag == "M4")
            {
                Charge(MyWeapon01.gameObject, PointForShopWeapon01, "M4", "ShopM4");                
                if(CounetrForCharge == 0) ChangingShops(MyWeapon01.gameObject, PointForShopWeapon01, "M4", "ShopM4" );            
            }
            //////////
            
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
            */
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

    

    


    void Charge(Transform ShopForCharge, Transform PointForRecharge, ShootControler ReferenseForWeapon)
    {
        if (!ReferenseForWeapon.WeaponShoop && CounetrForCharge == 0)
        {
            PutObjects(ShopForCharge, PointForRecharge);
            ReferenseForWeapon.WeaponShoop = MyShope01.gameObject;

            CounetrForCharge = 1;
        }
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

