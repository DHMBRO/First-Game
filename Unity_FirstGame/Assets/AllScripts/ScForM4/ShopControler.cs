using UnityEngine;

public class ShopControler : MonoBehaviour
{
    [SerializeField] public byte CurrentAmmo;
    [SerializeField] public float Mass = 0.0f;
    [SerializeField] public bool IsUsing = false;

    private void Start()
    {
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
    }

    private void Update()
    {
        if (transform.parent && transform.parent.tag == "SlotForShopInWeapon")
        {
            IsUsing = true;
        }
        else IsUsing = false;
    }
}
