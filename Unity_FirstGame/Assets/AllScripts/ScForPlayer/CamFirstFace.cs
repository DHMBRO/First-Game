using UnityEngine;

public class CamFirstFace : MonoBehaviour
{    
    [SerializeField] private Transform Player;    
    [SerializeField] private Transform PlayerHand;
    [SerializeField] public Transform ObjectRay;
    [SerializeField] private float Sens = 0.5f;
    [SerializeField] UiControler uiControler;
    [SerializeField] SlotControler SlotControlerScript;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;      
    }

    void Update()
    {
        if (true)
        {
            float MouseX = Input.GetAxis("Mouse X");
            float MouseY = Input.GetAxis("Mouse Y");

            gameObject.transform.Rotate(-MouseY * new Vector3(Sens, 0.0f, 0.0f));
            
            if (Player)
            {
                Player.Rotate(MouseX * new Vector3(0.0f, Sens, 0.0f));
            }
           

        }
    }
    
   

   



}
