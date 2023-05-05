using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControler : MonoBehaviour
{
    [SerializeField] Image Scope;
    
    [SerializeField] List<Image> AllLoot = new List<Image>();
    [SerializeField] List<Transform> AllSlots = new List<Transform>();

    [SerializeField] bool InventoryIsOpen = false;
    
    void Start()
    {
        if()
        {

        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            inventoryIsOpen = !InventoryIsOpen;
            Debug.Log(inventoryIsOpen);

            
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