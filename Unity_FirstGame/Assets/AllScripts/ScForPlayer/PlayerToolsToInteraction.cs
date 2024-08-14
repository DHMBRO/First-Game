using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class PlayerToolsToInteraction : MonoBehaviour
{
    public TryInteraction TryToInteractDelegate;
    public TryInteraction TryToInteractDelegadWithOutInput;

    [SerializeField] PlayerControler ControlerPlayer; 
    [SerializeField] public Transform LastSelectedObject;
    [SerializeField] public Transform ObjectThatWatching;

    [SerializeField] Transform CameraPlayer;
    [SerializeField] ThirdPersonCamera CameraCompPlayer;

    [SerializeField] float CurrentDistanceRay = 0.0f;
    [SerializeField] float DistanceRay = 1.0f;
    [SerializeField] float DistanceRayAiming = 100.0f;

    [SerializeField] Vector3 SizeCube = new Vector3(1.0f, 1.0f, 1.0f);
 
    [SerializeField] Color ColorRayCast = Color.white;

    [SerializeField] public RaycastHit[] MassForInteraction;
    [SerializeField] bool CanWork = true;

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

    public bool CheckToInteract(Transform Object)
    {
        return Object.GetComponent<IInteractionWithObjects>() != null || Object.GetComponent<IUsebleInterFace>() != null
          || Object.GetComponent<InfoWhatDoLoot>() || Object.GetComponent<ShootControler>()
          || Object.GetComponent<ShopControler>() || Object.GetComponent<HelmetControler>()
          || Object.GetComponent<HelmetControler>() || Object.GetComponent<ArmorControler>()
          || Object.GetComponent<BackPackContorler>() || Object.GetComponent<BoneControler>();
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

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(transform.position + transform.up * 1.5f, SizeCube);
    }

    public void InteractionWithRayCast()
    {   
        if(!CameraCompPlayer)
        {
            Debug.Log("Not set CameraCompPlayer");
            return;
        }

        if (TryToInteractDelegate == null)
        {
            Debug.Log("Not set TryToInteractionDelegate");
            return;
        }

        if(!CanWork)
        {
            Debug.Log("Canot work !");
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
        
        Transform NewSelectedObject = null;
        ScrForAllLoot ForAllLootScr = null;

        Ray RayCast = new Ray(Origin, Direction);

        MassForInteraction = new RaycastHit[10];
        MassForInteraction = Physics.RaycastAll(RayCast, CurrentDistanceRay);

        for (int i = 0;i < MassForInteraction.Length;i++)
        {
            ObjectThatWatching = MassForInteraction[i].transform;
            ForAllLootScr = ObjectThatWatching.GetComponent<ScrForAllLoot>();
            
            if (MassForInteraction[i].collider && ForAllLootScr && !ForAllLootScr.HasOwner && CheckToInteract(MassForInteraction[i].collider.transform))
            {
                ObjectThatWatching = MassForInteraction[i].transform;
                LastSelectedObject = MassForInteraction[i].transform;
                NewSelectedObject = LastSelectedObject;

                TryToInteractDelegadWithOutInput(LastSelectedObject);
                break;
            }
            else
            {
                continue;
            }    
        }

        if (NewSelectedObject == null)
        {
            TryToInteractDelegadWithOutInput(null);
            LastSelectedObject = null;
        }

        if (MassForInteraction.Length == 0)
        {
            LastSelectedObject = null;
            ObjectThatWatching = null;
            TryToInteractDelegadWithOutInput(null);
        }
    }
    
    public void SearchObjectsByBoxOverlap()
    {
        if (LastSelectedObject)
        {
            return;
        }

        Collider[] Hits = Physics.OverlapBox(transform.position + transform.up * 1.5f, SizeCube);
        List<Collider> UsableReferences = Array.FindAll<Collider>(Hits, obj => obj.gameObject.GetComponent<ScrForAllLoot>()).ToList<Collider>();
        
        float DistanceToCloserObject = 0.0f;
        Transform NewSelectedObject = null;
        ScrForAllLoot ForAllLootScr = null;

        for (int i = 0;i < Hits.Length;i++)
        {
            ForAllLootScr = Hits[i].GetComponent<ScrForAllLoot>();

            if (Hits[i].transform && ForAllLootScr && !ForAllLootScr.HasOwner && CheckToInteract(Hits[i].transform))
            {
                UsableReferences.Add(Hits[i]);
            }
        }

        if(UsableReferences.Count == 1)
        {
            LastSelectedObject = UsableReferences[0].transform;
        }
        else if (UsableReferences.Count > 1)
        {
            for (int i = 0;i < UsableReferences.Count;i++)
            {
                if(DistanceToCloserObject == 0.0f)
                {
                    DistanceToCloserObject = (UsableReferences[i].transform.position - transform.position).magnitude;
                    NewSelectedObject = UsableReferences[i].transform;
                }
                else if ((UsableReferences[i].transform.position - transform.position).magnitude <= DistanceToCloserObject)
                {
                    DistanceToCloserObject = (UsableReferences[i].transform.position - transform.position).magnitude;
                    NewSelectedObject = UsableReferences[i].transform;
                }
            }
        }

        if (NewSelectedObject)
        {
            LastSelectedObject = NewSelectedObject;
            TryToInteractDelegadWithOutInput(LastSelectedObject);
        }
        else
        {
            TryToInteractDelegadWithOutInput(null);
        }
    }
}


