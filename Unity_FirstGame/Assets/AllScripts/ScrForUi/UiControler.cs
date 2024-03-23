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
    [SerializeField] private SlotControler ControlerSlots;
    [SerializeField] private Inventory PlayerInventory;
    [SerializeField] private GameObject Inventory;
    
    //References Interface Canvas
    [SerializeField] public Image Scope;
    [SerializeField] public TextMeshProUGUI TableNameObjectForPickUp;
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
        PlayerInventory.GetComponent<PlayerToolsToInteraction>().PlayerSetInteractionDelegat += ChekToInteraction;

        //Control other game objects
        if (Inventory) Inventory.SetActive(InventoryIsOpen);
        Scope.gameObject.SetActive(InventoryIsOpen);
        
        //Use Methods
        InterfaceControler();
    }

    private void ChekToInteraction(Transform GivenReference)
    {
        ScrForAllLoot LocalScrForAllLoot = GivenReference.GetComponent<ScrForAllLoot>();

        if (LocalScrForAllLoot)
        {
            UpdateNameOnTable(LocalScrForAllLoot);
        }
        else DeleteNameOnTable();
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

    

    private void UpdateNameOnTable(ScrForAllLoot LocalObjectScript)
    {
        if (!TableNameObjectForPickUp)
        {
            Debug.Log("Not set TableNameObjectForPickUp !");
            return;
        }
        TableNameObjectForPickUp.text = LocalObjectScript.NameOfThisObject;
        

    }
    
    private void DeleteNameOnTable()
    {
        if (!TableNameObjectForPickUp)
        {
            Debug.Log("Not set TableNameObjectForPickUp !");
            return;
        }
        else TableNameObjectForPickUp.text = " ";
    }

    public void InterfaceControler()
    {
        //Audits
        if (!ControlerSlots) { Debug.Log("Not set ControlerPlayer");  return; }
        for (int i = 0;i < ArmorPanels.Length;i++)
        {
            if (ArmorPanels[i] == null) { Debug.Log("Not the entire array (ArmorPanels) has valid values"); return; }
            if (ArmorIndexes[i] == null) { Debug.Log("Not the entire array (ArmorIndexes) has valid values"); return; }
        }
        
        ArmorControler ControlerArmor;
        
        if (ControlerSlots.MyArmor)
        {
            ControlerArmor = ControlerSlots.MyArmor.GetComponent<ArmorControler>();
            

            for(int i = 0;i < 3; i++)
            {
                bool On = true;
                
                if(i >= ControlerArmor.ArmorPlatesCanUse) On = false;

                ArmorPanels[i].SetActive(On);
            }

            for (int i = 0;i < ControlerArmor.ArmorPlatesCanUse+1;i++)
            {
                if (i < ControlerArmor.ControlerArmorPlates.Count && ControlerArmor.ControlerArmorPlates[i] != null)
                {
                    ArmorIndexes[i].fillAmount = ControlerArmor.ControlerArmorPlates[i].CurrentHp;
                }
                else if(i < ArmorIndexes.Length) ArmorIndexes[i].fillAmount = 0.0f;

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

