using UnityEngine;

public class InfoForLoot
{
    
    public GameObject ObjectToInstantiate;

    public int CurrentAmmo;
     
    public void SaveInfo(GameObject ObjectFormGetInfo)
    {
        ScrForUseAmmo ScrUseAmmo = ObjectFormGetInfo.GetComponent<ScrForUseAmmo>();
        ScrForUseAmmo UseAmmoScr = ObjectToInstantiate.GetComponent<ScrForUseAmmo>();
        
        Debug.Log(ObjectFormGetInfo.name);
        
        if (!ScrUseAmmo)
        {
            Debug.Log("Not set ScrUseAmmo");
            CurrentAmmo = 0;
            return;
        }

        CurrentAmmo = ScrUseAmmo.CurrentAmmo;
        UseAmmoScr.CurrentAmmo = CurrentAmmo;

        Debug.Log("CurrentAmmo: " + UseAmmoScr.CurrentAmmo);

        

    }
    


}
