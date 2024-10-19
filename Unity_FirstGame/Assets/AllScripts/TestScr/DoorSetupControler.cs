using UnityEngine;

public class DoorSetupControler : MonoBehaviour, IInteractionWithObjects
{
    [SerializeField] Animator Animator;
    [SerializeField] private bool DoorIsOpen = false;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public bool CheckToUse()
    {
        if (!Animator)
        {
            Debug.Log("Not set Animator !");
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Interaction()
    {
        if (DoorIsOpen)
        {
            Animator.SetTrigger("Close");
        }
        else
        {
            Animator.SetTrigger("Open");
        }
        
        DoorIsOpen = !DoorIsOpen;

    }


}
