using UnityEngine;
using System.Collections.Generic;

public class ScrForUseAmmo : MonoBehaviour, IUsebleInterFace
{
    [SerializeField] public int CurrentAmmo;
    [SerializeField] private int MaxAmmo;
    //[SerializeField] private string KeyToShops;

    public TypeCaliber CaliberToBoxAmmo;

    public void Use(GameObject Target, SelectAnObject SelectObj)
    {
        SlotControler ControlerSlots = Target.GetComponent<SlotControler>();

        if (ControlerSlots)
        {
            List<ShopControler> AllShops = new List<ShopControler>();
            
            if(ControlerSlots.MyShope01)
            {
                ShopControler ControlerShop01 = ControlerSlots.MyShope01.GetComponent<ShopControler>();
                //if (ControlerShop01.KeyTupeCaliber == KeyToShops && ControlerShop01.ParentShop.tag == "UnloadingSlot") AllShops.Add(ControlerSlots.MyShope01.GetComponent<ShopControler>());
            }
            if (ControlerSlots.MyShope02)
            {
                ShopControler ControlerShop02 = ControlerSlots.MyShope02.GetComponent<ShopControler>();
                //if (ControlerShop02.KeyTupeCaliber == KeyToShops && ControlerShop02.ParentShop.tag == "UnloadingSlot") AllShops.Add(ControlerSlots.MyShope02.GetComponent<ShopControler>());
            }
            if (ControlerSlots.MyShope03)
            {
                ShopControler ControlerShop03 = ControlerSlots.MyShope03.GetComponent<ShopControler>();
                //if (ControlerShop03.KeyTupeCaliber == KeyToShops && ControlerShop03.ParentShop.tag == "UnloadingSlot") AllShops.Add(ControlerSlots.MyShope03.GetComponent<ShopControler>());
            }
            for (int i = 0;i < AllShops.Count; i++)
            {
                if(AllShops[i].MaxAmmo != AllShops[i].CurrentAmmo)
                {
                    if(AllShops[i].CurrentAmmo++ < AllShops[i].MaxAmmo)
                    {
                        for (int j = 0;j <= MaxAmmo; j++)
                        {
                            if ((AllShops[i].CurrentAmmo += j) <= AllShops[i].MaxAmmo)
                            {
                                AllShops[i].CurrentAmmo += j;
                            }
                        }
                    }    
                    else if (AllShops[i].CurrentAmmo++ == AllShops[i].MaxAmmo)
                    {
                        CurrentAmmo--;
                        AllShops[i].CurrentAmmo++;
                        break;
                    }
                }
                
            }


        }

        if(CurrentAmmo == 0)
        {
            SelectObj.SelectObject();
            Destroy(gameObject, 2.5f);
        }
    }

}
