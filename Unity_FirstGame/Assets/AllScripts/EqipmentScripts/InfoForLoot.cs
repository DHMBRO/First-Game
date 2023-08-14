using UnityEngine;

public class InfoForLoot
{
    
    public GameObject ObjectToInstantiate;

    public int CurrentAmmo;
     
    public void SaveInfo(GameObject ObjectFormGetInfo)
    {
        ScrForUseAmmo ScrUseAmmo = ObjectFormGetInfo.GetComponent<ScrForUseAmmo>();
        
        Debug.Log(ObjectFormGetInfo.name);
        
        if (!ScrUseAmmo)
        {
            Debug.Log("Not set ScrUseAmmo");
            CurrentAmmo = 0;
            return;
        }

        CurrentAmmo = ScrUseAmmo.CurrentAmmo;
        Debug.Log("CurrentAmmo: " + ScrUseAmmo.CurrentAmmo);

        

    }
    
    public void GetInfo(GameObject ObjectToGiveInfo)
    {
        ScrForUseAmmo ScrUseAmmo = ObjectToGiveInfo.GetComponent<ScrForUseAmmo>();

        if (!ScrUseAmmo)
        {
            Debug.Log("Not set ScrUseAmmo");
            CurrentAmmo = 0;
            return;
        }

        ScrUseAmmo.CurrentAmmo = CurrentAmmo;
        Debug.Log("Method Name: GetInfo   CurrentAmmo: " + ScrUseAmmo.CurrentAmmo);
       
    }

}
