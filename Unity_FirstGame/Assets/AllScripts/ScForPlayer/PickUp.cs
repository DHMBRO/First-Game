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
                ObjectToBeLifted = HitResult.collider.gameObject;
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
        //Debug.Log("1-0");
        if (ObjectToBeLifted)
        {
            //Debug.Log("1-1");
            if (Input.GetKeyDown(KeyCode.F) && ObjectToBeLifted && Counter == 0)
            {
                //Debug.Log("1-2");

                ShootControler ControlerWeapon = ObjectToBeLifted.GetComponent<ShootControler>();
                ShopControler ControlerShop = ObjectToBeLifted.GetComponent<ShopControler>();

                HelmetControler ControlerHelmet = ObjectToBeLifted.GetComponent<HelmetControler>();
                ArmorControler ControlerArmor = ObjectToBeLifted.GetComponent<ArmorControler>();
                BackPackContorler ControlerBackPack = ObjectToBeLifted.GetComponent<BackPackContorler>();

                ScrForAllLoot ScrLoot = ObjectToBeLifted.GetComponent<ScrForAllLoot>();

                
                for (int i = 0; i < TagsToPickup.Count; i++)
                {
                    //Debug.Log("For is work");
                    if (ObjectToBeLifted.tag == TagsToPickup[i])
                    {
                        //Debug.Log("1-3");
                        if (ControlerWeapon)
                        {
                            if (ControlerWeapon.TheGun == TypeWeapon.Weapon)
                            {
                                PickUpWeapons(ObjectToBeLifted);
                            }
                            else if (ControlerWeapon.TheGun == TypeWeapon.Pistol)
                            {
                                PickUpPistols(ObjectToBeLifted);
                            }
                        }
                        else if (ControlerShop)
                        {
                            PickUpShops(ObjectToBeLifted);
                        }
                        else if (ControlerHelmet || ControlerArmor || ControlerBackPack)
                        {
                            PickUpEqipment(ObjectToBeLifted);
                        }
                        else if (ScrLoot)
                        {
                            PickUpOther(ObjectToBeLifted);
                            Debug.Log("PickUpOther is work");
                        }
                    }   
                }
            }
            if (Input.GetKeyUp(KeyCode.F) && Counter == 1)
            {
                Counter = 0;
            }
            

        }
    }

    

    public void PickUpOther(GameObject ObjectToPickUp)
    {
        //Debug.Log("1");
        
        if (PlayerInventory && Counter == 0)
        {
            ScrForAllLoot Loot = ObjectToBeLifted.gameObject.GetComponent<ScrForAllLoot>();
            //Debug.Log("2");

            if (Loot && PlayerInventory.CurrentMass + Loot.Mass <= PlayerInventory.MaxMass && Loot.SpriteForLoot)
            {
                //Debug.Log("3");
                for (int i = 0; i < ReferencesForLoots.ReferencePrefabs.Count; i++)
                {
                    //Debug.Log("4");
                    ScrForAllLoot LootFromList = ReferencesForLoots.ReferencePrefabs[i].GetComponent<ScrForAllLoot>();

                    if (Loot == LootFromList)
                    {
                        Debug.Log("5");

                        InfoForLoot ObjectToGet = new InfoForLoot();
                        
                        ObjectToGet.ObjectToInstantiate = ReferencesForLoots.ReferencePrefabs[i];
                        
                        Debug.Log(ObjectToGet.ObjectToInstantiate);

                        PlayerInventory.InfoForSlots.Add(ObjectToGet);
                        PlayerInventory.CurrentMass += Loot.Mass;

                        for (int j = 0; j < InventoryUi.SpritesForBackPack.Count; j++)
                        {
                            if (InventoryUi.SpritesForBackPack[j] == PlayerInventory.None && Counter == 0)
                            {
                                //Debug.Log("6");
                                InventoryUi.SpritesForBackPack[j] = Loot.SpriteForLoot;
                                Counter = 1;
                                break;
                            }


                        }

                        Destroy(ObjectToPickUp);
                    }

                }

               
                
               
            }
        }
    }


    void PickUpEqipment(GameObject ObjectToPickUp)
    {
        if (ObjectToPickUp.CompareTag("Helmet"))
        {
            SlotControler.MyHelmet = ObjectToBeLifted.transform;
            PutObjects(SlotControler.MyHelmet, SlotControler.SlotHelmet);
            Counter = 1;
        }
        else if (ObjectToPickUp.CompareTag("Armor"))
        {
            SlotControler.MyArmor = ObjectToBeLifted.transform;
            PutObjects(SlotControler.MyArmor, SlotControler.SlotArmor);
            PutOn();
            Counter = 1;
        }
        else if (PlayerInventory && ObjectToPickUp.CompareTag("BackPack"))
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
                    }
                }
                
            }
            else
            {
                SlotControler.MyBackPack = ObjectToBeLifted.transform;
                PutObjects(SlotControler.MyBackPack, SlotControler.SlotBackPack);

                PlayerInventory.BackPack = SlotControler.MyBackPack.gameObject;
                PlayerInventory.ChargingValueMaxMass();
            }

            Counter = 1;
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

        
        if (ControlerShoot && ScrForLoot)
        {
            if (!SlotControler.MyWeapon01 && !SlotControler.MyWeapon02 && Counter == 0)
            {
                SlotControler.MyWeapon01 = ObjectForPickUp.transform;
                ControlerUi.SlotWeapon01.sprite = ScrForLoot.SpriteForLoot;
                
                PutObjects(SlotControler.MyWeapon01, SlotControler.SlotBack01);
                Counter = 1;
                //Debug.Log("1");

            }
            else if (SlotControler.MyWeapon01 && !SlotControler.MyWeapon02 && Counter == 0)
            {
                SlotControler.MyWeapon02 = ObjectForPickUp.transform;
                ControlerUi.SlotWeapon02.sprite = ScrForLoot.SpriteForLoot;
                
                PutObjects(SlotControler.MyWeapon02, SlotControler.SlotBack02);
                Counter = 1;
                //Debug.Log("2");

            }
            else 
            {
                Debug.Log("Cant take !");
            }
            if (ControlerShoot.WeaponShoop)
            {
                PickUpShops(ControlerShoot.WeaponShoop);
                //Debug.Log(ControlerShoot.WeaponShoop);

                //Debug.Log("1");
                
            }
            ControlerShoot.ControlerUi = ControlerUi;
        }          
    }

    public void PickUpPistols(GameObject ObjectForPickUp)
    {
        ShootControler ControlerShoot = ObjectForPickUp.GetComponent<ShootControler>();
        ScrForAllLoot ScrForLoot = ObjectForPickUp.GetComponent<ScrForAllLoot>();

        if (ControlerShoot && ScrForLoot)
        {
            if (!SlotControler.MyPistol01 && Counter == 0)
            {
                SlotControler.MyPistol01 = ObjectForPickUp.transform;
                ControlerUi.SlotPistol01.sprite = ScrForLoot.SpriteForLoot;

                PutObjects(SlotControler.MyPistol01, SlotControler.SlotPistol01);
                Counter = 1;
                //Debug.Log("3");
            }
        }
        else 
        {
            Debug.Log("Cant take !");
        }
        if (ControlerShoot.WeaponShoop) PickUpShops(ControlerShoot.WeaponShoop);
        ControlerShoot.ControlerUi = ControlerUi;
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
                Counter = 1;
            }
            else if (!SlotControler.MyShope02 && SlotControler.SlotShpo02)
            {
                ShopForPickUp.transform.position = ObjectToBeLifted.transform.position;
                ShopForPickUp.transform.rotation = ObjectToBeLifted.transform.rotation;

                SlotControler.MyShope02 = ShopForPickUp.transform;
                ControlerUi.SlotShop02.sprite = ScrForLoot.SpriteForLoot;

                if (!ControlerShop.IsUsing) PutObjects(SlotControler.MyShope02, SlotControler.SlotShpo02);
                Counter = 1;
            }
            else if (!SlotControler.MyShope03 && SlotControler.SlotShpo03)
            {
                ShopForPickUp.transform.position = ObjectToBeLifted.transform.position;
                ShopForPickUp.transform.rotation = ObjectToBeLifted.transform.rotation;
                
                SlotControler.MyShope03 = ShopForPickUp.transform;
                ControlerUi.SlotShop03.sprite = ScrForLoot.SpriteForLoot;

                if (!ControlerShop.IsUsing) PutObjects(SlotControler.MyShope03, SlotControler.SlotShpo03);
                Counter = 1;
            }
            else 
            {
                Debug.Log("Cant do this !");
            }

            
            //Debug.Log(!SlotControler.MyShope01);
            //Debug.Log(SlotControler.SlotShpo01);
            //Debug.Log(Counter == 0);

        }
    }
}