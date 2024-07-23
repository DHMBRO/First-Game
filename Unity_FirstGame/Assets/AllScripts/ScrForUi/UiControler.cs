using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiControler : MonoBehaviour
{
    //References To Canvas Components
    [SerializeField] private UiInventoryOutPut InventoryUi;
    [SerializeField] private GameObject InterFace;
    [SerializeField] private ButtonControler ControlerButton;

    //References Ro Player Components
    [SerializeField] private Camera CameraScr;
    [SerializeField] private SlotControler ControlerSlots;
    [SerializeField] private Inventory PlayerInventory;
    [SerializeField] public GameObject Inventory;

    //References Interface Canvas
    [SerializeField] private Image Scope;
    [SerializeField] Image CurrentScopeWeapon;
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
    [SerializeField] public Image[] SlotShopUi = new Image[3];  
    [SerializeField] public Image SlotShop01;
    [SerializeField] public Image SlotShop02;
    [SerializeField] public Image SlotShop03;
    
    //Slots Equipment
    [SerializeField] public Image SlotHelmet;
    [SerializeField] public Image SlotArmor;
    [SerializeField] public Image SlotBackPack;

    
    void Start()
    {
        //Setup
        InventoryIsOpen = false;
        PlayerInventory.GetComponent<PlayerToolsToInteraction>().TryToInteractDelegadWithOutInput += TryInteract;
        ControlerButton.GetComponent<ButtonControler>();

        SlotShopUi[0] = SlotShop01;
        SlotShopUi[1] = SlotShop02;
        SlotShopUi[2] = SlotShop03;


        //Control other game objects
        if (Inventory) Inventory.SetActive(InventoryIsOpen);
        Scope.gameObject.SetActive(true);
        
        //Use Methods
        InterfaceControler();
    }

    public void TryInteract(Transform GivenReference)
    {
        ScrForAllLoot LocalScrForAllLoot = null;

        if (GivenReference)
        {
            LocalScrForAllLoot = GivenReference.GetComponent<ScrForAllLoot>();
            
            if (LocalScrForAllLoot)
            {
                UpdateNameOnTable(LocalScrForAllLoot);
            }
            else DeleteNameOnTable();
        }
        else DeleteNameOnTable();
    }

    public void OpenOrCloseInventory(bool SetOpen)
    {
        InventoryIsOpen = SetOpen;
        SetInventory();
    }

    public void OpenOrCloseInventory()
    {
        InventoryIsOpen = !InventoryIsOpen;
        SetInventory();
    }

    private  void SetInventory()
    {
        //InterFace.SetActive(!InventoryIsOpen);
        Inventory.SetActive(InventoryIsOpen);
        IndexesTable.SetActive(!InventoryIsOpen);
        PrintUseMassAndMaxMass();

        CurrentMassInInventory.gameObject.SetActive(InventoryIsOpen);

        if (InventoryUi) InventoryUi.WriteSprite();
        if (ControlerButton)
        {
            ControlerButton.SetDropButton(false);
            ControlerButton.DisActiveUD();
        }

        if (InventoryIsOpen) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
    }

    void UpdateNameOnTable(ScrForAllLoot LocalObjectScript)
    {
        if (!TableNameObjectForPickUp || !LocalObjectScript.ShowNameOfThisObject)
        {
            Debug.Log("Not set TableNameObjectForPickUp !");
            return;
        }
        TableNameObjectForPickUp.text = LocalObjectScript.NameOfThisObject;
        
    }
    
    public void UpdateCurrentScopeImage(Image NewScope)
    {
        CurrentScopeWeapon = NewScope;
    }

    public void DeleteNameOnTable()
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


    void PrintUseMassAndMaxMass()
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

