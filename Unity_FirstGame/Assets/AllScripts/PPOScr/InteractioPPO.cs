using UnityEngine;

public class InteractioPPO : MonoBehaviour, IInteractionWithObjects
{
    ScrForAllLoot ScrForAllLoot;
    public PPOIs LocalPPO;

    [SerializeField] bool PPOIsEnable = false;
    [SerializeField] string WhenEnableTextIs;
    [SerializeField] string WhenDisableTextIs;

    [SerializeField] int TimesCanBeUse = 1;
    [SerializeField] bool InfinityTimesCanBeUse = false;

    private void Start()
    {
        ScrForAllLoot = GetComponent<ScrForAllLoot>();

        LocalPPO += UpdateInfo;
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
        PPOIsEnable = !PPOIsEnable;

        LocalPPO(PPOIsEnable);
        
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

        if (!InfinityTimesCanBeUse && TimesCanBeUse == 0)
        {
            ScrForAllLoot.ShowNameOfThisObject = false;            
        }


        PPOIsEnable = EnablePPO;

    }

}
