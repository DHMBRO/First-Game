using UnityEngine;

public class PickUp : MethodsFromDevelopers
{    
    [SerializeField] public GameObject ObjectToBeLifted;
    [SerializeField] private Transform TransformForCamera;
    
    [SerializeField] private float DistanceForRay = 2.0f;

    [SerializeField] private SlotControler SlotControler;    
                    
    private Ray RayForFindingObject;
    private int Counter = 0;

    void Start()
    {
        SlotControler = gameObject.GetComponent<SlotControler>();        
    }                   

    private void Update()
    {        
        RayForLoot();
        
        if (ObjectToBeLifted)
        {
            if (Input.GetKey(KeyCode.E) && Counter == 0 && ObjectToBeLifted)
            {
                if (ObjectToBeLifted.CompareTag("M4"))
                {
                    PickUpWeapon(ObjectToBeLifted);
                }
                if (ObjectToBeLifted.CompareTag("Glok"))
                {
                    PickUpPistol(ObjectToBeLifted);
                }
                
                
            }            
        }
        
        if (Counter == 1)
        {
            Counter = 0;
        }
    }

    void RayForLoot()
    {
        if (TransformForCamera)
        {                        
            Ray Asadas = new Ray(TransformForCamera.transform.position, TransformForCamera.transform.forward);
            
            Debug.DrawRay(TransformForCamera.transform.position, TransformForCamera.transform.forward * DistanceForRay, Color.red);

            if (Physics.Raycast(Asadas, out RaycastHit HitResult, DistanceForRay))
            {
                //Pick up all Knife
                if (HitResult.collider.gameObject.tag == "Knife")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                // Pick Up all weapon                 
                if (HitResult.collider.gameObject.tag == "M4")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                else if (HitResult.collider.gameObject.tag == "Glok")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                // Pick Up Shop For all
                if (HitResult.collider.gameObject.tag == "ShopForM4")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                else if (HitResult.collider.gameObject.tag == "ShopForGlok")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                else if (HitResult.collider.gameObject.tag == "Untagged")
                {
                    ObjectToBeLifted = null;
                }

            }            
        }
    }
    

    private GameObject PickUpWeapon(GameObject OriginalObject)
    {
        if (!SlotControler.MyWeapon01)
        {
            GameObject CopyObject = Instantiate(ObjectToBeLifted);
            Transform TransformForCopyM4 = CopyObject.GetComponent<Transform>();
            GameObject GameObkect = ObjectToBeLifted.gameObject;

            CopyObject.transform.position = ObjectToBeLifted.transform.position;
            CopyObject.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyWeapon01 = TransformForCopyM4;
            SlotControler.PutObjects(SlotControler.MyWeapon01, SlotControler.SlotBack01);

            Destroy(GameObkect);            
            Counter++;
            
            
        }
        return OriginalObject;
    }

    private GameObject PickUpPistol(GameObject OriginalObject)
    {
        if (!SlotControler.MyPistol01)
        {
            GameObject CopyObject = Instantiate(ObjectToBeLifted);
            Transform TransformForCopyM4 = CopyObject.GetComponent<Transform>();
            GameObject GameObkect = ObjectToBeLifted.gameObject;

            CopyObject.transform.position = ObjectToBeLifted.transform.position;
            CopyObject.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyPistol01 = TransformForCopyM4;
            SlotControler.PutObjects(SlotControler.MyPistol01, SlotControler.SlotPistol01);

            Destroy(GameObkect);
            Counter++;


        }
        return OriginalObject;
    }

    void TakeWeapon01()
    {
        if (!SlotControler.MyWeapon01)
        {
            GameObject CopyM4 = Instantiate(ObjectToBeLifted);
            Transform TransformForCopyM4 = CopyM4.GetComponent<Transform>();
            GameObject OriginalObject = ObjectToBeLifted.gameObject;

            CopyM4.transform.position = ObjectToBeLifted.transform.position;
            CopyM4.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyWeapon01 = TransformForCopyM4;
            SlotControler.PutObjects(SlotControler.MyWeapon01, SlotControler.SlotBack01);

            Destroy(OriginalObject);
            Counter++;

        }

    }

    void TakeWeapon02()
    {
        if (!SlotControler.MyWeapon01)
        {
            GameObject CopyM4 = Instantiate(ObjectToBeLifted);
            Transform TransformForCopyM4 = CopyM4.GetComponent<Transform>();
            GameObject OriginalObject = ObjectToBeLifted.gameObject;

            CopyM4.transform.position = ObjectToBeLifted.transform.position;
            CopyM4.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyWeapon01 = TransformForCopyM4;
            SlotControler.PutObjects(SlotControler.MyWeapon01, SlotControler.SlotBack01);

            Destroy(OriginalObject);
            Counter++;

        }

    }

    void TakePistol01()
    {
        if (!SlotControler.MyPistol01)
        {
            GameObject CopyGlok = Instantiate(ObjectToBeLifted);
            Transform TransformForCopyGlok = CopyGlok.GetComponent<Transform>();
            GameObject OriginalObject = ObjectToBeLifted.gameObject;

            CopyGlok.transform.position = ObjectToBeLifted.transform.position;
            CopyGlok.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyPistol01 = TransformForCopyGlok;
            SlotControler.PutObjects(SlotControler.MyPistol01, SlotControler.SlotPistol01);

            Destroy(OriginalObject);
            Counter++;

        }
    }

    void TakeKnife()
    {
        if (!SlotControler.MyKnife01)
        {
            GameObject CopyKnife = Instantiate(ObjectToBeLifted);
            Transform TransformForCopyKnife = CopyKnife.GetComponent<Transform>();
            GameObject OriginalObject = ObjectToBeLifted.gameObject;

            CopyKnife.transform.position = ObjectToBeLifted.transform.position;
            CopyKnife.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyKnife01 = TransformForCopyKnife;
            SlotControler.PutObjects(SlotControler.MyKnife01, SlotControler.SlotKnife01);

            Destroy(OriginalObject);
            Counter++;

        }
    }

    void TakeShopForGlok()
    {
        if (!SlotControler.MyShope01)
        {
            GameObject CopyShopForGlok = Instantiate(ObjectToBeLifted);
            Transform TransformForCopyShopForGlok = CopyShopForGlok.GetComponent<Transform>();
            GameObject OriginalShop = ObjectToBeLifted.gameObject;

            CopyShopForGlok.transform.position = ObjectToBeLifted.transform.position;
            CopyShopForGlok.transform.rotation = ObjectToBeLifted.transform.rotation;

            if (true)
            {
                SlotControler.MyShope01 = TransformForCopyShopForGlok;
                SlotControler.PutObjects(SlotControler.MyShope01, SlotControler.SlotShpo02);
            }
            Destroy(OriginalShop);
            Counter++;
        }
    }

    void TakeShopForM4()
    {
        if (ObjectToBeLifted.gameObject.tag == "ShopForM4")
        {
            GameObject CopyShopForM4 = Instantiate(ObjectToBeLifted);
            Transform TransformForCopyShopForM4 = CopyShopForM4.GetComponent<Transform>();
            GameObject OriginalShop = ObjectToBeLifted.gameObject;

            CopyShopForM4.transform.position = ObjectToBeLifted.transform.position;
            CopyShopForM4.transform.rotation = ObjectToBeLifted.transform.rotation;

            if (true)
            {
                SlotControler.MyShope01 = TransformForCopyShopForM4;
                SlotControler.PutObjects(SlotControler.MyShope01, SlotControler.SlotShpo01);
            }
            Destroy(OriginalShop);
            Counter++;

        }
    }
}





