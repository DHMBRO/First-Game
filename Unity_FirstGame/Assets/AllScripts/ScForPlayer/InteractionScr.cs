using UnityEngine;

public class InteractionScr : MonoBehaviour
{
    [SerializeField] IInteractionWithObjects LocalInetaction;
    
    void Start()
    {
        GetComponent<PlayerToolsToInteraction>().TryToInteractDelegate += TryToInteract;
    }

    public void TryToInteract(Transform GivenReference)
    {
        LocalInetaction = GivenReference.GetComponent<IInteractionWithObjects>();

        if (LocalInetaction != null && LocalInetaction.AuditToUse())
        {
            LocalInetaction.Interaction();
        }

    }
    
}
