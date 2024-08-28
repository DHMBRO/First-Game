using UnityEngine;

public class SingleInteractionActor : MonoBehaviour, IInteractionWithObjects
{
    [SerializeField] protected Animator Animator;
    [SerializeField] protected bool Interacted = false;
    [SerializeField] protected bool CanInteract = true;

    [SerializeField] protected string BeforeInteracted = "Not interacted";
    [SerializeField] protected string AfterInteracted = "Interacted";
    
    public UpdateOnEvent DelegateUpdateOnEvent;
    [SerializeField] protected ScrForAllLoot AllLootScr;

    protected void Start()
    {
        //Search 
        Animator = GetComponent<Animator>();
        if(!AllLootScr) AllLootScr = GetComponent<ScrForAllLoot>();

        //Setup 
        ChangeTextUI(BeforeInteracted);
    }

    public bool CheckInteractedState()
    {
        return Interacted;
    }

    public bool AuditToUse()
    {
        return CanInteract;   
    }

    virtual public void Interaction()
    {
        if (!Interacted)
        {
            Interacted = true;
            
            if(Animator) Animator.SetTrigger("Open");
            if(DelegateUpdateOnEvent != null) DelegateUpdateOnEvent();

            ChangeTextUI(AfterInteracted);
            CustomInteraction();
        }
    }

    virtual protected void CustomInteraction()
    {

    }

    public void SetUpCanWork(bool Value)
    {
        CanInteract = Value;
    }

    protected void ChangeTextUI(string NewText)
    {
        if(AllLootScr) 
        {
            AllLootScr.NameOfThisObject = NewText;
        }
        else
        {
            Debug.LogError(gameObject.name + ": Not set ScrForAllLoot !");
            
        }
    }

}
