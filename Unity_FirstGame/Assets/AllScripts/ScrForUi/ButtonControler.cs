using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonControler : MethodsFromDevelopers
{
    public GameObject SlotsPanel;
    public GameObject BackPackPanel;

    public GameObject WatchedSlot;
    public GameObject WatchedSlot02;
    
    public GameObject[] AllSlotsInPlayer = new GameObject[4];
    public GameObject[] AllSlotsOnPlayer = new GameObject[9];
    int IndexToList;

    public GameObject ButtonUse;
    public GameObject ButtonDrop;
    public GameObject ButtonDrop02;
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
    
    SlotControler ControlerSlot;
    DropControler ControlerDrop;

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
        ControlerSlot = PlayerInventory.GetComponent<SlotControler>();
        ControlerDrop = ControlerSlot.GetComponent<DropControler>();

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
            Debug.Log(WatchedSlot.name);
            Debug.Log(AllSlotsInPlayer[IndexToSlots].name);

            PutObjects(WatchedSlot.transform, AllSlotsInPlayer[IndexToSlots].transform, false);
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
            WatchedSlot02.SetActive(false);
            PanelInfoLoot.SetActive(false);

            Button ButtonU = ButtonUse.GetComponent<Button>();
            ButtonU.interactable = false;

            Button ButtonD = ButtonDrop.GetComponent<Button>();
            ButtonD.interactable = false;

            for (int i = 0; i < ObjectParameters.Length && ObjectParameters[i] != null; i++) ObjectParameters[i].text = null;
            ObjectDescription.text = null;

        }

    }
    
    public void SelectTheObject(int Index)
    {
        if (!WatchedSlot && !WatchedSlot02 && ButtonDrop02)
        {
            return;
        }

        if (AllSlotsOnPlayer[Index].GetComponent<Image>().sprite != None)
        {
            if (Index < 2)
            {
                PutObjects(WatchedSlot02.transform, AllSlotsOnPlayer[Index].transform, false);
                
                WatchedSlot02.SetActive(true);
                WatchedSlot.SetActive(false);
            }
            else if (Index >= 2)
            {
                PutObjects(WatchedSlot.transform, AllSlotsOnPlayer[Index].transform, false);
                
                WatchedSlot.SetActive(true);
                WatchedSlot02.SetActive(false);
            }
            
            IndexToList = Index;
            ButtonDrop02.GetComponent<Button>().interactable = true;
        }

    }

    

    public void SetDropButton(bool Enable)
    {
        if (!ButtonDrop02)
        {
            return;
        }
        
        ButtonDrop02.GetComponent<Button>().interactable = Enable;
    }
    
    public void DropSelectedObject()
    {
        if(!ControlerDrop)
        {
            return;
        }

        Transform SelectedObject = null;

        if(IndexToList < 3)
        {
            if (IndexToList == 0)
            {
                SelectedObject = ControlerSlot.MyWeapon01;
                ControlerSlot.MyWeapon01 = null;
            }
            else if (IndexToList == 1)
            {
                SelectedObject = ControlerSlot.MyWeapon02;
                ControlerSlot.MyWeapon02 = null;
            }
            else if (IndexToList == 2)
            {
                SelectedObject = ControlerSlot.MyPistol01;
                ControlerSlot.MyPistol01 = null;
            } 
        }
        else if (IndexToList > 2 && IndexToList < 6)
        {
            SelectedObject = ControlerSlot.Shop[IndexToList - 3];
            ControlerSlot.Shop[IndexToList - 3] = null; 
        }
        else
        {
            if (IndexToList == 6)
            {
                SelectedObject = ControlerSlot.MyHelmet;
                ControlerSlot.MyHelmet = null;
                ControlerSlot.HeadDamageScript.UpdateEquipment();
            }
            else if (IndexToList == 7)
            {
                SelectedObject = ControlerSlot.MyArmor;
                
                ControlerDrop.DropArmorVest();
                
                ControlerSlot.MyArmor = null;
                ControlerSlot.BodyDamageScript.UpdateEquipment();
                
            }

        }


        if (SelectedObject)
        {
            
            DropObjects(SelectedObject, ControlerDrop.PointForDrop, false);
            AllSlotsOnPlayer[IndexToList].GetComponent<Image>().sprite = None;

            DisActiveUD();
            SetDropButton(false);
            ControlerDrop.UpdateInformation(SelectedObject.gameObject);
        }

    }
}
