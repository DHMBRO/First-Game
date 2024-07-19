using UnityEngine;
using System.Collections.Generic;

public class InteractChecker : MonoBehaviour
{
    [SerializeField] List<SingleInteractionActor> AllInteractionObjects = new List<SingleInteractionActor>();
    [SerializeField] SingleInteractionActor Targetinteraction;

    private void Start()
    {
        Targetinteraction = GetComponent<SingleInteractionActor>();

        for (int i = 0;i < AllInteractionObjects.Count;i++) 
        {
            AllInteractionObjects[i].DelegateUpdateOnEvent += CheckToInteract;
        }   
    }

    private void CheckToInteract()
    {
        for (int i = 0; i < AllInteractionObjects.Count; i++)
        {
            if (!AllInteractionObjects[i].CheckInteractedState())
            {
                return;
            }
        }

        Targetinteraction.SetUpCanWork(true);

        if (Targetinteraction.AuditToUse())
        {
            Targetinteraction.Interaction();
        }
    }
}
