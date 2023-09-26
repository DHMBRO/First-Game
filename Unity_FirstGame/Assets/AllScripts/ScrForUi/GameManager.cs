using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MethodsFromDevelopers
{
    public GameObject SlotsPanel;
    public GameObject BackPackPanel;

    public GameObject WatchedSlot;
    public GameObject[] AllSlots = new GameObject[4];
    
    public GameObject ButtonUse;
    public GameObject ButtonDrop;
    public Sprite None;
        
    [SerializeField] private TextMeshProUGUI ObjectDescription;

    
    [SerializeField] private UiInventory InventoryUi;
    [SerializeField] Inventory PlayerInventory;


    private void Start()
    {
        if (InventoryUi.PlayerInventory) PlayerInventory = InventoryUi.PlayerInventory;
        else Debug.Log("Not set InventoryUi.PlayerInventory");

        InventoryUi = gameObject.GetComponent<UiInventory>();
        
        if (!InventoryUi) Debug.Log("Not set InventoryUi");

        if (WatchedSlot) WatchedSlot.SetActive(false);
        else Debug.Log("Not set WatchedSlot");
    }

    public void OpenSlots()
    {
        //Debug.Log("Slots is Open");
        if (BackPackPanel) BackPackPanel.SetActive(false);
        if (SlotsPanel) SlotsPanel.SetActive(true);
    }

    public void OpenInventory()
    {
        //Debug.Log("BackPack is Open");
        if (SlotsPanel) SlotsPanel.SetActive(false);
        if (BackPackPanel) BackPackPanel.SetActive(true);
    }

    public void ActiveUD(int IndexToSlots)
    {
        if (InventoryUi.SpritesForBackPack[InventoryUi.Count + IndexToSlots] == None)
        {
            Debug.Log("InventoryUi.Count :" + InventoryUi.Count);
            Debug.Log("PlayerInventory.InfoForSlots.Count :" + InventoryUi.SpritesForBackPack.Count);

            Debug.Log("Index was out of range !");
            return;
        }

        if (!ButtonUse || !ButtonDrop) return;

        if (WatchedSlot && IndexToSlots >= 0 || IndexToSlots  <= 3)
        {
            PutObjects(WatchedSlot.transform, AllSlots[IndexToSlots].transform);
            WatchedSlot.SetActive(true);

            Button ButtonU = ButtonUse.GetComponent<Button>();
            ButtonU.interactable = true;

            Button ButtonD = ButtonDrop.GetComponent<Button>();
            ButtonD.interactable = true;


            if (PlayerInventory.InfoForSlots[InventoryUi.Count + IndexToSlots].HaveDescription)
            {
                if (ObjectDescription)
                {
                    ObjectDescription.text = PlayerInventory.InfoForSlots[InventoryUi.Count + IndexToSlots].ObjectDescription;
                    Debug.Log(InventoryUi.PlayerInventory.InfoForSlots[InventoryUi.Count + IndexToSlots].ObjectToInstantiate.name);
                }
            }

            //Debug.Log("ActiveUD is work");
            
        }

        
    }


    public void DisActiveUD()
    {
        if (WatchedSlot && ButtonUse && ButtonDrop)
        {
            WatchedSlot.SetActive(false);
            
            Button ButtonU = ButtonUse.GetComponent<Button>();
            ButtonU.interactable = false;

            Button ButtonD = ButtonDrop.GetComponent<Button>();
            ButtonD.interactable = false;

            if (ObjectDescription) ObjectDescription.text = null;
        }

    }

}
