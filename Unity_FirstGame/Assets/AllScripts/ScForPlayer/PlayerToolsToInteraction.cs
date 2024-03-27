using UnityEngine;

public class PlayerToolsToInteraction : MonoBehaviour
{
    public ChekToInteraction PlayerChekToInteractionDelegat;

    [SerializeField] private Transform LastSelectedObject; 

    [SerializeField] Transform CameraPlayer;
    [SerializeField] ThirdPersonCamera CameraCompPlayer;

    [SerializeField] public Transform ObjectThatWatching;
    
    [SerializeField] float DistanceRay = 1.0f;
    //[SerializeField] float MoveForward = 1.0f;
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

        if (PlayerChekToInteractionDelegat == null)
        {
            Debug.Log("Not set PlayerSetInteractionDelegat");
            return;
        }
        
        Vector3 Origin = CameraCompPlayer.TargetCamera.transform.TransformPoint(CameraCompPlayer.DesirableVector) + CameraPlayer.forward / 2.0f;
        Vector3 Direction = CameraPlayer.forward * DistanceRay;
        
        Ray RayCast = new Ray(Origin, Direction);

        RaycastHit[] MassForInteraction = new RaycastHit[10];
        MassForInteraction = Physics.RaycastAll(RayCast, DistanceRay);

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


