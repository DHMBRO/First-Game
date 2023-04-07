using UnityEngine;

public class PickUp : MethodsFromDevelopers
{
    [SerializeField] public GameObject ObjectToBeLifted;
    [SerializeField] private Transform TransformForCamera;
    
    private float DistanceForRay = 2.0f;
    private int MainCounter = 0;
    private int Counter = 0;

    private SlotControler SlotControler;
    private Ray RayForFindingObject;
    

    void Start()
    {
        SlotControler = gameObject.GetComponent<SlotControler>();        
    }                   

    private void Update()
    {        
        RayForLoot();
        
        if (ObjectToBeLifted)
        {
            if (Input.GetKey(KeyCode.E) && ObjectToBeLifted)
            {
                if (ObjectToBeLifted.CompareTag("Glok") && !SlotControler.MyPistol01 && Counter == 0)
                {
                    MainCounter = 1;
                    PickUpPistol(ObjectToBeLifted);                    
                }
                else if (ObjectToBeLifted.CompareTag("M4") && !SlotControler.MyWeapon01 && !SlotControler.MyWeapon02 && Counter == 0)
                {
                    MainCounter = 2;
                    PickUpWeapon01(ObjectToBeLifted);                   
                }                                
                else if (ObjectToBeLifted.CompareTag("M4") && !SlotControler.MyWeapon02 && SlotControler.MyWeapon01 && Counter == 0)
                {                    
                    MainCounter = 3;
                    PickUpWeapon02(ObjectToBeLifted);                    
                }

                
                if (ObjectToBeLifted.CompareTag("ShopForGlok") && Counter == 0)
                {
                    PickUpShops(ObjectToBeLifted);                    
                }
                else if (ObjectToBeLifted.CompareTag("ShopForM4") && Counter == 0)
                {
                    PickUpShops(ObjectToBeLifted);
                
                }
                
            }            
                
        }
        
        if (Counter == 1 && Input.GetKeyUp(KeyCode.E))
        {
            Counter = 0;
        }
    }

    void RayForLoot()
    {
        if (TransformForCamera)
        {                        
            Ray RayForPickUp = new Ray(TransformForCamera.transform.position, TransformForCamera.transform.forward);
            
            Debug.DrawRay(TransformForCamera.transform.position, TransformForCamera.transform.forward * DistanceForRay, Color.red);

            if (Physics.Raycast(RayForPickUp, out RaycastHit HitResult, DistanceForRay))
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
            else
            {
                ObjectToBeLifted = null;
            }                                  
        }
    }
    

    private GameObject PickUpPistol(GameObject OriginalObject)
    {
        if (MainCounter == 1 && Counter == 0)
        {
            
            GameObject CopyObject = Instantiate(ObjectToBeLifted);
            Transform TransformForCopy = CopyObject.GetComponent<Transform>();
            GameObject GameObject = ObjectToBeLifted.gameObject;

            CopyObject.transform.position = ObjectToBeLifted.transform.position;
            CopyObject.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyPistol01 = TransformForCopy;
            PutObjects(SlotControler.MyPistol01, SlotControler.SlotPistol01);

            Destroy(GameObject);
            Counter++;


        }              
        return OriginalObject;
    }

    private GameObject PickUpWeapon01(GameObject OriginalObject)
    {
        if (MainCounter == 2 && Counter == 0)
        {
            GameObject CopyObject = Instantiate(ObjectToBeLifted);
            Transform TransformForCopy = CopyObject.GetComponent<Transform>();
            GameObject GameObject = ObjectToBeLifted.gameObject;

            CopyObject.transform.position = ObjectToBeLifted.transform.position;
            CopyObject.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyWeapon01 = TransformForCopy;
            PutObjects(SlotControler.MyWeapon01, SlotControler.SlotBack01);

            Destroy(GameObject);
            Counter++;

        }

        return OriginalObject;
    }
    
    private GameObject PickUpWeapon02(GameObject OriginalObject)
    {
        if (MainCounter == 3 && Counter == 0)
        {
            GameObject CopyObject = Instantiate(ObjectToBeLifted);
            Transform TransformForCopy = CopyObject.GetComponent<Transform>();
            GameObject GameObject = ObjectToBeLifted.gameObject;

            CopyObject.transform.position = ObjectToBeLifted.transform.position;
            CopyObject.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyWeapon02 = TransformForCopy;
            PutObjects(SlotControler.MyWeapon02, SlotControler.SlotBack02);

            Destroy(GameObject);
            Counter++;

        }
        return OriginalObject;
    }
    
    private GameObject PickUpShops(GameObject ShopForPickUp)
    {
        Debug.Log("2");
        if (!SlotControler.MyShope01 && SlotControler.SlotShpo01 &&Counter == 0)
        {
            GameObject CopyObject = Instantiate(ObjectToBeLifted);
            Transform TransformForCopy = CopyObject.GetComponent<Transform>();
            GameObject GameObject = ObjectToBeLifted.gameObject;

            CopyObject.transform.position = ObjectToBeLifted.transform.position;
            CopyObject.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyShope01 = TransformForCopy;
            PutObjects(SlotControler.MyShope01, SlotControler.SlotShpo01);

            Destroy(GameObject);
            Counter++;
            Debug.Log("A");
        }
        else if (!SlotControler.MyShope02 && SlotControler.SlotShpo02 && Counter == 0)
        {
            GameObject CopyObject = Instantiate(ObjectToBeLifted);
            Transform TransformForCopy = CopyObject.GetComponent<Transform>();
            GameObject GameObject = ObjectToBeLifted.gameObject;

            CopyObject.transform.position = ObjectToBeLifted.transform.position;
            CopyObject.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyShope02 = TransformForCopy;
            PutObjects(SlotControler.MyShope02, SlotControler.SlotShpo02);

            Destroy(GameObject);
            Counter++;
            Debug.Log("B");
        }
        else if (!SlotControler.MyShope03 && SlotControler.SlotShpo03 && Counter == 0)
        {
            GameObject CopyObject = Instantiate(ObjectToBeLifted);
            Transform TransformForCopy = CopyObject.GetComponent<Transform>();
            GameObject GameObject = ObjectToBeLifted.gameObject;

            CopyObject.transform.position = ObjectToBeLifted.transform.position;
            CopyObject.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyShope03 = TransformForCopy;
            PutObjects(SlotControler.MyShope03, SlotControler.SlotShpo03);

            Destroy(GameObject);
            Counter++;
            Debug.Log("C");
        }
        else if (true)
        {
            Debug.Log("Cant do this !");
        }
        return ShopForPickUp;
    }

}





