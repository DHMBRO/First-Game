using UnityEngine;

public class PickUp : MethodsFromDevelopers 
{
    [SerializeField] public GameObject ObjectToBeLifted;
    [SerializeField] private Transform TransformForCamera;
    [SerializeField] private CamFirstFace ReferenceForCamera;
    [SerializeField] private Transform TransformPos;    

    [SerializeField] private Inventory PlayerInventory;
    [SerializeField] private ReferenseForAllLoot ReferencesForLoots;

    [SerializeField] Sprite Sakr47;
    [SerializeField] Sprite Sm4;
    [SerializeField] Sprite Sglock;
    [SerializeField] Sprite Sknife;
    [SerializeField] GameObject weapon01;
    [SerializeField] GameObject weapon02;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject knife;

    private float DistanceForRay = 2.0f;
    private int MainCounter = 0;
    private int Counter = 0;

    private SlotControler SlotControler;
        

    void Start()
    {
        if (TransformForCamera)
        {
            ReferenceForCamera = TransformForCamera.gameObject.GetComponent<CamFirstFace>();
        }        
        SlotControler = gameObject.GetComponent<SlotControler>();        
    }

    private void Update()
    {        
        if (Counter == 1 && Input.GetKeyUp(KeyCode.F))
        {
            Counter = 0;
        }        
    }

    public void RayForLoot()
    {                
        if (TransformForCamera && ReferenceForCamera)
        {
            Ray RayForPickUp = new Ray(ReferenceForCamera.ObjectRay.transform.position, ReferenceForCamera.ObjectRay.transform.forward);

            Debug.DrawRay(ReferenceForCamera.ObjectRay.position, ReferenceForCamera.ObjectRay.forward * DistanceForRay, Color.red);

            if (Physics.Raycast(RayForPickUp, out RaycastHit HitResult, DistanceForRay))
            {
                LinkOther(HitResult);
                LinkEquipment(HitResult);
                LinkWeapons(HitResult);                
                LinkShops(HitResult);                                
            }
            else
            {
                ObjectToBeLifted = null;
            }            
        }
    }

