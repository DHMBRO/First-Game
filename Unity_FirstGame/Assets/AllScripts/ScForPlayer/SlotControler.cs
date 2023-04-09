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
    [SerializeField] private GameObject ObjectInHand;

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
        
        if (Counter == 1 && Input.GetKeyUp("1"))
        {
            Counter = 0;
        }
        MovingGunForSlots();
        UseM4();    
    }
        
    void MovingGunForSlots()
    {
        if (SlotHand && Input.GetKey("1"))
        {
            //Appropriation01();
            
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
        if (MyKnife01 && ObjectInHand == null)
        {
            if (SlotCounter == 0 && Counter == 0)
            {
                ObjectInHand = MyKnife01.gameObject;
                
                SlotCounter = 1;
                Counter = 1;
            
            }
        }

        //if ()
        {

        }
        
        if (ObjectInHand == MyWeapon01.gameObject)
        {
            if (SlotCounter == 3 && Counter == 0)
            {
                ObjectInHand = MyWeapon01.gameObject;

                SlotCounter = 4;
                Counter = 1;
            }
        }
        else if (ObjectInHand == MyWeapon02.gameObject)
        {
            if (SlotCounter == 4 && Counter == 0)
            {
                ObjectInHand = MyWeapon02.gameObject;

                SlotCounter = 5;
                Counter = 1;
            }
        }
    }

    



}