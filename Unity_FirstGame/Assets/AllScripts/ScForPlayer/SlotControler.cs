using UnityEngine;

public class SlotControler : MonoBehaviour
{
    //All Slots
    [SerializeField] public Transform SlotHand;
    //
    [SerializeField] public Transform SlotBack01;
    [SerializeField] private Transform SlotBack02;
    //
    [SerializeField] private Transform SlotPistol01;
    //
    [SerializeField] private Transform SlotKnife01;

    //Weapons for slots
    [SerializeField] public Transform MyWeapon01;
    [SerializeField] public Transform MyWeapon02;
    //    
    [SerializeField] public Transform MyPistol01;
    //
    [SerializeField] private Transform MyKnife01;
    //
    [SerializeField] public Transform MyShope01;
    [SerializeField] private Transform MyShope02;
    [SerializeField] private Transform MyShope03;
    //
    [SerializeField] public Transform SlotShpo01;
    [SerializeField] private Transform SlotShpo02;
    [SerializeField] private Transform SlotShpo03;
    //
    [SerializeField] private Transform UsingShope01;
    //[SerializeField] private Transform UsingShope02;
    //[SerializeField] private Transform UsingShope03;

    //All counet for work Script        
    [SerializeField] protected int Counter;
    [SerializeField] private int MainCounter;
    [SerializeField] public bool CanFire = true;


    //Fiset for other Scripts 
    [SerializeField] private PickUp PickUp;
    [SerializeField] private InventoryControler InventoryControler;



    void Start()
    {
        PickUp = gameObject.GetComponent<PickUp>();
        InventoryControler = gameObject.GetComponent<InventoryControler>();

        CanFire = false;
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
        if (MyWeapon01 && SlotBack01 && SlotHand && Input.GetKey("1"))
        {
            if (Counter < 1 && !CanFire)
            {
                PutObjects(MyWeapon01, SlotHand);
                CanFire = true;
                Counter++;
            }
            else if (Counter < 1 && CanFire)
            {
                PutObjects(MyWeapon01, SlotBack01);
                CanFire = false;
                Counter++;
            }
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