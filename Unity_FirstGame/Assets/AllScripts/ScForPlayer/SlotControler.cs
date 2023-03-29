using UnityEngine;

public class SlotControler : MonoBehaviour
{
    //All Slots
    [SerializeField] private Transform SlotGun;
    [SerializeField] public Transform SlotHand;

    //Weapons for slots
    [SerializeField] public Transform MyGun;


    //All counet for work Script        
    [SerializeField] protected int Counter;
    [SerializeField] public bool CanFire = true;

    //Fiset for other Scripts 
    [SerializeField] private PickUp PickUp;
    [SerializeField] private InventoryControler InventoryControler;


    void Start()
    {
        PickUp = gameObject.GetComponent<PickUp>();
        InventoryControler = gameObject.GetComponent<InventoryControler>();
        

        CanFire = false;
        Appropriation01();    
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
        if (MyGun && SlotGun && SlotHand)
        {
            if (Input.GetKey("1"))
            {
                if (Counter < 1 && !CanFire)
                {
                    Appropriation02();
                    CanFire = true;
                    Counter++;
                }
                else if (Counter < 1 && CanFire)
                {
                    Appropriation01();
                    CanFire = false;
                    Counter++;
                }
            }
        }

    }

    public void Appropriation01()
    {
        if (MyGun)
        {

            MyGun.SetParent(SlotGun);
            MyGun.transform.position = SlotGun.transform.position;
            MyGun.transform.rotation = SlotGun.transform.rotation;
        }

    }

    public void Appropriation02()
    {

        MyGun.SetParent(SlotHand);
        MyGun.transform.position = SlotHand.transform.position;
        MyGun.transform.rotation = SlotHand.transform.rotation;
    }

}
