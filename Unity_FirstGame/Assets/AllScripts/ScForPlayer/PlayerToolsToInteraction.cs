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

        /*
        if (Physics.Raycast(RayToRayCast, out RaycastHit A, DistanceRay, LayerMask.GetMask("Default")))
        {
            Debug.Log(A.collider.name);
        }
        */

        Debug.DrawRay(Origin, Direction, ColorRayCast);

        for (int i = 0;i < MassForInteraction.Length;i++)
        {
            ObjectThatWatching = MassForInteraction[i].transform;

            if (MassForInteraction[i].collider && PlayerChekToInteractionDelegat(MassForInteraction[i].collider.transform))
            {
                ObjectThatWatching = MassForInteraction[i].transform;
                LastSelectedObject = MassForInteraction[i].transform;

                Debug.Log(MassForInteraction[i].collider.name);
                break;
            }
            
        }

        /*

      for (int i = MassForInteraction.Length-1; i > -1 && i < MassForInteraction.Length; i++)
      {
          //Debug.Log("Part 1 is work");

          if (MassForInteraction[i].collider.gameObject != null && PlayerChekToInteractionDelegat != null)
          {
              if (PlayerChekToInteractionDelegat(MassForInteraction[i].collider.transform))
              {
                  Debug.Log(MassForInteraction[i].collider.name);
              }
              else Debug.Log(MassForInteraction[i].collider.name);

          }

          if (MassForInteraction[i].collider.gameObject != null && PlayerChekToInteractionDelegat != null 
          && PlayerChekToInteractionDelegat(MassForInteraction[i].collider.transform)) 
          {
              Debug.Log("Part 2 is work");

              if (!LastSelectedObject || (LastSelectedObject && LastSelectedObject.name != MassForInteraction[i].collider.name))
              {
                  Debug.Log("Part 3 is work");

                  LastSelectedObject = MassForInteraction[i].collider.transform;
                  ObjectThatWatching = MassForInteraction[i].collider.transform;

                  break;
              }

          }    
          */
    }



}


