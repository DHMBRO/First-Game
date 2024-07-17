using UnityEngine;

public class SingleInteractionActor : MonoBehaviour, IInteractionWithObjects
{
    [SerializeField] protected Animator Animator;
    [SerializeField] protected bool Interacted = false;

    [SerializeField] protected string BeforeInteracted = "Not interacted";
    [SerializeField] protected string AfterInteracted = "Interacted";

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

    public bool AuditToUse()
    {
        return true;
    }

    virtual public void Interaction()
    {
        if (!Interacted)
        {
            Interacted = true;
            Animator?.SetTrigger("Open");
            ChangeTextUI(AfterInteracted);
        }
        
    }


    protected void ChangeTextUI(string NewText)
    {
        AllLootScr.NameOfThisObject = NewText;
    }

}
