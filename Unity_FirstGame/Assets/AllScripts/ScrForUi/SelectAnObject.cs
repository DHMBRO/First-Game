using UnityEngine;

public class SelectAnObject : MethodsFromDevelopers
{
    [SerializeField] private Transform SlotToUseLoot;
    [SerializeField] private UiInventory UiInventory;
    [SerializeField] private Inventory Inventory;
    [SerializeField] private DropControler ControlerToDrop;
 
    [SerializeField] int IndexToList;

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
        //Debug.Log("2");
        
        GameObject ObjectToUse = Instantiate(Inventory.InfoForSlots[UiInventory.Count + IndexToList].ObjectToInstantiate);
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

            Inventory.ChangeMassInInventory();
            SelectObject();
            Inventory.ChangeMassInInventory();
            
        }

        
    }

    public void Drop()
    {
        //Debug.Log("3");

        GameObject ObjectToDrop = Instantiate(Inventory.InfoForSlots[UiInventory.Count + IndexToList].ObjectToInstantiate);
        ScrForAllLoot ScrLootObjectToDrop = ObjectToDrop.GetComponent<ScrForAllLoot>();

        if (!ScrLootObjectToDrop) return;
        Inventory.CurrentMass -= ScrLootObjectToDrop.Mass;


        if (ControlerToDrop) DropObjects(ObjectToDrop.transform, ControlerToDrop.PointForDrop);
        else Debug.Log("Not set ControlerToDrop");

        Inventory.ChangeMassInInventory();
        SelectObject();
        Inventory.ChangeMassInInventory();        
    }

}
