using UnityEngine;

public class TestScr : MonoBehaviour, IInteractionWithObjects
{
    [SerializeField] Vector3 ChangeRotateTo;
    
    [SerializeField] bool Used = false;
    [SerializeField] int TimesCanUse = 1;

    public bool AuditToUse()
    {
        if (TimesCanUse > 0)
        {
            TimesCanUse--;
            Used = false;
        }
        else Used = true;

        return !Used;
    }

    public void Interaction()
    {
        transform.localEulerAngles += ChangeRotateTo;

    }

}
