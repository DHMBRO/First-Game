using UnityEngine;


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
    [SerializeField] private Transform UsingShope01;
    //[SerializeField] private Transform UsingShope02;
    //[SerializeField] private Transform UsingShope03;

    //All counet for work Script        
    protected int Counter;
    private int SlotCounter;    
    //
    [SerializeField] public GameObject ObjectInHand = null;

    //Fiset for other Scripts 
    [SerializeField] private PickUp PickUp;
    [SerializeField] private InventoryControler InventoryControler;



    void Start()
    {
        PickUp = gameObject.GetComponent<PickUp>();
        InventoryControler = gameObject.GetComponent<InventoryControler>();

        
    }

    void Update()
    {
        MovingGunForSlots();

        if (Counter == 1 && Input.GetKeyUp("1"))
        {
            Counter = 0;
        }
        


    }
        
    void MovingGunForSlots()
    {
        if (SlotHand && Input.GetKey("1"))
        {
            
            Appropriation01();
            
        }
    }

    void UseM4()
    {
        if (SlotHand && Input.GetKey("1"))
        {
            if (MyWeapon01 && Counter == 0)
            {
                PutObjects(MyWeapon01,SlotHand);
                ObjectInHand = MyWeapon01.gameObject;
                Counter = 1;
            }
            else if (MyWeapon01 && SlotBack01 && Counter == 0)
            {
                PutObjects(MyWeapon01,SlotBack01);
                ObjectInHand = null;
                Counter = 1;
            }            
        }        
    }

    void Appropriation01()
    {
        if (SlotHand && Input.GetKeyDown("1"))
        {
            if (MyKnife01 && ObjectInHand == null && Counter == 0)
            {
                PutObjects(MyKnife01,SlotHand);

                ObjectInHand = MyKnife01.gameObject;
                Counter = 1;
                Debug.Log("I take Knife");
            }
            else if (MyKnife01 && SlotKnife01 && ObjectInHand == MyKnife01.gameObject && Counter == 0)
            {
                PutObjects(MyKnife01, SlotKnife01);
                ObjectInHand = null;
                
                if (MyPistol01)
                {
                    PutObjects(MyPistol01, SlotHand);
                    ObjectInHand = MyPistol01.gameObject;
                }
                Debug.Log("I take pistol");
                Counter = 1;

            }
            else if (MyWeapon01 && SlotBack01 && ObjectInHand == MyPistol01.gameObject && Counter == 0)
            {
                PutObjects(MyPistol01, SlotPistol01);
                ObjectInHand = null;                
                
                if (MyWeapon01)
                {
                    PutObjects(MyWeapon01, SlotHand);
                    ObjectInHand = MyWeapon01.gameObject;
                    
                }
                Debug.Log("I take weapon 1");
                Counter = 1;
            }
            else if (MyWeapon02 && SlotBack02 && ObjectInHand == MyWeapon01.gameObject && Counter == 0)
            {
                PutObjects(MyWeapon01,SlotBack01);
                ObjectInHand = null;
                
                if (MyWeapon02)
                {
                    PutObjects(MyWeapon02, SlotHand);
                    ObjectInHand = MyWeapon02.gameObject;
                }
                Debug.Log("I take weapon 2");
                Counter = 1;
            }
            else if (MyWeapon02 && SlotBack02 && ObjectInHand == MyWeapon02.gameObject && Counter == 0)
            {
                PutObjects(MyWeapon02,SlotBack02);
                
                ObjectInHand = null;
                                                                
                Counter = 1;

            }

        }
        

    }

    



}