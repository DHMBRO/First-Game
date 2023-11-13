using UnityEngine;

public class InfoForLoot
{
    
    public GameObject ObjectToInstantiate;

    public string ObjectDescription;
    public string ObjectName;

    public bool HaveDescription;
    
    public int CurrentAmmo;

     
    public void SaveInfo(GameObject ObjectFormGetInfo)
    {
        ScrForUseAmmo ScrUseAmmo = ObjectFormGetInfo.GetComponent<ScrForUseAmmo>();
        ScrForAllLoot ScrAllLoot = ObjectFormGetInfo.GetComponent<ScrForAllLoot>();

        //Debug.Log(ObjectFormGetInfo.name);
        
        if (ScrUseAmmo)
        {
            CurrentAmmo = ScrUseAmmo.CurrentAmmo;
            //Debug.Log("CurrentAmmo: " + ScrUseAmmo.CurrentAmmo);
        }

        if (ScrAllLoot)
        {
            if (ScrAllLoot.HaveDescription) ObjectDescription = ScrAllLoot.ObjectDescription;
            HaveDescription = ScrAllLoot.HaveDescription;
            ObjectName = ScrAllLoot.ObjectName;
        }
        //Debug.Log(HaveDescription);

    }
    
    public void GetInfo(GameObject ObjectToGiveInfo)
    {
        ScrForUseAmmo ScrUseAmmo = ObjectToGiveInfo.GetComponent<ScrForUseAmmo>();
        ScrForAllLoot ScrAllLoot = ObjectToGiveInfo.GetComponent<ScrForAllLoot>();

        if (ScrUseAmmo)
        {
            ScrUseAmmo.CurrentAmmo = CurrentAmmo;
            //Debug.Log("Method Name: GetInfo   CurrentAmmo: " + ScrUseAmmo.CurrentAmmo);

        }

        if (ScrAllLoot)
        {
            ScrAllLoot.HaveDescription = HaveDescription;
            ScrAllLoot.ObjectDescription = ObjectDescription;

        }

    }

}
