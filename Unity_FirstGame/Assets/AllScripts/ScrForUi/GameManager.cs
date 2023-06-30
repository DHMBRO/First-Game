using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject SlotsPanel;
    public GameObject BackPackPanel;

    public void OpenSlots()
    {
        Debug.Log("Slots is Open");
        if (BackPackPanel) BackPackPanel.SetActive(false);
        if (SlotsPanel) SlotsPanel.SetActive(true);
    }

    public void OpenBackpack()
    {
        Debug.Log("BackPack is Open");
        if (SlotsPanel) SlotsPanel.SetActive(false);
        if (BackPackPanel) BackPackPanel.SetActive(true);
    }

    public void A()
    {
        Debug.Log("Is work");
    }

}
