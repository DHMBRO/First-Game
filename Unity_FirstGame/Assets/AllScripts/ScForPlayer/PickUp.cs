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
                    Debug.Log("1");
                }
                else if (ObjectToBeLifted.CompareTag("M4") && !SlotControler.MyWeapon01 && !SlotControler.MyWeapon02 && Counter == 0)
                {
                    MainCounter = 2;
                    PickUpWeapon01(ObjectToBeLifted);
                    Debug.Log("2");
                }                
                else if (ObjectToBeLifted.CompareTag("M4") && !SlotControler.MyWeapon02 && SlotControler.MyWeapon01 && Counter == 0)
                {
                    MainCounter = 3;
                    PickUpWeapon02(ObjectToBeLifted);
                    Debug.Log("3");
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
            SlotControler.PutObjects(SlotControler.MyPistol01, SlotControler.SlotPistol01);

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
            SlotControler.PutObjects(SlotControler.MyWeapon01, SlotControler.SlotBack01);

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
            SlotControler.PutObjects(SlotControler.MyWeapon02, SlotControler.SlotBack02);

            Destroy(GameObject);
            Counter++;

        }
        return OriginalObject;
    }

}





