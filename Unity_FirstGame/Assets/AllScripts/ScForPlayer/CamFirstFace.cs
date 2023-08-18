    using UnityEngine;

public class CamFirstFace : MonoBehaviour
{    
    [SerializeField] private Transform Player;    
    [SerializeField] private Transform CameraObject;
    
    [SerializeField] public Transform ObjectRay;

    [SerializeField] private float Sens = 1.5f;
    [SerializeField] public bool InventoryIsOpen = false; 

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        InventoryIsOpen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryIsOpen = !InventoryIsOpen;
            //Cursor.lockState = CursorLockMode.None;
            
        }

        

        if (!InventoryIsOpen)
        {
            float MouseX = Input.GetAxis("Mouse X");
            float MouseY = Input.GetAxis("Mouse Y");

            if (CameraObject) CameraObject.transform.Rotate(-MouseY * new Vector3(Sens, 0.0f, 0.0f));
            else Debug.Log("Not set CameraObject");
            
            if (Player) Player.Rotate(MouseX * new Vector3(0.0f, Sens, 0.0f));
            else Debug.Log("Not set Player ");
        }
    }
    
   

   



}
