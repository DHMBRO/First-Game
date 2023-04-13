using UnityEngine;
using UnityEngine.UI;

public class UiControler : MonoBehaviour
{
    [SerializeField] Image scopeUi;
    [SerializeField] Image inventorySlot1x1;
    [SerializeField] Image inventorySlot2x1;

    public bool inventoryIsOpen;
    void Start()
    {
        inventoryIsOpen = false;
        scopeUi.enabled = true;
        inventorySlot1x1.enabled = false;
        inventorySlot2x1.enabled = false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            scopeUi.enabled = !scopeUi.enabled;
            inventorySlot1x1.enabled = !scopeUi.enabled;
            inventorySlot2x1.enabled = !scopeUi.enabled;
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
    }
}
