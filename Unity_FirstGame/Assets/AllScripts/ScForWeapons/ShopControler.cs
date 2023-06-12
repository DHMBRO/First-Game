using UnityEngine;

public class ShopControler : MonoBehaviour
{
    [SerializeField] public byte CurrentAmmo;
    [SerializeField] public float Mass = 0.0f;
    [SerializeField] public bool IsUsing = false;
    [SerializeField] public bool InInventory = false;
    GameObject ParentShop;

    private void Start()
    {
        /*
        if (gameObject.CompareTag("ShopM249"))
        {
            CurrentAmmo = 100;
        }
        else if (gameObject.CompareTag("ShopM4"))
        {
            CurrentAmmo = 30;
        }
        else if (gameObject.CompareTag("ShopAK47"))
        {
            CurrentAmmo = 35;
        }
        else if (gameObject.CompareTag("ShopGlok"))
        {
            CurrentAmmo = 20;
        }
        else if (gameObject.CompareTag("ShopM1911"))
        {
            CurrentAmmo = 10;
        }

        if (transform.parent && transform.parent.tag == "SlotForShopInWeapon")
        {
            IsUsing = true;
        }
        else IsUsing = false;
        */   
    }

    private void Update()
    {
        if (ParentShop && transform.parent && ParentShop != transform.parent || !ParentShop && transform.parent)
        {
            if (transform.parent && transform.parent.tag == "SlotForShopInWeapon")
            {
                IsUsing = true;
                InInventory = true;
                ParentShop = transform.parent.gameObject;
            }
            else if(transform.parent && transform.parent.tag == "UnloadingSlot") IsUsing = false;

            if (transform.parent && transform.parent.tag == "UnloadingSlot")
            {
                InInventory = true;
                IsUsing = false;
                ParentShop = transform.parent.gameObject;
            }
            else if(!transform.parent )
            {
                InInventory = false;
                IsUsing = false;
            }
        }
    }
}