    public void ComplertingTheLink()
    {
        if (ObjectToBeLifted)
        {
            if (Input.GetKeyDown(KeyCode.F) && ObjectToBeLifted)
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
                //
                if (ObjectToBeLifted.CompareTag("Helmet") && Counter == 0)
                {
                    PickUpEqipment(ObjectToBeLifted);
                }
                else if (ObjectToBeLifted.CompareTag("Armor") && Counter == 0)
                {
                    PickUpEqipment(ObjectToBeLifted);
                }
                else if (ObjectToBeLifted.CompareTag("BackPack") && Counter == 0)
                {
                    PickUpEqipment(ObjectToBeLifted);
                }
                //
                if (ObjectToBeLifted.CompareTag("Ammo9MM") && Counter == 0)
                {
                    PickUpOther(ObjectToBeLifted);
                }
                else if (ObjectToBeLifted.CompareTag("Ammo45_APC") && Counter == 0)
                {
                    PickUpOther(ObjectToBeLifted);
                }
                else if (ObjectToBeLifted.CompareTag("Ammo5_56MM") && Counter == 0)
                {
                    PickUpOther(ObjectToBeLifted);
                }
                else if (ObjectToBeLifted.CompareTag("Ammo7_62MM") && Counter == 0)
                {
                    PickUpOther(ObjectToBeLifted);
                }

            }
        }
    }

    private void LinkOther(RaycastHit RayResult)
    {                 
        if (RayResult.collider.gameObject.tag == "Ammo9MM")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }
        else if (RayResult.collider.gameObject.tag == "Ammo45_APC")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }
        else if (RayResult.collider.gameObject.tag == "Ammo5_56MM")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }
        else if (RayResult.collider.gameObject.tag == "Ammo7_62MM")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }        
    }
    
    private void LinkEquipment(RaycastHit RayResult)
    {
        if (RayResult.collider.gameObject.tag == "Helmet")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }
        else if (RayResult.collider.gameObject.tag == "Armor")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }
        else if (RayResult.collider.gameObject.tag == "BackPack")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }
    }

    private void LinkWeapons(RaycastHit RayResult)
    {
        if (RayResult.collider.gameObject.tag == "M1911")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }
        else if (RayResult.collider.gameObject.tag == "Glok")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }
        else if (RayResult.collider.gameObject.tag == "M4")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }
        else if (RayResult.collider.gameObject.tag == "AK47")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }
        else if (RayResult.collider.gameObject.tag == "M249")
        {
            ObjectToBeLifted = RayResult.collider.gameObject;
        }        
    }

    private void LinkShops(RaycastHit RayResult)
    {
        if (RayResult.collider.gameObject.GetComponent<ShopControler>())
        {
            ShopControler ShopControler = RayResult.collider.gameObject.GetComponent<ShopControler>();
         
            if (!ShopControler.InInventory && RayResult.collider.gameObject.tag == "ShopM4")
            {
                ObjectToBeLifted = RayResult.collider.gameObject;
            }
            else if (!ShopControler.InInventory && RayResult.collider.gameObject.tag == "ShopGlok")
            {
                ObjectToBeLifted = RayResult.collider.gameObject;
            }
            else if (!ShopControler.InInventory && RayResult.collider.gameObject.tag == "ShopAK47")
            {
                ObjectToBeLifted = RayResult.collider.gameObject;
            }
        }                      
    }

    public void PickUpOther(GameObject ObjectToPickUp)
    {
        if (PlayerInventory)
        {
            ScrMass LootMass = ObjectToBeLifted.gameObject.GetComponent<ScrMass>();
            if (PlayerInventory.CurrentMass + LootMass.Mass <= PlayerInventory.MaxMass)
            {
                PickUpBullets(ObjectToPickUp);
                PlayerInventory.CurrentMass += LootMass.Mass;
                Destroy(ObjectToPickUp);
                Counter++;
            }
        }
        
        void PickUpBullets(GameObject ObjectToPickUp)
        {
            if (ObjectToPickUp.gameObject.tag == "Ammo9MM")
            {
                PlayerInventory.SlotsForBackPack.Add(ReferencesForLoots.ValueLoots["9MM"]);                
            }
            else if (ObjectToPickUp.gameObject.tag == "Ammo45_APC")
            {
                PlayerInventory.SlotsForBackPack.Add(ReferencesForLoots.ValueLoots["45ACP"]);
            }
            else if (ObjectToPickUp.gameObject.tag == "Ammo5_56MM")
            {
                PlayerInventory.SlotsForBackPack.Add(ReferencesForLoots.ValueLoots["5,56MM"]);
            }
            else if (ObjectToPickUp.gameObject.tag == "Ammo7_62MM")
            {
                PlayerInventory.SlotsForBackPack.Add(ReferencesForLoots.ValueLoots["7,62MM"]);
            }
        }
    }

    void PickUpEqipment(GameObject ObjectToPickUp)
    {
        if (ObjectToPickUp.CompareTag("Helmet") && Counter == 0)
        {
            SlotControler.MyHelmet = ObjectToBeLifted.transform;
            PutObjects(SlotControler.MyHelmet, SlotControler.SlotHelmet);
            Counter++;
        }
        else if (ObjectToPickUp.CompareTag("Armor") && Counter == 0)
        {
            SlotControler.MyArmor = ObjectToBeLifted.transform;
            PutObjects(SlotControler.MyArmor, SlotControler.SlotArmor);
            PutOn();
            Counter++;
        }
        else if (ObjectToPickUp.CompareTag("BackPack") && Counter == 0)
        {
            SlotControler.MyBackPack = ObjectToBeLifted.transform;
            PutObjects(SlotControler.MyBackPack, SlotControler.SlotBackPack);            
            PlayerInventory.BackPack = SlotControler.MyBackPack.gameObject;
            PlayerInventory.ChargingValueMaxMass();                        
            Counter++;
        }
        
        void PutOn()
        {
            ArmorControler Armor = SlotControler.MyArmor.GetComponent<ArmorControler>();    
            
            SlotControler.SlotShpo01 = Armor.SlotShop01;
            SlotControler.SlotShpo02 = Armor.SlotShop02;
            SlotControler.SlotShpo03 = Armor.SlotShop03;

            SlotControler.SlotPistol01 = Armor.SlotPistol01;
            SlotControler.SlotKnife01 = Armor.SlotKnife01;            
            
            if (SlotControler.MyShope01) PutObjects(SlotControler.MyShope01, SlotControler.SlotShpo01);           
            if (SlotControler.MyShope02) PutObjects(SlotControler.MyShope02, SlotControler.SlotShpo02);            
            if (SlotControler.MyShope03) PutObjects(SlotControler.MyShope03, SlotControler.SlotShpo03);            
            
            if (SlotControler.MyPistol01) PutObjects(SlotControler.MyPistol01, SlotControler.SlotPistol01);
            PutObjects(SlotControler.MyKnife01, SlotControler.SlotKnife01);
        }
    }

    public void PickUpWeapons(GameObject ObjectForPickUp)
    {
        Debug.Log("1");
        if (!SlotControler.MyPistol01 && MainCounter == 1 && Counter == 0)
        {
            //pistol.GetComponent<IImage>().GetImage(Sglock);
            
            SlotControler.MyPistol01 = ObjectToBeLifted.transform;
            PutObjects(SlotControler.MyPistol01, SlotControler.SlotPistol01);            
            Counter++;
            Debug.Log("2");
        }
        else if (!SlotControler.MyWeapon01 && !SlotControler.MyWeapon02 && MainCounter == 2 && Counter == 0)
        {
            /*
            if (ObjectForPickUp.tag == "M4")
            {
                weapon01.GetComponent<IImage>().GetImage(Sm4);
            }
            else if(ObjectForPickUp.tag == "AK47")
            {
                weapon01.GetComponent<IImage>().GetImage(Sakr47);
            }
            */
            SlotControler.MyWeapon01 = ObjectForPickUp.transform;
            PutObjects(SlotControler.MyWeapon01, SlotControler.SlotBack01);            
            Counter++;
            
        }
        else if (SlotControler.MyWeapon01 && !SlotControler.MyWeapon02 && MainCounter == 2 && Counter == 0)
        {
            /*
            if (ObjectForPickUp.tag == "M4")
            {
                weapon02.GetComponent<IImage>().GetImage(Sm4);
            }
            else if (ObjectForPickUp.tag == "AK47")
            {
                weapon02.GetComponent<IImage>().GetImage(Sakr47);
            }
            */
            SlotControler.MyWeapon02 = ObjectForPickUp.transform;
            PutObjects(SlotControler.MyWeapon02, SlotControler.SlotBack02);           
            Counter++;
        }                
        else if (Counter == 0)
        {
            Debug.Log("Cant take !");
        }
    }

    public void PickUpShops(GameObject ShopForPickUp)
    {
        
        if (!SlotControler.MyShope01 && SlotControler.SlotShpo01 && Counter == 0)
        {            
            ShopForPickUp.transform.position = ObjectToBeLifted.transform.position;
            ShopForPickUp.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyShope01 = ShopForPickUp.transform;
            PutObjects(SlotControler.MyShope01, SlotControler.SlotShpo01);            
            Counter++;            
        }
        else if (!SlotControler.MyShope02 && SlotControler.SlotShpo02 && Counter == 0)
        {
            ShopForPickUp.transform.position = ObjectToBeLifted.transform.position;
            ShopForPickUp.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyShope02 = ShopForPickUp.transform;
            PutObjects(SlotControler.MyShope02, SlotControler.SlotShpo02);            
            Counter++;        
        }
        else if (!SlotControler.MyShope03 && SlotControler.SlotShpo03 && Counter == 0)
        {
            ShopForPickUp.transform.position = ObjectToBeLifted.transform.position;
            ShopForPickUp.transform.rotation = ObjectToBeLifted.transform.rotation;

            SlotControler.MyShope03 = ShopForPickUp.transform;
            PutObjects(SlotControler.MyShope03, SlotControler.SlotShpo03);            
            Counter++;           
        }
        else if (true)
        {
            Debug.Log("Cant do this !");
        }              
    }
}