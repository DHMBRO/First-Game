using UnityEngine;

public class AdvancedInteractionActor : SingleInteractionActor
{


    override public void Interaction()
    {
        if (!Interacted)
        {
            ChangeTextUI(AfterInteracted);
            Animator?.SetTrigger("Open");
        }
        else
        {
            ChangeTextUI(BeforeInteracted);
            Animator?.SetTrigger("Close");
        }
        
        Interacted = !Interacted;
    }
}
