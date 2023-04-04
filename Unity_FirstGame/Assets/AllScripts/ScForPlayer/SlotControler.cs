using UnityEngine;

public class SlotControler : MonoBehaviour
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
    [SerializeField] private Transform SlotShpo03;
    //
    [SerializeField] private Transform UsingShope01;
    //[SerializeField] private Transform UsingShope02;
    //[SerializeField] private Transform UsingShope03;

    //All counet for work Script        
    protected int Counter;
    private int MainCounter;    
    //
    [SerializeField] private Transform ObjectInHand;

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

    }




    void MovingGunForSlots()
    {
        if (SlotHand && Input.GetKey("1"))
        {
            if (MyKnife01 && MainCounter == 0 && Counter == 0)
            {
                PutObjects(MyKnife01,SlotHand);
                ObjectInHand = MyKnife01;

                MainCounter = 1;
                Counter++;
            }
            else if (MyKnife01 && SlotKnife01 && MainCounter == 1 && Counter == 0)
            {
                PutObjects(MyKnife01, SlotKnife01);
                ObjectInHand = null;
                Debug.Log("1");
                
                MainCounter = 2;
                Counter++;
            }

            if (MyPistol01 && MainCounter == 2 && Counter == 0)
            {
                PutObjects(MyPistol01, SlotHand);
                ObjectInHand = MyPistol01;

                MainCounter = 3;
                Counter++;

            }
            else if (MyPistol01 && SlotPistol01 && MainCounter == 3 && Counter == 0)
            {
                PutObjects(MyPistol01,SlotPistol01);
                ObjectInHand = null;

                MainCounter = 4;
                Counter++;

            }
            
            if (MyWeapon01 && MainCounter == 4 && Counter == 0)
            {
                PutObjects(MyWeapon01, SlotHand);
                ObjectInHand = MyWeapon01;

                MainCounter = 5;
                Counter++;

            }
            else if (MyWeapon01 && SlotBack01 && MainCounter == 5 && Counter == 0)
            {
                PutObjects(MyWeapon01, SlotBack01);
                ObjectInHand = null;

                MainCounter = 0;
                Counter++;

            }
            /*
            if (MyWeapon02 && MainCounter == 6 && Counter == 0)
            {

                PutObjects(MyWeapon02, SlotHand);
                ObjectInHand = MyWeapon02;

                MainCounter = 7;
                Counter++;

            }
            else if (MyWeapon02 && SlotBack02 && MainCounter == 7 && Counter == 0)
            {

                PutObjects(MyWeapon02, SlotBack02);
                ObjectInHand = null;

                MainCounter = 0;
                Counter++;

            }

            */
        }

    }

    

    public void ChargeM4()
    {

    }

    public void DisChargeM4()
    {

    }


    public Transform PutObjects(Transform ObjectForPut, Transform PosForPut)
    {
        ObjectForPut.transform.SetParent(PosForPut);

        ObjectForPut.position = PosForPut.transform.position;
        ObjectForPut.rotation = PosForPut.transform.rotation;

        return ObjectForPut;
    }

}