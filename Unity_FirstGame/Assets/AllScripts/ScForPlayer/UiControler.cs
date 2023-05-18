using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControler : MonoBehaviour
{
    [SerializeField] Image Scope;

    [SerializeField] GameObject Inventory;

    [SerializeField] bool InventoryIsOpen = false;
    
    void Start()
    {
        InventoryIsOpen = false;
        Inventory.SetActive(false);
        Scope.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryIsOpen = !InventoryIsOpen;
            Scope.enabled = !InventoryIsOpen;
            Inventory.SetActive(InventoryIsOpen);
            if (InventoryIsOpen)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void PrintLoot()
    {

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