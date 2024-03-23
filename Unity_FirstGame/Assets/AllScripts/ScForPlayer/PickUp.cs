using UnityEngine;
using System.Collections.Generic;

public class PickUp : MethodsFromDevelopers 
{
    [SerializeField] private Transform LocalObject;
    [SerializeField] bool CanWork = false;

    [SerializeField] private UiControler ControlerUi;
    [SerializeField] private UiInventoryOutPut InventoryUi;

    [SerializeField] private PlayerControler ControlerPlayer;
    [SerializeField] private Inventory PlayerInventory;
    
    [SerializeField] private ReferenseForAllLoot ReferencesForLoots;

    [SerializeField] List<string> TagsToPickup = new List<string>();
    
    //[SerializeField] public int Counter = 0;

    private SlotControler SlotControler;
    private DropControler ControlerToDrop;
    
    void Start()
    {
        //Get References
        SlotControler = GetComponent<SlotControler>();
        ControlerToDrop = GetComponent<DropControler>();
        ControlerPlayer = GetComponent<PlayerControler>();
        
        //Auditions
        if (!ControlerUi) Debug.Log("Not set ControlerUi");

        GetComponent<PlayerToolsToInteraction>().PlayerSetInteractionDelegat += ChekToInteraction;
        
    }

    public void Work()
    {
        if (LocalObject && CanWork)
        {
            ComplertingTheLink();
        }

    }

    private void ChekToInteraction(Transform GivenReference)
    {
        LocalObject = GivenReference;
        CanWork = true;
    }

    private void ComplertingTheLink()
    {
        ShootControler ControlerWeapon = LocalObject.GetComponent<ShootControler>();
        ShopControler ControlerShop = LocalObject.GetComponent<ShopControler>();

        HelmetControler ControlerHelmet = LocalObject.GetComponent<HelmetControler>();
        ArmorControler ControlerArmor = LocalObject.GetComponent<ArmorControler>();
        BackPackContorler ControlerBackPack = LocalObject.GetComponent<BackPackContorler>();

        ScrForAllLoot ScrLoot = LocalObject.GetComponent<ScrForAllLoot>();

        for (int i = 0; i < TagsToPickup.Count; i++)
        {
            if (LocalObject.tag == TagsToPickup[i])
            {
                if (ControlerWeapon && ControlerWeapon.TheGun == TypeWeapon.Weapon)
                {
                    PickUpWeapons(LocalObject);
                }
                else if (ControlerWeapon && ControlerWeapon.TheGun == TypeWeapon.Pistol)
                {
                    PickUpPistols(LocalObject);
                }
                else if (ControlerShop)
                {
                    PickUpShops(LocalObject);
                }
                else if (ControlerHelmet || ControlerArmor || ControlerBackPack)
                {
                    PickUpEqipment(LocalObject);
                    ControlerUi.InterfaceControler();
                }
                else if (ScrLoot)
                {
                    PickUpOther(LocalObject);
                }
            }
        }
    }


    

    public void PickUpOther(Transform ObjectToPickUp)
    {
        
        if (PlayerInventory)
        {
            ScrForAllLoot Loot = ObjectToPickUp.gameObject.GetComponent<ScrForAllLoot>();
            InfoWhatDoLoot InfoLoot = ObjectToPickUp.gameObject.GetComponent<InfoWhatDoLoot>();

            if (Loot && PlayerInventory.CurrentMass + Loot.Mass <= PlayerInventory.MaxMass && Loot.SpriteForLoot)
            {
                for (int i = 0; i < ReferencesForLoots.ReferencePrefabs.Count; i++)
                {

                    InfoWhatDoLoot LootFromList = ReferencesForLoots.ReferencePrefabs[i].GetComponent<InfoWhatDoLoot>();

                    if (InfoLoot.InfoTheObject == LootFromList.InfoTheObject)
                    {

                        ScrSaveAndGiveInfo ObjectToGet = new ScrSaveAndGiveInfo();

                        ObjectToGet.ObjectToInstantiate = ReferencesForLoots.ReferencePrefabs[i];
                        ObjectToGet.SaveInfo(ObjectToPickUp.gameObject);

                        PlayerInventory.InfoForSlots.Add(ObjectToGet);
                        PlayerInventory.CurrentMass += Loot.Mass;

                        for (int j = 0; j < InventoryUi.SpritesForBackPack.Count; j++)
                        {
                            if (InventoryUi.SpritesForBackPack[j] == PlayerInventory.None)
                            {
                                //Debug.Log("6");
                                InventoryUi.SpritesForBackPack[j] = Loot.SpriteForLoot;
                                break;
                            }
                        }
                        Destroy(ObjectToPickUp);
                    }
                }
            }
        }
    }


