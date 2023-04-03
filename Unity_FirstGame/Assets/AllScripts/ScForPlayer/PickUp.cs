using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] protected GameObject TakeObject;
    [SerializeField] public GameObject WatchObject;
    [SerializeField] protected Transform[] TakeObjects;
    [SerializeField] public Camera CameraScript;
    [SerializeField] protected int Count = 0;


    [SerializeField] protected SlotControler MySlotControler;
    [SerializeField] private CamFirstFace MyCamFirstFace;
    



    void Start()
    {
        MySlotControler = gameObject.GetComponent<SlotControler>();
        
    }
           
    

    

    private void Update()
    {
        Appropriation01();

        if (TakeObject)
        {            
            if (Input.GetKey(KeyCode.E) && Count == 0)
            {
                TakeM4();
                TakeShopForM4();                
            }            
            else if (Count == 1)
            {
                Count = 0;
            }
        }
        else
        {
            Debug.Log("TakeObject = null");
        }
    }

    void Appropriation01()
    {
        if (MyCamFirstFace.ObjectForWatch)
        {
            if (MyCamFirstFace.ObjectForWatch.gameObject.tag == "M4")
            {
                TakeObject = MyCamFirstFace.ObjectForWatch.gameObject;                
            }
            else if (MyCamFirstFace.ObjectForWatch.gameObject.tag == "ShopForM4")
            {
                TakeObject = MyCamFirstFace.ObjectForWatch.gameObject; 
            }
        }
                
    }

    void TakeM4()
    {
        if (TakeObject && MyCamFirstFace.ObjectForWatch)
        {
            if (MyCamFirstFace.ObjectForWatch.gameObject.tag == "M4")
            {
                GameObject CopyM4 = Instantiate(TakeObject);
                Transform TransformForCopyM4 = CopyM4.GetComponent<Transform>();
                GameObject OriginalObject = TakeObject.gameObject;

                CopyM4.transform.position = TakeObject.transform.position;
                CopyM4.transform.rotation = TakeObject.transform.rotation;

                MySlotControler.MyWeapon01 = TransformForCopyM4;
                MySlotControler.PutObjects(MySlotControler.MyWeapon01, MySlotControler.SlotBack01);

                Destroy(OriginalObject);
                Count++;

            }

        }
        
    }

    void TakeShopForM4()
    {
        if (TakeObject && MyCamFirstFace.ObjectForWatch)
        {
            if (MyCamFirstFace.ObjectForWatch.gameObject.tag == "ShopForM4")
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
                    MySlotControler.PutObjects(MySlotControler.MyShope01, MySlotControler.SlotShpo01);
                }
                Destroy(OriginalShop);
                Count++;

            }
        }
        
    }







}





