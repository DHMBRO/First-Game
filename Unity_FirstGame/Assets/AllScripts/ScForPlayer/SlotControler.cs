using UnityEngine;
using System.Collections.Generic;

public class SlotControler : MethodsFromDevelopers
{    
    //All Slots
    [SerializeField] public Transform SlotHand;
    //
    [SerializeField] public Transform[] SlotsForBack01;
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
    private int SlotCounter;    
    //
    [SerializeField] public GameObject ObjectInHand = null;    
    

    //Fiset for other Scripts 
    [SerializeField] private PickUp PickUp;
    [SerializeField] private InventoryControler InventoryControler;
    //
    

    void Start()
    {
        PickUp = gameObject.GetComponent<PickUp>();
        InventoryControler = gameObject.GetComponent<InventoryControler>();       
    }

    void Update()
    {
        AppropriationReferenceForUseShop();
    }

    public void MovingGunForSlots()
    {
        ChangingSlots();
        if (Input.GetKeyDown(KeyCode.R))
        {
            
        }
        

        if (!MyWeapon01 && MyWeapon02)
        {
            MyWeapon01 = MyWeapon02;
            PutObjects(MyWeapon01, SlotBack01);

            MyWeapon02 = null;
        }
        else if (Counter == 1 && Input.GetKeyUp("1"))
        {
            Counter = 0;
        }
    }
   
    void AppropriationReferenceForUseShop()
    {
        ShootControler ShootControler;
        
        if (MyPistol01 && !PointForShopPistol01)
        {
            ShootControler = MyPistol01.GetComponent<ShootControler>();
            PointForShopPistol01 = ShootControler.SlotForUseShop;
        }
        if (MyWeapon01 && !PointForShopPistol01 && !PointForShopWeapon02)
        {
            ShootControler = MyWeapon01.GetComponent<ShootControler>();
            PointForShopWeapon01 = ShootControler.SlotForUseShop;
        }
        if (MyWeapon02 && PointForShopWeapon01 &&!PointForShopWeapon02)
        {
            ShootControler = MyWeapon02.GetComponent<ShootControler>();
            PointForShopWeapon02 = ShootControler.SlotForUseShop;
        }

    }

    void ChangingShops()
    {

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

    



}