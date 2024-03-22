using UnityEngine;

public class PlayerToolsToInteraction : MonoBehaviour
{
    public SetInteractions PlayerSetInteractionDelegat;
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

        if (PlayerSetInteractionDelegat == null)
        {
            Debug.Log("Not set PlayerSetInteractionDelegat");
            return;
        }
        

        Vector3 Origin = CameraCompPlayer.TargetCamera.transform.TransformPoint(CameraCompPlayer.DesirableVector) + CameraPlayer.forward / 2.0f;
        Vector3 Direction = CameraPlayer.forward * DistanceRay;

        RaycastHit[] MassForInteraction = new RaycastHit[5];
        
        MassForInteraction = Physics.RaycastAll(Origin, CameraPlayer.forward, DistanceRay);
        
        Debug.DrawRay(Origin, Direction, ColorRayCast);
        

        for (int i = MassForInteraction.Length-1; i > -1 && i < MassForInteraction.Length; i++)
        {
            if (MassForInteraction[i].collider.gameObject != null && !MassForInteraction[i].collider.isTrigger)
            {
                if (!LastSelectedObject || (LastSelectedObject && LastSelectedObject.name != MassForInteraction[i].collider.name))
                {

                    LastSelectedObject = MassForInteraction[i].collider.transform;
                    ObjectThatWatching = MassForInteraction[i].collider.transform;
                    
                    PlayerSetInteractionDelegat(ObjectThatWatching);
                    break;
                }
                
            }    
        }



    }

}
