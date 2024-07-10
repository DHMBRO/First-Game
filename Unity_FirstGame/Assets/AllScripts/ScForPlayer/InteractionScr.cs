using UnityEngine;

public class InteractionScr : MonoBehaviour
{
    [SerializeField] IInteractionWithObjects LocalInetaction;
    [SerializeField] bool CanWork = false;

    void Start()
    {
        GetComponent<PlayerToolsToInteraction>().TryToInteractionDelegate += Work;
    }

    public void Work(Transform GivenReference)
    {
        if (LocalInetaction != null && CanWork)
        {
            InteractionWithSomething(LocalInetaction);
        }

    }
    
    private void TryToInteract(Transform GivenReference)
    {
        if (GivenReference && GivenReference.GetComponent<IInteractionWithObjects>() != null)
        {
            LocalInetaction = GivenReference.GetComponent<IInteractionWithObjects>();
            CanWork = true;
        }
        else
        {
            LocalInetaction = null;
            CanWork = false;
        }
    }

    private void InteractionWithSomething(IInteractionWithObjects LocalInetaction)
    {
        if (LocalInetaction != null && LocalInetaction.AuditToUse())
        {
            LocalInetaction.Interaction();
        }

    }

}
