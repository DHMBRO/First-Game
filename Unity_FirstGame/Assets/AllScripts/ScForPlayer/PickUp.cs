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
            Debug.Log("You can pick up M4");
        }
        */

        if (other.gameObject.CompareTag("ShopForM4"))
        {
            TakeObject = other.gameObject;
            Debug.Log("You can pick up ShopForM4");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /*
        if (other.gameObject.CompareTag("M4"))
        {
            TakeObject = null;
            Debug.Log("You cant pick up M4");
        }
        */

        if (other.gameObject.CompareTag("ShopForM4"))
        {
            TakeObject = null;
            Debug.Log("You cant pick up ShopForM4");
        }

        void Update()
        {
            Debug.Log("Is work");
            TakeM4();
            if (MySlotControler && TakeObject.transform.CompareTag("M4") && Input.GetKey(KeyCode.E))
            {

            }
            else if (Count == 1)
            {
                Count = 0;
            }
        }

        void TakeM4()
        {
            if (Count == 0)
            {
                GameObject CopyM4 = Instantiate(TakeObject);

                CopyM4.transform.position = TakeObject.transform.position;
                CopyM4.transform.rotation = TakeObject.transform.rotation;

                Transform TransformForCopyM4 = CopyM4.GetComponent<Transform>();

                GameObject OriginalObject = TakeObject.gameObject;
                Destroy(OriginalObject);

                if (CopyM4.gameObject.CompareTag("M4"))
                {
                    MySlotControler.MyWeapon01 = TransformForCopyM4;
                    MySlotControler.PutWeapon01 ();
                }

                Count++;
            }
        }        

        void TakeShopForM4()
        {
            if (Count == 0)
            {
                GameObject CopyShopForM4 = Instantiate(TakeObject);

                CopyShopForM4.transform.position = TakeObject.transform.position;
                CopyShopForM4.transform.rotation = TakeObject.transform.rotation;

                Transform TransformForCopyShopForM4 = CopyShopForM4.GetComponent<Transform>();
                /*
                if (CopyShopForM4.gameObject.CompareTag("ShopForM4"))
                {
                    MySlotControler. = TransformForCopyShopForM4;
                    MySlotControler.Appropriation01();
                }
                */
            }

            
        }

        
    }




}





