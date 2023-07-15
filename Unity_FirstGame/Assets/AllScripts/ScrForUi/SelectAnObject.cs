using UnityEngine;
using System.Collections.Generic;

public class SelectAnObject : MethodsFromDevelopers
{
    [SerializeField] private Transform SlotToUseLoot;
    [SerializeField] private UiInventory UiInventory;
    [SerializeField] private Inventory Inventory;
    [SerializeField] private DropControler ControlerToDrop;
 
    [SerializeField] int IndexToList;
    
    public void PrintIndexToList(int Index)
    {
        IndexToList = Index;
    }

    public void SelectObject()
    {
        if (UiInventory && Inventory && IndexToList >= 0 || IndexToList <= 3)
        {
            Inventory.SpritesForBackPack.RemoveAt(UiInventory.Count + IndexToList);
            Inventory.SlotsForBackPack.RemoveAt(UiInventory.Count + IndexToList);
            Inventory.SpritesForBackPack.Add(UiInventory.None);
            
            UiInventory.WriteSprite();
        }
        else if (!UiInventory) Debug.Log("Not set UiInventory");
        else if(!Inventory) Debug.Log("Not set Inventory");

    }

    public void Use()
    {
        Debug.Log("2");
        
        GameObject ObjectToUse = Instantiate(Inventory.SlotsForBackPack[UiInventory.Count + IndexToList].ObjectToInstantiate);
        IUsebleInterFace UseLoot = ObjectToUse.GetComponent<IUsebleInterFace>();

        if (SlotToUseLoot && UseLoot != null) PutObjects(ObjectToUse.transform, SlotToUseLoot);
        else if (UseLoot == null)
        {
            Debug.Log("Not set IUsebleInterFace");
            if (!SlotToUseLoot) Debug.Log("Not set SlotToUseLoot");
        }

        if (UseLoot != null)
        {
            UseLoot.Use(Inventory.gameObject, gameObject.GetComponent<SelectAnObject>());
            SelectObject();
        }
        
    }

    public void Drop()
    {
        Debug.Log("3");

        GameObject ObjectToDrop = Instantiate(Inventory.SlotsForBackPack[UiInventory.Count + IndexToList].ObjectToInstantiate);
        
        if (ControlerToDrop) DropObjects(ObjectToDrop.transform, ControlerToDrop.PointForDrop);
        else Debug.Log("Not set ControlerToDrop");
        
        SelectObject();
    }

}
