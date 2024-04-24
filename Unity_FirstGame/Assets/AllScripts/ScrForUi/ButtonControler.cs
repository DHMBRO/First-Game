using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonControler : MethodsFromDevelopers
{
    public GameObject SlotsPanel;
    public GameObject BackPackPanel;

    public GameObject WatchedSlot;
    public GameObject[] AllSlots = new GameObject[4];
    
    public GameObject ButtonUse;
    public GameObject ButtonDrop;
    public Sprite None;
    
    //All Panels
    [SerializeField] GameObject PanelInfoLoot;

    //Parameters && Description
    [SerializeField] private TextMeshProUGUI[] ObjectParameters = new TextMeshProUGUI[6];
    [SerializeField] private TextMeshProUGUI ObjectAmoutOfSomefing;
    [SerializeField] private TextMeshProUGUI ObjectDescription;
    //Refences To Player Components
    [SerializeField] private UiInventoryOutPut InventoryUi;
    [SerializeField] Inventory PlayerInventory;


    private void Start()
    {
        //First Comands
        if (InventoryUi.PlayerInventory) PlayerInventory = InventoryUi.PlayerInventory;
        else Debug.Log("Not set InventoryUi.PlayerInventory");

        if (PanelInfoLoot) PanelInfoLoot.SetActive(false);
        else Debug.Log("Not set GameManager.PanelInfoLoot");

        if (WatchedSlot) WatchedSlot.SetActive(false);
        else Debug.Log("Not set WatchedSlot");
        
        //Add Rferences

        InventoryUi = gameObject.GetComponent<UiInventoryOutPut>();
        if (!InventoryUi) Debug.Log("Not set InventoryUi");
        
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
        else PanelInfoLoot.SetActive(true);


        if (!ButtonUse || !ButtonDrop) return;

        if (WatchedSlot && IndexToSlots >= 0 || IndexToSlots  <= 3)
        {
            PutObjects(WatchedSlot.transform, AllSlots[IndexToSlots].transform, false);
            WatchedSlot.SetActive(true);

            Button ButtonU = ButtonUse.GetComponent<Button>();
            ButtonU.interactable = true;

            Button ButtonD = ButtonDrop.GetComponent<Button>();
            ButtonD.interactable = true;


            if (PlayerInventory.InfoForSlots[InventoryUi.Count + IndexToSlots].HaveDescription)
            {
                for (int i = 0; i < PlayerInventory.InfoForSlots[InventoryUi.Count + IndexToSlots].ObjectParemeters.Length && i < ObjectParameters.Length && ObjectParameters[i] != null; i++)
                {
                    ObjectParameters[i].text = PlayerInventory.InfoForSlots[InventoryUi.Count + IndexToSlots].ObjectParemeters[i];
                }
                ObjectDescription.text = PlayerInventory.InfoForSlots[InventoryUi.Count + IndexToSlots].ObjectDescription;
                
                //Show the Ammo
                if(!ObjectAmoutOfSomefing) { Debug.Log("Not set ObjectAmoutOfSomefing"); return; }

                if (PlayerInventory.InfoForSlots[InventoryUi.Count + IndexToSlots].ShowTheAmmo)
                {
                    ObjectAmoutOfSomefing.text = PlayerInventory.InfoForSlots[InventoryUi.Count + IndexToSlots].CurrentAmmoUi;

                }
                else ObjectAmoutOfSomefing.text = "Dont have it";

            }           
            
        }

        
    }


    public void DisActiveUD()
    {
        if (WatchedSlot && ButtonUse && ButtonDrop)
        {
            WatchedSlot.SetActive(false);
            PanelInfoLoot.SetActive(false);

            Button ButtonU = ButtonUse.GetComponent<Button>();
            ButtonU.interactable = false;

            Button ButtonD = ButtonDrop.GetComponent<Button>();
            ButtonD.interactable = false;

            for (int i = 0; i < ObjectParameters.Length && ObjectParameters[i] != null; i++) ObjectParameters[i].text = null;
            ObjectDescription.text = null;

        }

    }

}
