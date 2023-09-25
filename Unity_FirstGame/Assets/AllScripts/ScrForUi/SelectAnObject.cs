using UnityEngine;
using System.Collections.Generic;
public class SelectAnObject : MethodsFromDevelopers
{
    [SerializeField] private Transform SlotToUseLoot;
    [SerializeField] private UiInventory UiInventory;
    [SerializeField] private Inventory Inventory;
    [SerializeField] private DropControler ControlerToDrop;
        
    [SerializeField] int IndexToList;
    [SerializeField] GameObject ObjectToUse;

    private void Start()
    {
        if(Inventory)
        {
            SlotControler ControlerSlots = Inventory.GetComponent<SlotControler>();
            if (ControlerSlots)
            {
                SlotToUseLoot = ControlerSlots.SlotHand;
                //Debug.Log(SlotToUseLoot);
            }
            
        }

    }

    private void Update()
    {
        if (ObjectToUse) Debug.Log(ObjectToUse.name);
        else Debug.Log("Not set ObjectToUse");
    }

    public void PrintIndexToList(int Index)
    {
        IndexToList = Index;
    }

    public void SelectObject()
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
        Inventory.InfoForSlots[UiInventory.Count + IndexToList].GetInfo(ObjectToUse);

        //Debug.Log("Cordinates: " + ObjectToUse.transform.position.x + " to x " + ObjectToUse.transform.position.y + " to y " + ObjectToUse.transform.position.z + " to z ");

        Rigidbody RigObjectToUse = ObjectToUse.GetComponent<Rigidbody>();
        if (RigObjectToUse) Destroy(RigObjectToUse);
        
        ScrForAllLoot ScrForLoot = ObjectToUse.GetComponent<ScrForAllLoot>();
        IUsebleInterFace UseLoot = ObjectToUse.GetComponent<IUsebleInterFace>();

        if (UseLoot != null)
        {
            UseLoot.Use(Inventory.gameObject, Inventory.InfoForSlots[UiInventory.Count + IndexToList] , gameObject.GetComponent<SelectAnObject>());
            this.ObjectToUse = ObjectToUse;

            

            if (ScrForLoot.CanCombining) 
            {
                CombiningLoot(Inventory.InfoForSlots[UiInventory.Count + IndexToList]);
                
                ScrForUseAmmo ScrAmmo = Inventory.InfoForSlots[UiInventory.Count + IndexToList].ObjectToInstantiate.GetComponent<ScrForUseAmmo>();

                Debug.Log(Inventory.InfoForSlots[UiInventory.Count + IndexToList].CurrentAmmo);
                Debug.Log(ScrAmmo.CurrentAmmo);


            }


            Inventory.ChangeMassInInventory();
            UiInventory.WriteSprite();

            //Debug.Log("InfoForSlots.Count: " + Inventory.InfoForSlots.Count);
            //Debug.Log("SpritesForBackPack.Count: " + UiInventory.SpritesForBackPack.Count);

        }
    }

    public void PutLoot(Transform LootToPut)
    {
        PutObjects(LootToPut, SlotToUseLoot);
    }


    public void Drop()
    {
        //Debug.Log("3");

        GameObject ObjectToDrop = Instantiate(Inventory.InfoForSlots[UiInventory.Count + IndexToList].ObjectToInstantiate);
        Inventory.InfoForSlots[UiInventory.Count + IndexToList].GetInfo(ObjectToDrop);

        //Debug.Log("Cordinates: " + ObjectToDrop.transform.position.x + " to x " + ObjectToDrop.transform.position.y + " to y " + ObjectToDrop.transform.position.z + " to z ");

        if (ControlerToDrop) DropObjects(ObjectToDrop.transform, ControlerToDrop.PointForDrop);
        else Debug.Log("Not set ControlerToDrop");

        
        SelectObject();
        Inventory.ChangeMassInInventory();
        
        //Debug.Log("InfoForSlots.Count"  + Inventory.InfoForSlots.Count);
        
    }


    private void CombiningLoot(InfoForLoot ObjectToUse)
    {
        
        List<ScrForAllLoot> ScrForAllLoot = new List<ScrForAllLoot>();
        List<ScrInfoToLoot> ScrInfoToLoot = new List<ScrInfoToLoot>();
        List<InfoForLoot> ScrInfoForLoot = new List<InfoForLoot>();

        ScrInfoToLoot ScrInfoToObjectUse = ObjectToUse.ObjectToInstantiate.GetComponent<ScrInfoToLoot>();
        ScrForAllLoot ScrForAllLootOBG = ObjectToUse.ObjectToInstantiate.GetComponent<ScrForAllLoot>();

        if (!ScrInfoToObjectUse)
        {
            Debug.Log("Not set ScrInfoToObjectUse");
            return;
        }


        for (int i = 0;i < Inventory.InfoForSlots.Count; i++)
        {
            ScrForAllLoot.Add(Inventory.InfoForSlots[i].ObjectToInstantiate.GetComponent<ScrForAllLoot>());
            ScrInfoToLoot.Add(Inventory.InfoForSlots[i].ObjectToInstantiate.GetComponent<ScrInfoToLoot>());
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
                    ScrInfoToLoot ScrInfoLoot = Inventory.InfoForSlots[j].ObjectToInstantiate.GetComponent<ScrInfoToLoot>();

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
