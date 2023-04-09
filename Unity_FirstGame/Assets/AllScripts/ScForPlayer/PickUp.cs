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
                //Pick up weapons 
                if (ObjectToBeLifted.CompareTag("Glok") && Counter == 0)
                {
                    MainCounter = 1;
                    PickUpWeapons(ObjectToBeLifted);
                }
                else if (ObjectToBeLifted.CompareTag("M1911") && Counter == 0)
                {
                    MainCounter = 1;
                    PickUpWeapons(ObjectToBeLifted);
                }
                else if (ObjectToBeLifted.CompareTag("M4") && Counter == 0)
                {
                    MainCounter = 2;
                    PickUpWeapons(ObjectToBeLifted);
                }                                
                else if (ObjectToBeLifted.CompareTag("AK47") && Counter == 0)
                {                    
                    MainCounter = 2;
                    PickUpWeapons(ObjectToBeLifted);                 
                }
                else if (ObjectToBeLifted.CompareTag("M249") && Counter == 0)
                {
                    MainCounter = 2;
                    PickUpWeapons(ObjectToBeLifted);
                }
                //Pick up shops 
                if (ObjectToBeLifted.CompareTag("ShopM4") && Counter == 0)
                {
                    PickUpShops(ObjectToBeLifted);                    
                }
                else if (ObjectToBeLifted.CompareTag("ShopAK47") && Counter == 0)
                {
                    PickUpShops(ObjectToBeLifted);
                }
                else if (ObjectToBeLifted.CompareTag("ShopM249") && Counter == 0)
                {
                    PickUpShops(ObjectToBeLifted);
                }
                else if (ObjectToBeLifted.CompareTag("ShopM1911") && Counter == 0)
                {
                    PickUpShops(ObjectToBeLifted);
                }
                else if (ObjectToBeLifted.CompareTag("ShopGlok") && Counter == 0)
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

                // Pick Up all weapon                 
                if (HitResult.collider.gameObject.tag == "M1911")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                else if (HitResult.collider.gameObject.tag == "Glok")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                else if (HitResult.collider.gameObject.tag == "M4")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                else if (HitResult.collider.gameObject.tag == "AK47")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                else if (HitResult.collider.gameObject.tag == "M249")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }


                // Pick Up Shop For all
                if (HitResult.collider.gameObject.tag == "ShopM4")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                else if (HitResult.collider.gameObject.tag == "ShopGlok")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                else if (HitResult.collider.gameObject.tag == "ShopAK47")
                {
                    ObjectToBeLifted = HitResult.collider.gameObject;
                }
                
                

            }
            else
            {
                ObjectToBeLifted = null;
            }                                  
        }
    }
    

  

    private GameObject PickUpWeapons(GameObject ObjectForPickUp)
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
        else if (!SlotControler.MyWeapon01  && !SlotControler.MyWeapon02 && MainCounter == 2 && Counter == 0)
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
        else if (SlotControler.MyWeapon01 && !SlotControler.MyWeapon02 &&MainCounter == 2 && Counter == 0)
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
        else if (Counter == 0)
        {
            Debug.Log("Cant take !");
        }
        return ObjectForPickUp;
    }
       
    private GameObject PickUpShops(GameObject ShopForPickUp)
    {
        
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
        }
        else if (true)
        {
            Debug.Log("Cant do this !");
        }
        return ShopForPickUp;
    }

}





