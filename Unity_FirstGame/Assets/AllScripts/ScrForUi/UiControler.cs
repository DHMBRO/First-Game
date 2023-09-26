using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class UiControler : MonoBehaviour
{
    [SerializeField] private UiInventory InventoryUi;
    [SerializeField] private GameManager ManagerToGame;

    [SerializeField] public Image Scope;
    [SerializeField] GameObject Inventory;
    [SerializeField] Camera CameraScr;
    [SerializeField] public bool InventoryIsOpen = false;

    [SerializeField] public Image SlotWeapon01;
    [SerializeField] public Image SlotWeapon02;
    [SerializeField] public Image SlotPistol01;

    [SerializeField] public Image SlotShop01;
    [SerializeField] public Image SlotShop02;
    [SerializeField] public Image SlotShop03;

    [SerializeField] public Image SlotHelmet;
    [SerializeField] public Image SlotArmor;
    [SerializeField] public Image SlotBackPack;

    [SerializeField] private TextMeshProUGUI CurrentMassInInventory;
    [SerializeField] private TextMeshProUGUI CurrentMaxMassInInventory;

    [SerializeField] private Inventory PlayerInventory;

    void Start()
    {
        InventoryIsOpen = false;
        if (Inventory) Inventory.SetActive(false);
        //if (!CameraScr) Debug.Log("Not set CameraScr");
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {   
            InventoryIsOpen = !InventoryIsOpen;
            Scope.enabled = !InventoryIsOpen;
            
            Inventory.SetActive(InventoryIsOpen);
            PrintUseMassAndMaxMass();

            CurrentMassInInventory.gameObject.SetActive(InventoryIsOpen);
            CurrentMaxMassInInventory.gameObject.SetActive(InventoryIsOpen);


            if (InventoryUi) InventoryUi.WriteSprite();
            if (ManagerToGame) ManagerToGame.DisActiveUD();

            if (InventoryIsOpen) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
            
        }
        

    }

    private void PrintUseMassAndMaxMass()
    {
        string CurrentMassS = PlayerInventory.CurrentMass.ToString();
        string MaxMassS = PlayerInventory.MaxMass.ToString();

        CurrentMassInInventory.text = "Current mass in inventory: " + CurrentMassS;
        CurrentMaxMassInInventory.text = "Max mass in inventory: " + MaxMassS;
        
    }


}
