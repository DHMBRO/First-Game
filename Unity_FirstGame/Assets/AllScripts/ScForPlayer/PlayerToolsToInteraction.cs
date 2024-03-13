using UnityEngine;
using System.Collections.Generic;
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
        /*
        if(!CameraCompPlayer)
        {
            Debug.Log("Not set CameraCompPlayer");
            return;
        }

        List<RaycastHit> RayForInteraction = new List<RaycastHit>();
        RaycastHit LocalRayForInteraction = new RaycastHit();

        Physics.Raycast(CameraPlayer.position, CameraPlayer.forward, CameraCompPlayer.CurrentMoveBackDistance, LocalRayForInteraction);

        RayForInteraction.Add();

        //Physics.RaycastAll(CameraPlayer.transform.position, CameraPlayer.transform.forward, CameraCompPlayer.CurrentMoveBackDistance)

        Debug.DrawLine(CameraPlayer.position, CameraPlayer.forward * CameraCompPlayer.CurrentMoveBackDistance, ColorRayCast);


        if (RayForInteraction.Length > 0)
        {
            for (int i = 0; i < RayForInteraction.Length; i++)
            {
                if (RayForInteraction[i].collider.isTrigger)
                {

                }
            }
        }

        
        */
    }
    
}
