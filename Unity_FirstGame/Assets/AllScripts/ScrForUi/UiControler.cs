using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiControler : MonoBehaviour
{
    //References To Canvas Components
    [SerializeField] private UiInventoryOutPut InventoryUi;
    [SerializeField] private ButtonControler ManagerToGame;

    //References Ro Player Components
    [SerializeField] private Camera CameraScr;
    [SerializeField] private PlayerControler ControlerPlayer;
    [SerializeField] private Inventory PlayerInventory;
    [SerializeField] private GameObject Inventory;
    
    //References Interface Canvas
    [SerializeField] public Image Scope;
    [SerializeField] private GameObject IndexesTable;
    //Armor References
    [SerializeField] private GameObject[] ArmorPanels = new GameObject[3];
    [SerializeField] private Image[] ArmorIndexes = new Image[3];
    //Other
    [SerializeField] public bool InventoryIsOpen = false;
    [SerializeField] private TextMeshProUGUI CurrentMassInInventory;
    
    //Slots Weapon
    [SerializeField] public Image SlotWeapon01;
    [SerializeField] public Image SlotWeapon02;
    [SerializeField] public Image SlotPistol01;
    
    //Slots Shop
    [SerializeField] public Image SlotShop01;
    [SerializeField] public Image SlotShop02;
    [SerializeField] public Image SlotShop03;
    
    //Slots Equipment
    [SerializeField] public Image SlotHelmet;
    [SerializeField] public Image SlotArmor;
    [SerializeField] public Image SlotBackPack;

    
    void Start()
    {
        //Change value varriables
        InventoryIsOpen = false;
        
        //Control other game objects
        if (Inventory) Inventory.SetActive(InventoryIsOpen);
        Scope.gameObject.SetActive(InventoryIsOpen);
        
        //Use Methods
        InterfaceControler();
    }

    
    public void OpenOrCloseInventory()
    {
        InventoryIsOpen = !InventoryIsOpen;
        Scope.enabled = !InventoryIsOpen;

        Inventory.SetActive(InventoryIsOpen);
        IndexesTable.SetActive(!InventoryIsOpen);
        PrintUseMassAndMaxMass();

        CurrentMassInInventory.gameObject.SetActive(InventoryIsOpen);

        if (InventoryUi) InventoryUi.WriteSprite();
        if (ManagerToGame) ManagerToGame.DisActiveUD();

        if (InventoryIsOpen) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;

    }

    
    public void InterfaceControler()
    {
        //Audits
        if (!ControlerPlayer) { Debug.Log("Not set ControlerPlayer");  return; }
        for (int i = 0;i < ArmorPanels.Length;i++)
        {
            if (ArmorPanels[i] == null) { Debug.Log("Not the entire array (ArmorPanels) has valid values"); return; }
            if (ArmorIndexes[i] == null) { Debug.Log("Not the entire array (ArmorIndexes) has valid values"); return; }
        }
        
        ArmorControler ControlerArmor;
        
        if (ControlerPlayer.SlotControler.MyArmor)
        {
            ControlerArmor = ControlerPlayer.SlotControler.MyArmor.GetComponent<ArmorControler>();
            

            for(int i = 0;i < 3; i++)
            {
                bool On = true;
                
                if(i >= ControlerArmor.SlotsCanUse) On = false;

                ArmorPanels[i].SetActive(On);
            }

            for (int i = 0;i < ControlerArmor.ControlerArmorPlate.Count;i++)
            {
                ArmorIndexes[i].fillAmount = ControlerArmor.ControlerArmorPlate[i].CurrentHpUi;
            }

            

        }
        else for (int i = 0; i < ArmorPanels.Length; i++) ArmorPanels[i].SetActive(false);

    }


    private void PrintUseMassAndMaxMass()
    {
        string CurrentMassS;
        string MaxMassS;

        CurrentMassS = PlayerInventory.CurrentMass.ToString();
        MaxMassS = PlayerInventory.MaxMass.ToString();

        if (PlayerInventory.BackPack && !PlayerInventory.BackPackPlayer) PlayerInventory.BackPackPlayer = PlayerInventory.BackPack.GetComponent<BackPackContorler>();
        else if (PlayerInventory.BackPackPlayer)
        {
            CurrentMassS = PlayerInventory.CurrentMass.ToString();
            MaxMassS = PlayerInventory.BackPackPlayer.CurrentMaxMass.ToString();

        }
        
        CurrentMassInInventory.text = "Mass: " + CurrentMassS + "/" + MaxMassS;
        
    }


}

