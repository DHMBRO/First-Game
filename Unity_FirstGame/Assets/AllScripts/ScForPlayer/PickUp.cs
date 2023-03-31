using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] protected GameObject TakeObject;

    [SerializeField] protected Transform[] TakeObjects;

    [SerializeField] protected int Count = 0;


    [SerializeField] protected SlotControler MySlotControler;

    void Start()
    {
        MySlotControler = gameObject.GetComponent<SlotControler>();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.CompareTag("M4"))
        {
            TakeObject = other.gameObject;
            
        }
        */
        
        if (other.gameObject.CompareTag("ShopForM4"))
        {
            TakeObject = other.gameObject;
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        /*
        if (other.gameObject.CompareTag("M4"))
        {
            TakeObject = null;
    
        }        
        */
        
        if (other.gameObject.CompareTag("ShopForM4"))
        {
            TakeObject = null;            
        }
        
    }

    private void Update()
    {
        if (TakeObject && Input.GetKey(KeyCode.E) && Count == 0)
        {
            TakeM4();
            TakeShopForM4();

        }        
        else if (Count == 1)
        {
            Count = 0;
        }
    }

    void TakeM4()
    {
        if (TakeObject.gameObject.CompareTag("M4"))
        {
            GameObject CopyM4 = Instantiate(TakeObject);
            Transform TransformForCopyM4 = CopyM4.GetComponent<Transform>();
            GameObject OriginalObject = TakeObject.gameObject;

            CopyM4.transform.position = TakeObject.transform.position;
            CopyM4.transform.rotation = TakeObject.transform.rotation;


            if (true)
            {
                MySlotControler.MyWeapon01 = TransformForCopyM4;
                MySlotControler.PutWeapon(MySlotControler.MyWeapon01, MySlotControler.SlotBack01);

            }
            Destroy(OriginalObject);
            Count++;
        }
        
    }

    void TakeShopForM4()
    {
        if (TakeObject.gameObject.CompareTag("ShopForM4"))
        {
            GameObject CopyShopForM4 = Instantiate(TakeObject);
            Transform TransformForCopyShopForM4 = CopyShopForM4.GetComponent<Transform>();
            GameObject OriginalShop = TakeObject.gameObject;

            CopyShopForM4.transform.position = TakeObject.transform.position;
            CopyShopForM4.transform.rotation = TakeObject.transform.rotation;


            if (true)
            {
                Debug.Log("1");
                MySlotControler.MyShope01 = TransformForCopyShopForM4;
                MySlotControler.PutShop(MySlotControler.MyShope01, MySlotControler.SlotShpo01);
            }
            Destroy(OriginalShop);
            Count++;

        }
    }







}





