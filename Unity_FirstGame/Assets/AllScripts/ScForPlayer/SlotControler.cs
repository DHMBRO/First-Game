using UnityEngine;

public class SlotControler : MonoBehaviour
{
    //All Slots
    [SerializeField] public Transform SlotHand;
    //
    [SerializeField] private Transform SlotBack01;
    [SerializeField] private Transform SlotBack02;    
    //
    [SerializeField] private Transform SlotPistol01;
    //
    [SerializeField] private Transform SlotKnife01;
    //
    [SerializeField] private Transform SlotShpo01;
    [SerializeField] private Transform SlotShpo02;
    [SerializeField] private Transform SlotShpo03;
    //

    //Weapons for slots
    [SerializeField] public Transform MyWeapon01;
    [SerializeField] public Transform MyWeapon02;
    //    
    [SerializeField] public Transform MyPistol01;
    //
    [SerializeField] private Transform MyKnife01;
    //
    [SerializeField] private Transform MyShop01;
    [SerializeField] private Transform MyShop03;
    [SerializeField] private Transform MyShop02;
    
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
                UseWeapon01();
                CanFire = true;
                Counter++;
            }
            else if (Counter < 1 && CanFire)
            {
                PutWeapon01();
                CanFire = false;
                Counter++;
            }
        }
        //else if ()
        {

        }


    }

    public void UseWeapon01()
    {
        if (SlotHand && MyWeapon01)
        {
            MyWeapon01.SetParent(SlotHand);
            MyWeapon01.transform.position = SlotHand.transform.position;
            MyWeapon01.transform.rotation = SlotHand.transform.rotation;

        }
    }

    public void PutWeapon01()
    {
        if (SlotHand && MyWeapon01)
        {
            MyWeapon01.SetParent(SlotBack01);
            MyWeapon01.transform.position = SlotBack01.transform.position;
            MyWeapon01.transform.rotation = SlotBack01.transform.rotation;
        }
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
        //if ()
        {

        }
    }
        















}
