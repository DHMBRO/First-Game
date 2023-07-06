using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControler : MonoBehaviour
{
    [SerializeField] private CamFirstFace CameraPlayer;
    [SerializeField] private UiInventory InventoryUi;
    [SerializeField] private GameManager ManagerToGame;

    [SerializeField] Image Scope;
    [SerializeField] GameObject Inventory;
    [SerializeField] public bool InventoryIsOpen = false;

    [SerializeField] public Image SlotWeapon01;
    [SerializeField] public Image SlotWeapon02;
    [SerializeField] public Image SlotPistol01;

    [SerializeField] public Image SlotShop01;
    [SerializeField] public Image SlotShop02;
    [SerializeField] public Image SlotShop03;

    [SerializeField] public Image Loot01;
    
    [SerializeField] public Button Loot01Use;
    [SerializeField] public Button Loot01Drop;


    void Start()
    {
        InventoryIsOpen = false;
        if(Inventory) Inventory.SetActive(false);
        

        
        
    }

    void Update()
    {
        if (CameraPlayer && Input.GetKeyDown(KeyCode.I))
        {
            CameraPlayer.InventoryIsOpen = !CameraPlayer.InventoryIsOpen;
            InventoryIsOpen = !InventoryIsOpen;
            Scope.enabled = !InventoryIsOpen;
            Inventory.SetActive(InventoryIsOpen);
            
            if (InventoryUi) InventoryUi.WriteSprite();
            if (ManagerToGame) ManagerToGame.DisActiveUD();

            if (InventoryIsOpen) Cursor.lockState = CursorLockMode.Confined;
            else Cursor.lockState = CursorLockMode.Locked;

        }

    }
    
    
    /*[SerializeField] Image scopeUi;
    [SerializeField] public List<Image> inventorySlot1x1 = new List<Image>();
    [SerializeField] public List<Image> inventorySlot1x2 = new List<Image>();
    [SerializeField] public Dictionary<string, Image> inventorySlot = new Dictionary<string, Image>();

    public bool inventoryIsOpen;
    void Start()
    {
        inventoryIsOpen = false;
        scopeUi.enabled = true;
        for (int count = 0,i = 0; i < inventorySlot.Count; i++)
        {

            inventorySlot[];
        }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            scopeUi.enabled = !scopeUi.enabled;
            for (int i = 0; i < inventorySlot1x1.Count; i++)
            {
                inventorySlot1x1[i].enabled = !inventorySlot1x1[i].enabled;
            }
            for (int i = 0; i < inventorySlot1x2.Count; i++)
            {
                inventorySlot1x2[i].enabled = !inventorySlot1x2[i].enabled;
            }
            if (Cursor.lockState == CursorLockMode.Confined)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            inventoryIsOpen = !inventoryIsOpen;
        }
    }*/

    
}