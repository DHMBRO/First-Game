using UnityEngine;

public class PickUp : MonoBehaviour
{    
    [SerializeField] private GameObject ObjectToBeLifted;    
    
    [SerializeField] private SlotControler SlotControler;    
    [SerializeField] private Transform TransformForCamera;    
    
    [SerializeField] private float DistanceForRay = 0.6f;
    
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
            if (Input.GetKey(KeyCode.E) && Counter == 0)
            {
                TakeM4();
                TakeShopForM4();                
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
            RayForFindingObject = new Ray(TransformForCamera.transform.position, TransformForCamera.transform.forward * DistanceForRay);
            
            Debug.DrawRay(TransformForCamera.transform.position, TransformForCamera.transform.forward * DistanceForRay, Color.blue);

            if (Physics.Raycast(RayForFindingObject, out RaycastHit HitResult))
            {                
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
        if (ObjectToBeLifted)
        {
            if (ObjectToBeLifted.gameObject.tag == "M4")
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





