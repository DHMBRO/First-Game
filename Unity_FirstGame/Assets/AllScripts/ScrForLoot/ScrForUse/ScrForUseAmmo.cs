using UnityEngine;
using System.Collections.Generic;

public class ScrForUseAmmo : MonoBehaviour, IUsebleInterFace
{
    [SerializeField] public int CurrentAmmo;
    [SerializeField] public int MaxAmmo;
    //[SerializeField] private string KeyToShops;

    public TypeCaliber CaliberToBox;

    public void Use(GameObject Target, ScrSaveAndGiveInfo InfoLoot, UseAndDropTheLoot SelectObj)
    {
        SlotControler ContrlerToSlots = Target.GetComponent<SlotControler>();
        
        List<ShopControler> AllShopsHave = new List <ShopControler>();
        List<ShopControler> NotFullMag = new List<ShopControler>();
        List<ShopControler> ShopCanReload = new List<ShopControler>();
        ShopControler ShopToReload = null;

        for (int i = 0; i < ContrlerToSlots.Shop.Length;i++) if (ContrlerToSlots.Shop[i]) AllShopsHave.Add(ContrlerToSlots.Shop[i].GetComponent<ShopControler>());
        for (int i = 0; i < AllShopsHave.Count; i++) if (AllShopsHave[i].CurrentAmmo != AllShopsHave[i].MaxAmmo) NotFullMag.Add(AllShopsHave[i]);
        for (int i = 0; i < NotFullMag.Count; i++) if (NotFullMag[i].CaliberToShop == CaliberToBox) ShopCanReload.Add(NotFullMag[i]);

        
        int NotEnoughAmmo;

        if (ShopCanReload.Count == 1) ShopToReload = ShopCanReload[0];
        else if (ShopCanReload.Count == 2)
        {
            if (ShopCanReload[0].CurrentAmmo >= ShopCanReload[1].CurrentAmmo) ShopToReload = ShopCanReload[0];
            else if(ShopCanReload[0].CurrentAmmo < ShopCanReload[1].CurrentAmmo) ShopToReload = ShopCanReload[1];
        }
        else if (ShopCanReload.Count == 3)
        {
            if (ShopCanReload[0].CurrentAmmo >= ShopCanReload[1].CurrentAmmo) ShopToReload = ShopCanReload[0];
            else if (ShopCanReload[0].CurrentAmmo < ShopCanReload[1].CurrentAmmo) ShopToReload = ShopCanReload[1];
            if (ShopToReload.CurrentAmmo < ShopCanReload[2].CurrentAmmo) ShopToReload = ShopCanReload[2];
        }

        if (ShopToReload ) ReloadMag();

        void ReloadMag()
        {
            NotEnoughAmmo = ShopToReload.MaxAmmo - ShopToReload.CurrentAmmo;

            if (NotEnoughAmmo <= CurrentAmmo)
            {
                ShopToReload.CurrentAmmo = ShopToReload.MaxAmmo;
                CurrentAmmo -= NotEnoughAmmo;
            }
            else
            {
                ShopToReload.CurrentAmmo += CurrentAmmo;
                CurrentAmmo = 0;
            }
        }


        if (CurrentAmmo == 0)
        {
            Debug.Log("Name Object:" + gameObject.name);
            Debug.Log("Current Ammo: " + CurrentAmmo);

            SelectObj.DeleteReferenceToLoot();
            return;
        }
        else 
        {
            InfoLoot.SaveInfo(gameObject);

            Destroy(gameObject);
        }
    
    }
 
    public bool Audit(GameObject Target, ScrSaveAndGiveInfo InfoLoot, UseAndDropTheLoot SelectObj)
    {
        SlotControler ControlerSlots = Target.GetComponent<SlotControler>();
        List<ShopControler> ControlerShops = new List<ShopControler>();
        bool Result = false;


        for (int i = 0;i < ControlerSlots.Shop.Length;i++)
        {
            if (ControlerSlots.Shop[i] != null)
            {
                ControlerShops.Add(ControlerSlots.Shop[i].GetComponent<ShopControler>());
            }
        }

        for(int i = 0;!Result && i < ControlerShops.Count;i++)
        {
            if (ControlerShops[i].CurrentAmmo == ControlerShops[i].MaxAmmo)
            {
                Result = false;

            }
            else Result = true;
        }


        return Result;

    }   


}
 