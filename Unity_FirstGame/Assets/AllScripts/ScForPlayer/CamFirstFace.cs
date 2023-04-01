using UnityEngine;

public class CamFirstFace : MonoBehaviour
{
    [SerializeField] private Transform Player;

    [SerializeField] private float DistanzeForRay = 6.0f;
    [SerializeField] private float Sens = 0.5f;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        gameObject.transform.Rotate(-MouseY * new Vector3(Sens, 0.0f, 0.0f));

        if (Player)
        {
            Player.Rotate(MouseX * new Vector3(0.0f, Sens, 0.0f));            
        }
        else
        {
            Player = transform.parent;
            Debug.Log("1");
        }
        Raycast();
    }
    
    void Raycast()
    {
        Ray RayForPickUp = new Ray(transform.position, transform.forward * DistanzeForRay);        
        if (Physics.Raycast(RayForPickUp, out RaycastHit HitResult))
        {
            Debug.Log(HitResult.collider.tag);
        }

        Debug.DrawRay(transform.position, transform.forward * DistanzeForRay, Color.blue);
        
    }
}
