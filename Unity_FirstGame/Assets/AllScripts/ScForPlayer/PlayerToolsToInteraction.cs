using UnityEngine;

public class PlayerToolsToInteraction : MonoBehaviour
{
    public ChekToInteraction PlayerChekToInteractionDelegat;

    [SerializeField] PlayerControler ControlerPlayer; 
    [SerializeField] Transform LastSelectedObject; 

    [SerializeField] Transform CameraPlayer;
    [SerializeField] ThirdPersonCamera CameraCompPlayer;

    [SerializeField] public Transform ObjectThatWatching;

    [SerializeField] float CurrentDistanceRay = 0.0f;
    [SerializeField] float DistanceRay = 1.0f;
    [SerializeField] float DistanceRayAiming = 100.0f;
    
    [SerializeField] Color ColorRayCast = Color.white;
    
    private void Start()
    {
        //Setup References
        if (CameraPlayer)
        {
            CameraCompPlayer = CameraPlayer.GetComponent<ThirdPersonCamera>();
        }
        else Debug.Log("Not set CameraPlayer");

        ControlerPlayer = GetComponent<PlayerControler>();
        

    }

    public void InteractionWithRayCast()
    {
        
        if(!CameraCompPlayer)
        {
            Debug.Log("Not set CameraCompPlayer");
            return;
        }

        if (PlayerChekToInteractionDelegat == null)
        {
            Debug.Log("Not set PlayerSetInteractionDelegat");
            return;
        }


        switch (ControlerPlayer.StateCamera)
        {
            case global::CameraPlayer.Aiming:
                CurrentDistanceRay = DistanceRayAiming;
                break;
            default:
                CurrentDistanceRay = DistanceRay;
                break;
        }


        Vector3 Origin = CameraPlayer.position + (CameraPlayer.forward * CameraCompPlayer.CurrentMoveBackDistance);
        Vector3 Direction = CameraPlayer.forward * CurrentDistanceRay;
        
        Ray RayCast = new Ray(Origin, Direction);

        RaycastHit[] MassForInteraction = new RaycastHit[10];
        MassForInteraction = Physics.RaycastAll(RayCast, CurrentDistanceRay);

        Debug.DrawRay(Origin, Direction, ColorRayCast);

        for (int i = 0;i < MassForInteraction.Length;i++)
        {
            ObjectThatWatching = MassForInteraction[i].transform;

            if (MassForInteraction[i].collider && PlayerChekToInteractionDelegat(MassForInteraction[i].collider.transform))
            {
                ObjectThatWatching = MassForInteraction[i].transform;
                LastSelectedObject = MassForInteraction[i].transform;

                break;
            }
        }
    }
}


