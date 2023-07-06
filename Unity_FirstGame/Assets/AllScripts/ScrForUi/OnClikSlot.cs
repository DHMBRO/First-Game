using UnityEngine;

public class OnClikSlot : MonoBehaviour
{
    [SerializeField] GameObject PanelToDo;

    public void OnClikButton()
    {
        Debug.Log("Button is work");
        if(PanelToDo) PanelToDo.SetActive(true);
    }


}
