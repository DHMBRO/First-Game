using UnityEngine;

public class PlayerToolsToInteraction : MonoBehaviour
{
    [SerializeField] Transform CameraPlayer;
    [SerializeField] ThirdPersonCamera CameraCompPlayer;
    [SerializeField] Color ColorRayCast = Color.white;

    private void Start()
    {
        //Setup References
        if (CameraPlayer)
        {
            CameraCompPlayer = CameraPlayer.GetComponent<ThirdPersonCamera>();
        }
        else Debug.Log("Not set CameraPlayer");
        

    }

    public void InteractionWithRayCast()
    {
        if(!CameraCompPlayer)
        {
            Debug.Log("Not set CameraCompPlayer");
            return;
        }

        RaycastHit[] RayForInteraction = new RaycastHit[5];
        RayForInteraction = Physics.RaycastAll(CameraPlayer.transform.position, CameraPlayer.transform.forward, CameraCompPlayer.CurrentMoveBackDistance);


        Debug.DrawLine(CameraPlayer.position, CameraPlayer.forward * CameraCompPlayer.CurrentMoveBackDistance, ColorRayCast);
        
        //if (Physics.Raycast(RayForInteraction, out RaycastHit HitResult))
        {
            
        }
        
    }
    
}
