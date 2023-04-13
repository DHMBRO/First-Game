using UnityEngine;
using UnityEngine.UI;

public class UiControler : MonoBehaviour
{
    [SerializeField] Image scopeUi;
    [SerializeField] Image inventorySlot1x1;
    [SerializeField] Image inventorySlot2x1;
    void Start()
    {
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
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
