using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class UiControler : MonoBehaviour
{
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

    
    void Start()
    {
        InventoryIsOpen = false;
        if (Inventory) Inventory.SetActive(false);   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {   
            InventoryIsOpen = !InventoryIsOpen;
            Scope.enabled = !InventoryIsOpen;
            Inventory.SetActive(InventoryIsOpen);
            
            if (InventoryUi) InventoryUi.WriteSprite();
            if (ManagerToGame) ManagerToGame.DisActiveUD();

            if (InventoryIsOpen) Cursor.lockState = CursorLockMode.Confined;
            else Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    
    
    
}