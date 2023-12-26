using UnityEngine;
using System.Collections.Generic;
public class UseAndDropTheLoot : MethodsFromDevelopers
{
    
    [SerializeField] private Transform SlotToUseLoot;
    [SerializeField] private UiInventoryOutPut UiInventory;
    [SerializeField] private Inventory Inventory;
    [SerializeField] public PlayerControler ControlerPlayer;
    [SerializeField] private DropControler ControlerToDrop;
        
    [SerializeField] int IndexToList;
    [SerializeField] public GameObject ObjectToUse;

    private void Start()
    {
        if(Inventory)
        {
            SlotControler ControlerSlots = Inventory.GetComponent<SlotControler>();
            PlayerControler ControlerPlayer = Inventory.GetComponent<PlayerControler>(); 
            
            if (ControlerSlots)
            {
                SlotToUseLoot = ControlerSlots.SlotHand;
                //Debug.Log(SlotToUseLoot);
            }
            if (!ControlerPlayer) Debug.Log("Not set ControlerPlayer");

        }

    }


    public void PrintIndexToList(int Index)
    {
        IndexToList = Index;
    }

    public void DeleteReferenceToLoot()
    {
        if (UiInventory && Inventory && IndexToList >= 0 || IndexToList <= 3)
        {
            if (UiInventory.Count + IndexToList < UiInventory.SpritesForBackPack.Count) UiInventory.SpritesForBackPack.RemoveAt(UiInventory.Count + IndexToList);
            else Debug.Log("Index was out of range");
            
            if(UiInventory.Count + IndexToList < Inventory.InfoForSlots.Count) Inventory.InfoForSlots.RemoveAt(UiInventory.Count + IndexToList);
            else Debug.Log("Index was out of range");

            UiInventory.SpritesForBackPack.Add(UiInventory.None);
            UiInventory.WriteSprite();
        }
        else if (!UiInventory) Debug.Log("Not set UiInventory");
        else if(!Inventory) Debug.Log("Not set Inventory");

    }

    public void Use()
    {        
        GameObject ObjectToUse = Instantiate(Inventory.InfoForSlots[UiInventory.Count + IndexToList].ObjectToInstantiate);
        if (Inventory.InfoForSlots[UiInventory.Count + IndexToList] != null) Inventory.InfoForSlots[UiInventory.Count + IndexToList].GetInfo(ObjectToUse);

        //Debug.Log("Cordinates: " + ObjectToUse.transform.position.x + " to x " + ObjectToUse.transform.position.y + " to y " + ObjectToUse.transform.position.z + " to z ");

        Rigidbody RigObjectToUse = ObjectToUse.GetComponent<Rigidbody>();
        if (RigObjectToUse) Destroy(RigObjectToUse);
        
        ScrForAllLoot ScrForLoot = ObjectToUse.GetComponent<ScrForAllLoot>();
        IUsebleInterFace UseLoot = ObjectToUse.GetComponent<IUsebleInterFace>();

        if (UseLoot != null && ControlerPlayer)
        {
            if (UseLoot.Audit(Inventory.gameObject, Inventory.InfoForSlots[UiInventory.Count + IndexToList], GetComponent<UseAndDropTheLoot>()))
            {
                UseLoot.Use(Inventory.gameObject, Inventory.InfoForSlots[UiInventory.Count + IndexToList], GetComponent<UseAndDropTheLoot>());
                this.ObjectToUse = ObjectToUse;

            }
            else Debug.Log("Use it now doesn't have meaning !");

            Debug.Log(UseLoot.Audit(Inventory.gameObject, Inventory.InfoForSlots[UiInventory.Count + IndexToList], GetComponent<UseAndDropTheLoot>()));


            if (ScrForLoot.CanCombining && Inventory.InfoForSlots.Count > (UiInventory.Count + IndexToList)) 
            {
                CombiningLoot(Inventory.InfoForSlots[UiInventory.Count + IndexToList]);
                
                ScrForUseAmmo ScrAmmo = Inventory.InfoForSlots[UiInventory.Count + IndexToList].ObjectToInstantiate.GetComponent<ScrForUseAmmo>();

                //Debug.Log(Inventory.InfoForSlots[UiInventory.Count + IndexToList].CurrentAmmo);
                //Debug.Log(ScrAmmo.CurrentAmmo);


            }

            Inventory.ChangeMassInInventory();
            UiInventory.WriteSprite();

            //Debug.Log("InfoForSlots.Count: " + Inventory.InfoForSlots.Count);
            //Debug.Log("SpritesForBackPack.Count: " + UiInventory.SpritesForBackPack.Count);

        }
    }

    public void PutLoot(Transform LootToPut)
    {
        PutObjects(LootToPut, SlotToUseLoot, false);
    }


