using UnityEngine;
using System.Collections.Generic;

public class PickUp : MethodsFromDevelopers 
{
    [SerializeField] public GameObject ObjectToBeLifted;
    [SerializeField] private Transform ObjecttoRay;
    
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
        if (!ControlerUi) Debug.Log("Not set ControlerUi");
    }

    public void RayForLoot()
    {                
        if (ObjecttoRay)
        {
            //Debug.Log("RayForLoot is work");
            Ray RayForPickUp = new Ray(ObjecttoRay.position, ObjecttoRay.forward);
            
            Debug.DrawRay(ObjecttoRay.position, ObjecttoRay.forward * DistanceForRay, Color.red);
            
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
                //Debug.Log(ScrLoot);
                
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
        
        if (PlayerInventory && Counter == 0)
        {
            ScrForAllLoot Loot = ObjectToPickUp.gameObject.GetComponent<ScrForAllLoot>();
            ScrInfoToLoot InfoLoot = ObjectToPickUp.gameObject.GetComponent<ScrInfoToLoot>();

            if (Loot && PlayerInventory.CurrentMass + Loot.Mass <= PlayerInventory.MaxMass && Loot.SpriteForLoot)
            {
                for (int i = 0; i < ReferencesForLoots.ReferencePrefabs.Count; i++)
                {
                    
                    ScrInfoToLoot LootFromList = ReferencesForLoots.ReferencePrefabs[i].GetComponent<ScrInfoToLoot>();

                    if (InfoLoot.InfoTheObject == LootFromList.InfoTheObject)
                    {
                        
                        InfoForLoot ObjectToGet = new InfoForLoot();

                        ObjectToGet.ObjectToInstantiate = ReferencesForLoots.ReferencePrefabs[i];
                        ObjectToGet.SaveInfo(ObjectToPickUp);

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
        ScrForAllLoot ScrForLoot = ObjectToPickUp.GetComponent<ScrForAllLoot>();

        HelmetControler HelmetIsPickUped = ObjectToPickUp.GetComponent<HelmetControler>();
        HelmetControler HelmetIsUsing;

        ArmorControler ArmorIsPickUped = ObjectToPickUp.GetComponent<ArmorControler>();
        ArmorControler ArmorIsUsing;

        BackPackContorler BackPackIsPickUped = ObjectToPickUp.GetComponent<BackPackContorler>();
        BackPackContorler BackpackIsUsing;


        if (ObjectToPickUp.CompareTag("Helmet"))
        {
            if (SlotControler.MyHelmet)
            {
                HelmetIsUsing = SlotControler.MyHelmet.GetComponent<HelmetControler>();
                ChangeHelmet();
            }
            else PickUpHelmet();
        }
        else if (ObjectToPickUp.CompareTag("Armor"))
        {
            if (SlotControler.MyArmor)
            {
                ArmorIsUsing = SlotControler.MyArmor.GetComponent<ArmorControler>();
                ChangeArmor();
            }
            else PickUpArmor();
        }
        else if (BackPackIsPickUped.CompareTag("BackPack"))
        {
            if (SlotControler.MyBackPack)
            {
                BackpackIsUsing = SlotControler.MyBackPack.GetComponent<BackPackContorler>();
                ChangeBackPack();
            }
            else PickUpBackPack();
        }   

        void PickUpHelmet()
        {
            if (ControlerUi) ControlerUi.SlotHelmet.sprite = ScrForLoot.SpriteForLoot;
            else Debug.Log("Not set ControlerUi");

            HelmetIsPickUped.Use = true;
            SlotControler.MyHelmet = ObjectToPickUp.transform;

            PutObjects(SlotControler.MyHelmet, SlotControler.SlotHelmet);
            
        }
        void ChangeHelmet()
        {
            if (ControlerToDrop && HelmetIsPickUped.LevelHelmet > HelmetIsUsing.LevelHelmet)
            {
                DropObjects(SlotControler.MyHelmet.transform, ControlerToDrop.PointForDrop);

                if (ControlerUi) ControlerUi.SlotHelmet.sprite = ScrForLoot.SpriteForLoot;
                else Debug.Log("Not set ControelrUi");

                HelmetIsUsing.Use = false;
                HelmetIsPickUped.Use = true;

                SlotControler.MyHelmet = ObjectToPickUp.transform;

                PutObjects(SlotControler.MyHelmet, SlotControler.SlotHelmet);

            }
        }

        void PickUpArmor()
        {
            if (ControlerUi) ControlerUi.SlotArmor.sprite = ScrForLoot.SpriteForLoot;
            else Debug.Log("Not set ControlerUi");

            ArmorIsPickUped.ChangePosition(true);

            SlotControler.MyArmor = ObjectToPickUp.transform;
            
            PutObjects(SlotControler.MyArmor, SlotControler.SlotArmor);
            PutOn();

        }
        void ChangeArmor()
        {
            if (ControlerToDrop && ArmorIsPickUped.LevelArmor > ArmorIsUsing.LevelArmor)
            {
                DropObjects(SlotControler.MyArmor.transform, ControlerToDrop.PointForDrop);

                if (ControlerUi) ControlerUi.SlotArmor.sprite = ScrForLoot.SpriteForLoot;
                else Debug.Log("Not set ControlerUi");

                ArmorIsUsing.ChangePosition(false);
                ArmorIsPickUped.ChangePosition(true);

                SlotControler.MyArmor = ObjectToPickUp.transform;
                
                PutObjects(SlotControler.MyArmor, SlotControler.SlotArmor);
            }
        }

        void PickUpBackPack()
        {
            if (ControlerUi) ControlerUi.SlotBackPack.sprite = ScrForLoot.SpriteForLoot;
            else Debug.Log("Not set ControlerUi");

            SlotControler.MyBackPack = ObjectToPickUp.transform;
            PutObjects(SlotControler.MyBackPack, SlotControler.SlotBackPack);

            PlayerInventory.BackPack = SlotControler.MyBackPack.gameObject;
            PlayerInventory.ChargingValueMaxMass(BackPackIsPickUped);
        }
        void ChangeBackPack()
        {
            if (ControlerToDrop && BackPackIsPickUped.LevelBackPack > BackpackIsUsing.LevelBackPack)
            {
                DropObjects(SlotControler.MyBackPack.transform, ControlerToDrop.PointForDrop);

                if (ControlerUi) ControlerUi.SlotBackPack.sprite = ScrForLoot.SpriteForLoot;
                else Debug.Log("Not set ControlerUi");

                SlotControler.MyBackPack = ObjectToPickUp.transform;
                PutObjects(SlotControler.MyBackPack, SlotControler.SlotBackPack);
                
                PlayerInventory.BackPack = SlotControler.MyBackPack.gameObject;
                PlayerInventory.ChargingValueMaxMass(BackPackIsPickUped);
            }
        }

    }

    void PutOn()
    {
        if (SlotControler && !SlotControler.MyArmor) return;

        ArmorControler Armor = SlotControler.MyArmor.GetComponent<ArmorControler>();
        Debug.Log(Armor);

        if (Armor && Armor.Use)
        {
            SlotControler.SlotsShop[0] = Armor.SlotShop01;
            SlotControler.SlotsShop[1] = Armor.SlotShop02;
            SlotControler.SlotsShop[2] = Armor.SlotShop03;

            SlotControler.SlotPistol01 = Armor.SlotPistol01;
            SlotControler.SlotKnife01 = Armor.SlotKnife01;

            if (SlotControler.Shop[0]) PutObjects(SlotControler.Shop[0], SlotControler.SlotsShop[0]);
            if (SlotControler.Shop[1]) PutObjects(SlotControler.Shop[1], SlotControler.SlotsShop[1]);
            if (SlotControler.Shop[2]) PutObjects(SlotControler.Shop[2], SlotControler.SlotsShop[2]);

            if (SlotControler.MyPistol01) PutObjects(SlotControler.MyPistol01, SlotControler.SlotPistol01);
            PutObjects(SlotControler.MyKnife01, SlotControler.SlotKnife01);
        }
        else Debug.Log("Not set Armor");
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
                if(ControlerUi) ControlerUi.SlotWeapon01.sprite = ScrForLoot.SpriteForLoot;
                

                PutObjects(SlotControler.MyWeapon01, SlotControler.SlotBack01);
                Counter = 1;
                //Debug.Log("1");

            }
            else if (SlotControler.MyWeapon01 && !SlotControler.MyWeapon02 && Counter == 0)
            {
                SlotControler.MyWeapon02 = ObjectForPickUp.transform;
                if(ControlerUi) ControlerUi.SlotWeapon02.sprite = ScrForLoot.SpriteForLoot;
                
                PutObjects(SlotControler.MyWeapon02, SlotControler.SlotBack02);
                Counter = 1;
                //Debug.Log("2");

            }
            else 
            {
                Debug.Log("Cant take !");
            }
            
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
                if (ControlerUi) ControlerUi.SlotPistol01.sprite = ScrForLoot.SpriteForLoot;

                PutObjects(SlotControler.MyPistol01, SlotControler.SlotPistol01);
                Counter = 1;
                //Debug.Log("3");
            }
        }
        else 
        {
            Debug.Log("Cant take !");
        }
        PutOn();   
    }

    public void PickUpShops(GameObject ShopForPickUp )
    {
        ShopControler ControlerShop = ShopForPickUp.GetComponent<ShopControler>();
        ScrForAllLoot ScrForLoot = ShopForPickUp.GetComponent<ScrForAllLoot>();

        //Debug.Log("PickUpShops is work");


        if (!ControlerShop && !ScrForLoot) return;

        //Debug.Log("0");

        if (!ControlerShop.InInventory)
        {
            if (!SlotControler.Shop[0] && SlotControler.SlotsShop[0])
            {
                //Debug.Log("1");

                ShopForPickUp.transform.position = ObjectToBeLifted.transform.position;
                ShopForPickUp.transform.rotation = ObjectToBeLifted.transform.rotation;

                SlotControler.Shop[0] = ShopForPickUp.transform;

                //Debug.Log("SlotControler.MyShope01: " + SlotControler.MyShope01);

                if (ControlerUi) ControlerUi.SlotShop01.sprite = ScrForLoot.SpriteForLoot;
                if (!ControlerShop.IsUsing) PutObjects(SlotControler.Shop[0], SlotControler.SlotsShop[0]);
                ControlerShop.PutShopInParent();
                Counter = 1;
            }
            else if (!SlotControler.Shop[1] && SlotControler.SlotsShop[1])
            {
                //Debug.Log("2");

                ShopForPickUp.transform.position = ObjectToBeLifted.transform.position;
                ShopForPickUp.transform.rotation = ObjectToBeLifted.transform.rotation;

                SlotControler.Shop[1] = ShopForPickUp.transform;
                if (ControlerUi) ControlerUi.SlotShop02.sprite = ScrForLoot.SpriteForLoot;

                if (!ControlerShop.IsUsing) PutObjects(SlotControler.Shop[1], SlotControler.SlotsShop[1]);
                ControlerShop.PutShopInParent();
                Counter = 1;
            }
            else if (!SlotControler.Shop[2] && SlotControler.SlotsShop[2])
            {
                //Debug.Log("3");

                ShopForPickUp.transform.position = ObjectToBeLifted.transform.position;
                ShopForPickUp.transform.rotation = ObjectToBeLifted.transform.rotation;
                
                SlotControler.Shop[2] = ShopForPickUp.transform;
                if (ControlerUi) ControlerUi.SlotShop03.sprite = ScrForLoot.SpriteForLoot;

                if (!ControlerShop.IsUsing) PutObjects(SlotControler.Shop[2], SlotControler.SlotsShop[2]);
                ControlerShop.PutShopInParent();
                Counter = 1;
            }
            else 
            {
                Debug.Log("Cant do this !");
            }


            //Debug.Log(!SlotControler.Shop[0]);
            //Debug.Log(SlotControler.SlotsShop[0]);
            //Debug.Log(Counter == 0);

            PutOn();
        }
    }
}