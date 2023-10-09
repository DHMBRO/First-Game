using UnityEngine;
using System.Collections.Generic;

public class ScrForUseAmmo : MonoBehaviour, IUsebleInterFace
{
    [SerializeField] public int CurrentAmmo;
    [SerializeField] public int MaxAmmo;
    //[SerializeField] private string KeyToShops;

    public TypeCaliber CaliberToBox;

    public void Use(GameObject Target, InfoForLoot InfoLoot, SelectAnObject SelectObj)
    {
        SlotControler ContrlerToSlots = Target.GetComponent<SlotControler>();
        List<ShopControler> AllShopsHave = new List <ShopControler>();

        for (int i = 0; i < ContrlerToSlots.Shop.Length;i++) if (ContrlerToSlots.Shop[i]) AllShopsHave.Add(ContrlerToSlots.Shop[i].GetComponent<ShopControler>());
        
        if (CurrentAmmo == 0)
        {
            Debug.Log("Name Object:" + gameObject.name);
            Debug.Log("Current Ammo: " + CurrentAmmo);

            SelectObj.SelectObject();
            return;
        }
        else 
        {
            InfoLoot.SaveInfo(gameObject);
            Destroy(gameObject);
        }
    }

}

/*
 
        if (!ContrlerToSlots)
        {
            Debug.Log("Not set Slot Controler");
            return;
        }

        for (int i = 0;i < ContrlerToSlots.Shop.Length;i++)
        {
            if (ContrlerToSlots.Shop[i] != null)
            {
                ShopControler.Add(ContrlerToSlots.Shop[i].GetComponent<ShopControler>());
            }
        }

        for (int i = 0;i < ShopControler.Count; i++)
        {
            if (ShopControler[i].CaliberToShop != CaliberToBox)
            {
                ShopControler.RemoveAt(i);
            }
        }

        for (int i = 0;i < ShopControler.Count; i++)
        {
            if (ShopControler[i].CurrentAmmo == ShopControler[i].MaxAmmo)
            {
                ShopControler.RemoveAt(i);
            }
        }

        if (ShopControler.Count == 0)
        {
            Debug.Log("ShopControler.Count: " + ShopControler.Count);
            return;
        }
        else if (ShopControler.Count == 1)
        {
            
        }
        else if (ShopControler.Count == 2)
        {

        }
        else if (ShopControler.Count == 3)
        {

        }

 */ 