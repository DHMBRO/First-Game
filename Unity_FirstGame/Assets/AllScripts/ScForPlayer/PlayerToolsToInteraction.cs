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

    public Vector3 RaycastByInteraction()
    {
        Vector3 Origin = CameraPlayer.position + (CameraPlayer.forward * CameraCompPlayer.CurrentMoveBackDistance);
        Vector3 Direction = CameraPlayer.forward;
        Vector3 EndPoint = new Vector3();

        Ray RayCast = new Ray(Origin, Direction * DistanceRayAiming);
        
        if (Physics.Raycast(RayCast, out RaycastHit HitResult, CurrentDistanceRay)) 
        {
            EndPoint = HitResult.point;
        }
        else
        {
            EndPoint = CameraPlayer.position + (CameraPlayer.forward * DistanceRayAiming);   
        }
        return EndPoint;
        
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
        ScrForAllLoot ForAllLootScr = null;

        Ray RayCast = new Ray(Origin, Direction);

        RaycastHit[] MassForInteraction = new RaycastHit[10];
        MassForInteraction = Physics.RaycastAll(RayCast, CurrentDistanceRay);

        Debug.DrawRay(Origin, Direction, ColorRayCast);

        for (int i = 0;i < MassForInteraction.Length;i++)
        {
            ObjectThatWatching = MassForInteraction[i].transform;
            ForAllLootScr = ObjectThatWatching.GetComponent<ScrForAllLoot>();

            if (MassForInteraction[i].collider && ForAllLootScr && !ForAllLootScr.HasOwner && PlayerChekToInteractionDelegat(MassForInteraction[i].collider.transform))
            {
                ObjectThatWatching = MassForInteraction[i].transform;
                LastSelectedObject = MassForInteraction[i].transform;

                break;
            }
            else PlayerChekToInteractionDelegat(null);

        }
    }
}


