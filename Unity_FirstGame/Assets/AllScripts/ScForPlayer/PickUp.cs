using UnityEngine;
using System.Collections.Generic;

public class PickUp : MethodsFromDevelopers 
{
    [SerializeField] public GameObject ObjectToBeLifted;
    [SerializeField] private CamFirstFace ReferenceForCamera;
    
    [SerializeField] private UiControler ControlerUi;
    [SerializeField] private UiInventory InventoryUi;

    [SerializeField] private Inventory PlayerInventory;
    [SerializeField] private ReferenseForAllLoot ReferencesForLoots;

    [SerializeField] List<string> TagsToPickup = new List<string>();

    private float DistanceForRay = 2.0f;
    private int MainCounter = 0;
    private int Counter = 0;

    private SlotControler SlotControler;
    private DropControler ControlerToDrop;
    
    void Start()
    {
        SlotControler = gameObject.GetComponent<SlotControler>();
        ControlerToDrop = gameObject.GetComponent<DropControler>();
        ReferenceForCamera = gameObject.GetComponent<CamFirstFace>();
        
       
        
    }

    public void RayForLoot()
    {                
        if (ReferenceForCamera)
        {
            Ray RayForPickUp = new Ray(ReferenceForCamera.ObjectRay.transform.position, ReferenceForCamera.ObjectRay.transform.forward);
            
            Debug.DrawRay(ReferenceForCamera.ObjectRay.position, ReferenceForCamera.ObjectRay.forward * DistanceForRay, Color.red);
            
            if (Physics.Raycast(RayForPickUp, out RaycastHit HitResult, DistanceForRay))
            {
                ComplertingTheLink();              
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
                ShootControler ControlerWeapon = ObjectToBeLifted.GetComponent<ShootControler>();
                ShopControler ControlerShop = ObjectToBeLifted.GetComponent<ShopControler>();

                HelmetControler ControlerHelmet = ObjectToBeLifted.GetComponent<HelmetControler>();
                ArmorControler ControlerArmor = ObjectToBeLifted.GetComponent<ArmorControler>();
                BackPackContorler ControlerBackPack = ObjectToBeLifted.GetComponent<BackPackContorler>();

                ScrForAllLoot ScrLoot = ObjectToBeLifted.GetComponent<ScrForAllLoot>();

                for (int i = 0; i < TagsToPickup.Count; i++)
                {
                    if (ObjectToBeLifted.tag == TagsToPickup[i])
                    {
                        if (ControlerWeapon)
                        {


                        }
                        else if (ControlerShop)
                        {

                        }
                        else if (ControlerHelmet)
                        {

                        }
                        else if (ControlerArmor)
                        {

                        }
                        else if (ControlerBackPack)
                        {

                        }
                        else if (ScrLoot)
                        {

                        }


                    }
                }


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
                if (ObjectToBeLifted.CompareTag("FirstAidKits") && Counter == 0)
                {
                    PickUpOther(ObjectToBeLifted);
                }
                //
                else if (ObjectToBeLifted.CompareTag("Ammo9MM") && Counter == 0)
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

    

    public void PickUpOther(GameObject ObjectToPickUp)
    {
        if (PlayerInventory)
        {
            ScrForAllLoot Loot = ObjectToBeLifted.gameObject.GetComponent<ScrForAllLoot>();
            
            if (Loot && PlayerInventory.CurrentMass + Loot.Mass <= PlayerInventory.MaxMass && Loot.SpriteForLoot)
            {
                
                for (int i = 0; i < ReferencesForLoots.ValueLoots.Count; i++)
                {
                    
                    if (ObjectToPickUp.gameObject.tag == ReferencesForLoots.ValueLoots[i].gameObject.tag)
                    {

                        InfoForLoot ObjectToGet = new InfoForLoot();
                        ObjectToGet.ObjectToInstantiate = ReferencesForLoots.ValueLoots[i];

                        
                        PlayerInventory.InfoForSlots.Add(ObjectToGet);
                        PlayerInventory.CurrentMass += Loot.Mass;
                        
                        for (int j = 0;j < InventoryUi.SpritesForBackPack.Count;j++)
                        {
                            if (InventoryUi.SpritesForBackPack[j] == PlayerInventory.None)
                            { 
                                InventoryUi.SpritesForBackPack[j] = Loot.SpriteForLoot;
                                break;
                            }
                        }

                        
                    }
                }
                Destroy(ObjectToPickUp);
                
                Counter++;
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
        else if (PlayerInventory && ObjectToPickUp.CompareTag("BackPack") && Counter == 0)
        {
            if (PlayerInventory.BackPack)
            {
                BackPackContorler BackPackControlerInInventory = SlotControler.MyBackPack.GetComponent<BackPackContorler>();
                BackPackContorler BackPackControlerToPickUp = ObjectToPickUp.gameObject.GetComponent<BackPackContorler>();
                
                if (BackPackControlerInInventory && BackPackControlerToPickUp)
                {
                    if (ControlerToDrop && BackPackControlerToPickUp.LevelBackPack > BackPackControlerInInventory.LevelBackPack)
                    {
                        DropObjects(SlotControler.MyBackPack.transform, ControlerToDrop.PointForDrop);

                        SlotControler.MyBackPack = ObjectToBeLifted.transform;
                        PutObjects(SlotControler.MyBackPack, SlotControler.SlotBackPack);

                        PlayerInventory.BackPack = SlotControler.MyBackPack.gameObject;
                        PlayerInventory.ChargingValueMaxMass();
                        Counter++;
                    }
                }
               
            }
            else
            {
                SlotControler.MyBackPack = ObjectToBeLifted.transform;
                PutObjects(SlotControler.MyBackPack, SlotControler.SlotBackPack);

                PlayerInventory.BackPack = SlotControler.MyBackPack.gameObject;
                PlayerInventory.ChargingValueMaxMass();
                
                Counter++;
                Debug.Log("6");
            }

            
        }
        
        void PutOn()
        {
            ArmorControler Armor = SlotControler.MyArmor.GetComponent<ArmorControler>();
            if (Armor && Armor.Use)
            {
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
    }


    public void PickUpWeapons(GameObject ObjectForPickUp)
    {
        ShootControler ControlerShoot = ObjectForPickUp.GetComponent<ShootControler>();
        ScrForAllLoot ScrForLoot = ObjectForPickUp.GetComponent<ScrForAllLoot>();


        if (ControlerShoot)
        {
            if (!SlotControler.MyPistol01 && MainCounter == 1 && Counter == 0)
            {
                SlotControler.MyPistol01 = ObjectForPickUp.transform;
                ControlerUi.SlotPistol01.sprite = ScrForLoot.SpriteForLoot;


                PutObjects(SlotControler.MyPistol01, SlotControler.SlotPistol01);
                Counter++;

            }
            else if (!SlotControler.MyWeapon01 && !SlotControler.MyWeapon02 && MainCounter == 2 && Counter == 0)
            {
                SlotControler.MyWeapon01 = ObjectForPickUp.transform;
                ControlerUi.SlotWeapon01.sprite = ScrForLoot.SpriteForLoot;


                PutObjects(SlotControler.MyWeapon01, SlotControler.SlotBack01);
                Counter++;

            }
            else if (SlotControler.MyWeapon01 && !SlotControler.MyWeapon02 && MainCounter == 2 && Counter == 0)
            {
                SlotControler.MyWeapon02 = ObjectForPickUp.transform;
                ControlerUi.SlotWeapon02.sprite = ScrForLoot.SpriteForLoot;


                PutObjects(SlotControler.MyWeapon02, SlotControler.SlotBack02);
                Counter++;
            }
            else if (Counter == 0)
            {
                Debug.Log("Cant take !");
            }
            if (ControlerShoot.WeaponShoop) PickUpShops(ControlerShoot.WeaponShoop);
            ControlerShoot.ControlerUi = ControlerUi;
        }          

    }

    public void PickUpShops(GameObject ShopForPickUp)
    {
        ShopControler ControlerShop = ShopForPickUp.GetComponent<ShopControler>();
        ScrForAllLoot ScrForLoot = ShopForPickUp.GetComponent<ScrForAllLoot>();

        
        if (ControlerShop && !ControlerShop.InInventory)
        {
            if (!SlotControler.MyShope01 && SlotControler.SlotShpo01)
            {
        
                ShopForPickUp.transform.position = ObjectToBeLifted.transform.position;
                ShopForPickUp.transform.rotation = ObjectToBeLifted.transform.rotation;
                
                SlotControler.MyShope01 = ShopForPickUp.transform;
                ControlerUi.SlotShop01.sprite = ScrForLoot.SpriteForLoot;

                if (!ControlerShop.IsUsing) PutObjects(SlotControler.MyShope01, SlotControler.SlotShpo01);
                //Counter++;
            }
            else if (!SlotControler.MyShope02 && SlotControler.SlotShpo02)
            {
                ShopForPickUp.transform.position = ObjectToBeLifted.transform.position;
                ShopForPickUp.transform.rotation = ObjectToBeLifted.transform.rotation;

                SlotControler.MyShope02 = ShopForPickUp.transform;
                ControlerUi.SlotShop02.sprite = ScrForLoot.SpriteForLoot;

                if (!ControlerShop.IsUsing) PutObjects(SlotControler.MyShope02, SlotControler.SlotShpo02);
                //Counter++;
            }
            else if (!SlotControler.MyShope03 && SlotControler.SlotShpo03)
            {
                ShopForPickUp.transform.position = ObjectToBeLifted.transform.position;
                ShopForPickUp.transform.rotation = ObjectToBeLifted.transform.rotation;
                
                SlotControler.MyShope03 = ShopForPickUp.transform;
                ControlerUi.SlotShop03.sprite = ScrForLoot.SpriteForLoot;

                if (!ControlerShop.IsUsing) PutObjects(SlotControler.MyShope03, SlotControler.SlotShpo03);
                //Counter++;
            }
            else if (true)
            {
                Debug.Log("Cant do this !");
            }

            Debug.Log(!SlotControler.MyShope01);
            Debug.Log(SlotControler.SlotShpo01);
            Debug.Log(Counter == 0);
        }
    }
}