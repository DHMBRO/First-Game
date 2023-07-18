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

    [SerializeField] private List <Image> ImagesToSlots = new List<Image>();
    [SerializeField] private List<string> NameToObjects = new List<string>();

    [SerializeField] private List <float> ScaleSlotsToX = new List<float>();
    [SerializeField] private List <float> ScaleSlotsToY = new List<float>();
    
    [SerializeField] private Dictionary<string,int> KeyToScale = new Dictionary<string,int>();


    void Start()
    {
        for (int i = 0;i < NameToObjects.Count; i++)
        {
            KeyToScale.Add(NameToObjects[i],i);
            
            //Debug.Log("i:" + i);
            //Debug.Log("KeyToScale[NameToObjects[i]]:" + KeyToScale[NameToObjects[i]]);
        }

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