    void PickUpEqipment(Transform ObjectToPickUp)
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
            
            if (ControlerUi)
            {
                ControlerUi.InterfaceControler();
            }
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

            PutObjects(SlotControler.MyHelmet, SlotControler.SlotHelmet, false);
            
        }

        void ChangeHelmet()
        {
            if (ControlerToDrop && HelmetIsPickUped.LevelHelmet > HelmetIsUsing.LevelHelmet)
            {
                DropObjects(SlotControler.MyHelmet.transform, ControlerToDrop.PointForDrop, false);

                if (ControlerUi) ControlerUi.SlotHelmet.sprite = ScrForLoot.SpriteForLoot;
                else Debug.Log("Not set ControelrUi");

                HelmetIsUsing.Use = false;
                HelmetIsPickUped.Use = true;

                SlotControler.MyHelmet = ObjectToPickUp.transform;

                PutObjects(SlotControler.MyHelmet, SlotControler.SlotHelmet, false);

            }
        }

        void PickUpArmor()
        {
            Debug.Log("1");

            if (ControlerUi) ControlerUi.SlotArmor.sprite = ScrForLoot.SpriteForLoot;
            else Debug.Log("Not set ControlerUi");

            ArmorIsPickUped.ChangePosition(true);

            SlotControler.MyArmor = ObjectToPickUp.transform;
            
            PutObjects(SlotControler.MyArmor, SlotControler.SlotArmor, false);
            PutOn();

        }
        void ChangeArmor()
        {
            Debug.Log("2");
            if (ControlerToDrop && ArmorIsPickUped.LevelArmor > ArmorIsUsing.LevelArmor)
            {
                DropObjects(SlotControler.MyArmor.transform, ControlerToDrop.PointForDrop, false);
                
                if (ControlerUi) ControlerUi.SlotArmor.sprite = ScrForLoot.SpriteForLoot;
                else Debug.Log("Not set ControlerUi");

                ArmorIsUsing.ChangePosition(false);
                ArmorIsPickUped.ChangePosition(true);

                SlotControler.MyArmor = ObjectToPickUp.transform;
                
                PutObjects(SlotControler.MyArmor, SlotControler.SlotArmor, false);
                PutOn();
            }
        }

        void PickUpBackPack()
        {
            if (ControlerUi) ControlerUi.SlotBackPack.sprite = ScrForLoot.SpriteForLoot;
            else Debug.Log("Not set ControlerUi");

            SlotControler.MyBackPack = ObjectToPickUp.transform;
            PutObjects(SlotControler.MyBackPack, SlotControler.SlotBackPack, false);

            PlayerInventory.BackPack = SlotControler.MyBackPack.gameObject;
            PlayerInventory.ChargingValueMaxMass(BackPackIsPickUped);
        }

        void ChangeBackPack()
        {
            if (ControlerToDrop && BackPackIsPickUped.LevelBackPack > BackpackIsUsing.LevelBackPack)
            {
                DropObjects(SlotControler.MyBackPack.transform, ControlerToDrop.PointForDrop, false);

                if (ControlerUi) ControlerUi.SlotBackPack.sprite = ScrForLoot.SpriteForLoot;
                else Debug.Log("Not set ControlerUi");

                SlotControler.MyBackPack = ObjectToPickUp.transform;
                PutObjects(SlotControler.MyBackPack, SlotControler.SlotBackPack, false);
                
                PlayerInventory.BackPack = SlotControler.MyBackPack.gameObject;
                PlayerInventory.ChargingValueMaxMass(BackPackIsPickUped);
            }
        }

    }

    void PutOn()
    {
        //Debug.Log("Pick up armor is working !");


        if (SlotControler && !SlotControler.MyArmor) return;
        ArmorControler MyArmor = SlotControler.MyArmor.GetComponent<ArmorControler>();
        //Debug.Log("You did the (if) !");

        if (MyArmor && MyArmor.Use)
        {
            //SlotControler.SlotsShop[0] = MyArmor.SlotShop01;
            //SlotControler.SlotsShop[1] = MyArmor.SlotShop02;
            //SlotControler.SlotsShop[2] = MyArmor.SlotShop03;

            //SlotControler.SlotPistol01 = MyArmor.SlotPistol01;
            //SlotControler.SlotKnife01 = MyArmor.SlotKnife01;

            if (SlotControler.Shop[0]) PutObjects(SlotControler.Shop[0], MyArmor.SlotShop01, false);
            if (SlotControler.Shop[1]) PutObjects(SlotControler.Shop[1], MyArmor.SlotShop02, false);
            if (SlotControler.Shop[2]) PutObjects(SlotControler.Shop[2], MyArmor.SlotShop03, false);

            if (SlotControler.MyPistol01) PutObjects(SlotControler.MyPistol01, MyArmor.SlotPistol01, false);
            PutObjects(SlotControler.MyKnife01, MyArmor.SlotKnife01, false);
        }
        else Debug.Log("Not set Armor");
    }

    public void PickUpWeapons(Transform ObjectForPickUp)
    {
        ShootControler ControlerShoot = ObjectForPickUp.GetComponent<ShootControler>();
        ScrForAllLoot ScrForLoot = ObjectForPickUp.GetComponent<ScrForAllLoot>();

        
        if (ControlerShoot && ScrForLoot)
        {
            if (!SlotControler.MyWeapon01 && !SlotControler.MyWeapon02)
            {
                SlotControler.MyWeapon01 = ObjectForPickUp.transform;
                if(ControlerUi) ControlerUi.SlotWeapon01.sprite = ScrForLoot.SpriteForLoot;
                PutObjects(SlotControler.MyWeapon01, SlotControler.SlotBack01, false);
                return;
            }
            else if (SlotControler.MyWeapon01 && !SlotControler.MyWeapon02)
            {
                SlotControler.MyWeapon02 = ObjectForPickUp.transform;
                if(ControlerUi) ControlerUi.SlotWeapon02.sprite = ScrForLoot.SpriteForLoot;
                
                PutObjects(SlotControler.MyWeapon02, SlotControler.SlotBack02, false);
                return;
            }
            else 
            {
                Debug.Log("Cant take !");
            }
            
        }
        
    }

    public void PickUpPistols(Transform ObjectForPickUp)
    {
        ShootControler ControlerShoot = ObjectForPickUp.GetComponent<ShootControler>();
        ScrForAllLoot ScrForLoot = ObjectForPickUp.GetComponent<ScrForAllLoot>();

        
        if (ControlerShoot && ScrForLoot)
        {
            if (!SlotControler.MyPistol01)
            {
                SlotControler.MyPistol01 = ObjectForPickUp.transform;
                if (ControlerUi) ControlerUi.SlotPistol01.sprite = ScrForLoot.SpriteForLoot;

                PutObjects(SlotControler.MyPistol01, SlotControler.SlotPistol01, false);
                return;
            }
        }
        else 
        {
            Debug.Log("Cant take !");
        }
        PutOn();   
    }

    public void PickUpShops(Transform ShopForPickUp )
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

                ShopForPickUp.transform.position = LocalObject .transform.position;
                ShopForPickUp.transform.rotation = LocalObject .transform.rotation;

                SlotControler.Shop[0] = ShopForPickUp.transform;

                //Debug.Log("SlotControler.MyShope01: " + SlotControler.MyShope01);

                if (ControlerUi) ControlerUi.SlotShop01.sprite = ScrForLoot.SpriteForLoot;
                if (!ControlerShop.IsUsing) PutObjects(SlotControler.Shop[0], SlotControler.SlotsShop[0], false);
                ControlerShop.PutShopInParent();
                return;
            }
            else if (!SlotControler.Shop[1] && SlotControler.SlotsShop[1])
            {
                //Debug.Log("2");

                ShopForPickUp.transform.position = LocalObject .transform.position;
                ShopForPickUp.transform.rotation = LocalObject .transform.rotation;

                SlotControler.Shop[1] = ShopForPickUp.transform;
                if (ControlerUi) ControlerUi.SlotShop02.sprite = ScrForLoot.SpriteForLoot;

                if (!ControlerShop.IsUsing) PutObjects(SlotControler.Shop[1], SlotControler.SlotsShop[1], false);
                ControlerShop.PutShopInParent();
                return;
            }
            else if (!SlotControler.Shop[2] && SlotControler.SlotsShop[2])
            {
                //Debug.Log("3");

                ShopForPickUp.transform.position = LocalObject .transform.position;
                ShopForPickUp.transform.rotation = LocalObject .transform.rotation;
                
                SlotControler.Shop[2] = ShopForPickUp.transform;
                if (ControlerUi) ControlerUi.SlotShop03.sprite = ScrForLoot.SpriteForLoot;

                if (!ControlerShop.IsUsing) PutObjects(SlotControler.Shop[2], SlotControler.SlotsShop[2], false);
                ControlerShop.PutShopInParent();
                return;
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