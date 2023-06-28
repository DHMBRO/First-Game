using UnityEngine;
using UnityEngine.UI;

public class UiInventory : MonoBehaviour
{
    public GameObject SlotsPanel;
    public GameObject BackPackPanel;


    [SerializeField] UiControler ControlerUi;
    
    public void OpenSlots()
    {
        if (BackPackPanel) BackPackPanel.SetActive(false);
        if (SlotsPanel) SlotsPanel.SetActive(true);
    }

    public void OpenBackpack()
    {
        if (SlotsPanel) SlotsPanel.SetActive(false);
        if (BackPackPanel) BackPackPanel.SetActive(true);
        Debug.Log("BackPack is Open");
    }
    
}
