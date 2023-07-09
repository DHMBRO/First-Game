using UnityEngine;
using System.Collections.Generic;

public class SelectAnObject : MethodsFromDevelopers
{
    [SerializeField] private Transform SlotToUseLoot;
    [SerializeField] private UiInventory UiInventory;
    [SerializeField] private Inventory Inventory;
    

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
            Inventory.SpritesForBackPack.Add(UiInventory.None);
            Inventory.SlotsForBackPack.RemoveAt(UiInventory.Count + IndexToList);
            UiInventory.WriteSprite();
        }
        else if (!UiInventory) Debug.Log("Not set UiInventory");
        else if(!Inventory) Debug.Log("Not set Inventory");

    }

    public void Use()
    {
        Debug.Log("2");

        GameObject ObjectToUse = Instantiate(Inventory.SlotsForBackPack[UiInventory.Count + IndexToList]);
        IUsebleInterFace UseLoot = ObjectToUse.GetComponent<IUsebleInterFace>();

        if (SlotToUseLoot) PutObjects(ObjectToUse.transform, SlotToUseLoot);
        else if (!SlotToUseLoot) Debug.Log("Not set SlotToUseLoot");

        ScrForUseHeal ScrUseHeal  = ObjectToUse.GetComponent<ScrForUseHeal>();
        ScrUseHeal.ObjectToHeal = Inventory.ScrInfoForLoot.ObjectToHeal;

        if (UseLoot != null) UseLoot.Use();
        SelectObject();
    }

    public void Drop()
    {
        Debug.Log("3");

        GameObject ObjectToDrop = Instantiate(Inventory.SlotsForBackPack[UiInventory.Count + IndexToList]);
        DropObjects(ObjectToDrop.transform, Inventory.ScrInfoForLoot.PointToDrop);

        SelectObject();
    }

}
