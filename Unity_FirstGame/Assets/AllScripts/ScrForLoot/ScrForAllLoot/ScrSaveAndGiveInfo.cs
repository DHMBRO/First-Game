using UnityEngine;

public class ScrSaveAndGiveInfo
{
    //Main Reference For Work
    public GameObject ObjectToInstantiate;
    public int CurrentAmmo;

    //Additional Info For Work
    public string[] ObjectParemeters = new string[6];
    public string ObjectDescription;
    public bool HaveDescription;


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
            for (int i = 0; i < ScrAllLoot.ParametersLoot.Length; i++) ObjectParemeters[i] = ScrAllLoot.ParametersLoot[i];
            ObjectDescription = ScrAllLoot.Descrition;
            HaveDescription = ScrAllLoot.HaveDescription;
        }
        
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
            ScrAllLoot.ParametersLoot = ObjectParemeters;
            ScrAllLoot.Descrition = ObjectDescription;
        }

    }

}
