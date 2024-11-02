using UnityEngine;

public class ScrToActivateHeli : MonoBehaviour, IInteractionWithObjects
{
    [SerializeField] HelicopterScr ScrHelicopter;
    bool Used = false;

    public bool CheckToUse()
    {
        if (!ScrHelicopter || Used)
        {
            if (!ScrHelicopter)
            {
                Debug.Log("Not set ScrHelicopter !");
            }
            return false;
        }
        else 
        {
            return true;
        }
    }

    public void Interaction()
    {
        ScrHelicopter.NextState();
        Used = true;
    }

}