    public void Drop()
    {
        //Debug.Log("3");

        GameObject ObjectToDrop = Instantiate(Inventory.InfoForSlots[UiInventory.Count + IndexToList].ObjectToInstantiate);
        if(Inventory.InfoForSlots[UiInventory.Count + IndexToList] != null) Inventory.InfoForSlots[UiInventory.Count + IndexToList].GetInfo(ObjectToDrop);

        //Debug.Log("Cordinates: " + ObjectToDrop.transform.position.x + " to x " + ObjectToDrop.transform.position.y + " to y " + ObjectToDrop.transform.position.z + " to z ");

        if (ControlerToDrop) DropObjects(ObjectToDrop.transform, ControlerToDrop.PointForDrop, false);
        else Debug.Log("Not set ControlerToDrop");


        DeleteReferenceToLoot();
        Inventory.ChangeMassInInventory();
        
        //Debug.Log("InfoForSlots.Count"  + Inventory.InfoForSlots.Count);
        
    }


    private void CombiningLoot(ScrSaveAndGiveInfo ObjectToUse)
    {
        
        List<ScrForAllLoot> ScrForAllLoot = new List<ScrForAllLoot>();
        List<InfoWhatDoLoot> ScrInfoToLoot = new List<InfoWhatDoLoot>();
        List<ScrSaveAndGiveInfo> ScrInfoForLoot = new List<ScrSaveAndGiveInfo>();

        InfoWhatDoLoot ScrInfoToObjectUse = ObjectToUse.ObjectToInstantiate.GetComponent<InfoWhatDoLoot>();
        ScrForAllLoot ScrForAllLootOBG = ObjectToUse.ObjectToInstantiate.GetComponent<ScrForAllLoot>();

        if (!ScrInfoToObjectUse)
        {
            Debug.Log("Not set ScrInfoToObjectUse");
            return;
        }


        for (int i = 0;i < Inventory.InfoForSlots.Count; i++)
        {
            ScrForAllLoot.Add(Inventory.InfoForSlots[i].ObjectToInstantiate.GetComponent<ScrForAllLoot>());
            ScrInfoToLoot.Add(Inventory.InfoForSlots[i].ObjectToInstantiate.GetComponent<InfoWhatDoLoot>());
            ScrInfoForLoot.Add(Inventory.InfoForSlots[i]);

            //Debug.Log(Inventory.InfoForSlots[i].CurrentAmmo);    
            
        }


        for (int i = 0;i < ScrForAllLoot.Count;i++)
        {
            if (ScrInfoToLoot[i].InfoTheObject != ScrInfoToObjectUse.InfoTheObject)
            {
                ScrForAllLoot.RemoveAt(i);
                ScrInfoToLoot.RemoveAt(i);
                ScrInfoForLoot.RemoveAt(i);
                i--;
                //Debug.Log("ScrInfoToLoot[i].InfoTheObject != ScrInfoToObjectUse.InfoTheObject is true");
            }
        }
        
        
        for (int i = 0;i < ScrForAllLoot.Count;i++)
        {
            if (!ScrForAllLoot[i].CanCombining)
            {
                ScrForAllLoot.RemoveAt(i);
                ScrInfoToLoot.RemoveAt(i);
                ScrInfoForLoot.RemoveAt(i);
                //Debug.Log(" ObjectToUse.CanCombining is false");
            }
        }


        int SumeAmmo = 0;

        for (int i = 0; i < ScrForAllLoot.Count; i++)
        {
            ScrForUseAmmo CurrentAmmoScript = ScrForAllLoot[i].GetComponent<ScrForUseAmmo>();
            if (!CurrentAmmoScript || ScrInfoForLoot[i].CurrentAmmo == CurrentAmmoScript.MaxAmmo)
            {
                ScrForAllLoot.RemoveAt(i);
                ScrInfoToLoot.RemoveAt(i);
                ScrInfoForLoot.RemoveAt(i);
            }
            else 
            {
                SumeAmmo += ScrInfoForLoot[i].CurrentAmmo;
                ScrInfoForLoot[i].CurrentAmmo = 0;
            }
        }

        for (int i = 0; i < ScrForAllLoot.Count; i++)
        {
            ScrForUseAmmo CurrentAmmoScript = ScrForAllLoot[i].GetComponent<ScrForUseAmmo>();
            if (SumeAmmo > 0)
            {
                if (SumeAmmo >= CurrentAmmoScript.MaxAmmo)
                {
                    ScrInfoForLoot[i].CurrentAmmo = CurrentAmmoScript.MaxAmmo;
                    SumeAmmo -= CurrentAmmoScript.MaxAmmo;
                }   
                else 
                {
                    ScrInfoForLoot[i].CurrentAmmo = SumeAmmo;
                    SumeAmmo = 0;
                }

            }
            else
            {
                for (int j = 0;j < Inventory.InfoForSlots.Count; j++)
                {
                    InfoWhatDoLoot ScrInfoLoot = Inventory.InfoForSlots[j].ObjectToInstantiate.GetComponent<InfoWhatDoLoot>();

                    if (ScrInfoLoot.InfoTheObject == ScrInfoToObjectUse.InfoTheObject && Inventory.InfoForSlots[j].CurrentAmmo == 0)
                    {
                        //Debug.Log("J: " + j);
                        //Debug.Log("CurrentAmmo: " + Inventory.InfoForSlots[j].CurrentAmmo);

                        Inventory.InfoForSlots.RemoveAt(j);                        
                    }
                }
            }
        }



    }


}
