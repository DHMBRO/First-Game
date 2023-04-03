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
                UseWeapon(MyWeapon01,SlotHand);
                CanFire = true;
                Counter++;
            }
            else if (Counter < 1 && CanFire)
            {
                PutWeapon(MyWeapon01,SlotBack01);
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

    public Transform UseShope(Transform ShopeForUse, Transform PosForUse)
    {
        ShopeForUse.SetParent(PosForUse);
        ShopeForUse.position = PosForUse.position;
        ShopeForUse.rotation = PosForUse.rotation;

        return ShopeForUse;
    }

    public Transform PutShop(Transform ShopForPut, Transform PosForPutShop)
    {

        ShopForPut.SetParent(PosForPutShop);
        ShopForPut.position = PosForPutShop.position;
        ShopForPut.rotation = PosForPutShop.rotation;
        Debug.Log("2");
        return ShopForPut;

    }

    public Transform PutShopeInSlot(Transform PutingShop, Transform SlotForPutingShop)
    {
        PutingShop.SetParent(SlotForPutingShop);
        PutingShop.position = SlotForPutingShop.position;
        PutingShop.rotation = SlotForPutingShop.rotation;
        return PutingShop;

    }

    public void UsingWeapons()
    {

    }

    public  Transform UseWeapon(Transform WeaponForUse, Transform PosForUseWeapon)
    {

        WeaponForUse.SetParent(PosForUseWeapon);
        WeaponForUse.position = PosForUseWeapon.position;
        WeaponForUse.rotation = PosForUseWeapon.rotation;

        return WeaponForUse.transform;    
    }
    
    public Transform PutWeapon(Transform WeaponForPut, Transform PosForPutWeapon)
    {        
        WeaponForPut.SetParent(PosForPutWeapon);
        WeaponForPut.position = PosForPutWeapon.position;
        WeaponForPut.rotation = PosForPutWeapon.rotation;
        
        return WeaponForPut;
    }

    public void UseWeapon02()
    {
        if (SlotHand && MyWeapon02)
        {
            MyWeapon02.SetParent(SlotHand);
            MyWeapon02.transform.position = SlotHand.transform.position;
            MyWeapon02.transform.rotation = SlotHand.transform.rotation;

        }
    }

    public void PutWeapon02()
    {
        if (SlotBack02 && MyWeapon02)
        {
            MyWeapon02.SetParent(SlotBack02);
            MyWeapon02.transform.position = SlotBack02.transform.position;
            MyWeapon02.transform.rotation = SlotBack02.transform.rotation;
        }
    }

    public void UsePistol01()
    {
        if (SlotHand && MyPistol01)
        {
            MyPistol01.SetParent(SlotHand);
            MyPistol01.transform.position = SlotHand.transform.position;
            MyPistol01.transform.rotation = SlotHand.transform.rotation;
        }
    }

    public void PutPistol01()
    {
        if (SlotPistol01 && MyPistol01)
        {
            MyPistol01.SetParent(SlotPistol01);
            MyPistol01.transform.position = SlotPistol01.transform.position;
            MyPistol01.transform.rotation = SlotPistol01.transform.rotation;
        }

    }

    public void UseKnife01()
    {
        if (SlotHand && MyKnife01)
        {
            MyKnife01.SetParent(SlotHand);
            MyKnife01.transform.position = SlotHand.transform.position;
            MyKnife01.transform.rotation = SlotHand.transform.rotation;
        }
    }

    public void PutKnike01()
    {
        if (SlotKnife01 && MyKnife01)
        {

        }
    }
}