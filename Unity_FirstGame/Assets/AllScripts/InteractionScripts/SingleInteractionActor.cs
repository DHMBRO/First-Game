using UnityEngine;

public class SingleInteractionActor : MonoBehaviour, IInteractionWithObjects
{
    [SerializeField] protected Animator Animator;
    [SerializeField] protected bool Interacted = false;
    [SerializeField] protected bool CanWork = true;

    [SerializeField] protected string BeforeInteracted = "Not interacted";
    [SerializeField] protected string AfterInteracted = "Interacted";
    
    public UpdateOnEvent DelegateUpdateOnEvent;
    private ScrForAllLoot AllLootScr;

    void Start()
    {
        //Search 
        Animator = GetComponent<Animator>();
        AllLootScr = GetComponent<ScrForAllLoot>();

        if (!AllLootScr)
        {
            Debug.LogError("Not set ScrForAllLoot !");
        }

        //Setup 
        ChangeTextUI(BeforeInteracted);
    }

    public bool CheckInteractedState()
    {
        return Interacted;
    }

    public bool AuditToUse()
    {
        return CanWork;
        
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
        CanWork = Value;
    }

    protected void ChangeTextUI(string NewText)
    {
        AllLootScr.NameOfThisObject = NewText;
    }

}
