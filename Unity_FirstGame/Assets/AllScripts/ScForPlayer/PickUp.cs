using UnityEngine;

public class PickUp : MonoBehaviour
{    
    [SerializeField] public GameObject ObjectToBeLifted;    
    
    [SerializeField] private SlotControler SlotControler;    
    [SerializeField] private Transform TransformForCamera;    
    
    [SerializeField] private float DistanceForRay = 2.0f;
    
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
                //Take all Knife
                TakeKnife();
                //Pick up Weapons
                TakeM4();
                TakeGlok();                
                //Pick up Shop for Weapons
                TakeShopForM4();
                TakeShopForGlok();
            }            
            else if (Counter == 1)
            {
                Counter = 0;
            }
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
    

    void TakeM4()
    {
        if (ObjectToBeLifted.gameObject.tag == "M4" && !SlotControler.MyWeapon01)
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

    void TakeGlok()
    {
        if (ObjectToBeLifted)
        {            
            if (ObjectToBeLifted.gameObject.tag == "Glok" && SlotControler.MyPistol01 == null)
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
    }

    void TakeKnife()
    {
        if (ObjectToBeLifted)
        {
            if (ObjectToBeLifted.gameObject.tag == "Knife" && SlotControler.MyKnife01 == null)
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
    }

    void TakeShopForGlok()
    {
        if (ObjectToBeLifted)
        {
            if (ObjectToBeLifted.gameObject.tag == "ShopForGlok")
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
    }

    void TakeShopForM4()
    {
        if (ObjectToBeLifted)
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
}





