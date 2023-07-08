using UnityEngine;
using System.Collections.Generic;

public class SelectAnObject : MonoBehaviour
{
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
        IUsebleInterFace UseLoot = Inventory.SlotsForBackPack[UiInventory.Count + IndexToList].GetComponent<IUsebleInterFace>();
        if(UseLoot != null) UseLoot.Use();
        SelectObject();
    }

    public void Drop()
    {
        Debug.Log("3");
        IDrop DropLoot = Inventory.SlotsForBackPack[UiInventory.Count + IndexToList].GetComponent<IDrop>();
        if (DropLoot != null) DropLoot.Drop();
        SelectObject();
    }

}
