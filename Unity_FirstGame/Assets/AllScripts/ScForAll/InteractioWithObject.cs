using UnityEngine;

public class InteractioWithObject : MonoBehaviour, IInteractionWithObjects
{
    ScrForAllLoot ScrForAllLoot;
    public IsEnable ObjectIs;

    [SerializeField] string WhenEnableTextIs;
    [SerializeField] string WhenDisableTextIs;

    int TimesCanBeUse = 1;
    bool InfinityTimesCanBeUse = false;
    bool Enable = false;


    private void Start()
    {
        ScrForAllLoot = GetComponent<ScrForAllLoot>();
        
        ObjectIs += UpdateInfo;

        UpdateInfo(false);        
    }

    public bool AuditToUse()
    {
        bool Result = true;
        
        if ((!InfinityTimesCanBeUse && TimesCanBeUse == 0) || !ScrForAllLoot)
        {
            Result = false;
        }

        return Result;
    }

    public void Interaction()
    {
        if(!InfinityTimesCanBeUse) TimesCanBeUse--;
        Enable = !Enable;

        ObjectIs(Enable);
        
    }

    private void UpdateInfo(bool EnablePPO)
    {
        if(EnablePPO)
        {
            ScrForAllLoot.NameOfThisObject = WhenEnableTextIs;
        }
        else
        {
            ScrForAllLoot.NameOfThisObject = WhenDisableTextIs;
        }

        Enable = EnablePPO;

    }

}
