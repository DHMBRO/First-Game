using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiControler : MonoBehaviour
{
    //References To Canvas Components
    [SerializeField] private UiInventory InventoryUi;
    [SerializeField] private GameManager ManagerToGame;

    //References Ro Player Components
    [SerializeField] private Camera CameraScr;
    [SerializeField] private Inventory PlayerInventory;
    [SerializeField] private GameObject Inventory;
    [SerializeField] public ArmorControler ControlerArmor; 

    //References Indexes
    [SerializeField] public Image Scope;
    [SerializeField] private GameObject IndexesTable;
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
        InventoryIsOpen = false;
        if (Inventory) Inventory.SetActive(false);
        //if (!CameraScr) Debug.Log("Not set CameraScr");
        
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
