using UnityEngine;

public class ShopControler : MonoBehaviour
{
    [SerializeField] public byte CurrentAmmo;
    [SerializeField] public float Mass = 0.0f;
    [SerializeField] private BoxCollider ColiderToShop;
    [SerializeField] private Rigidbody RigidbodyToShop;

    [SerializeField] public bool InInventory = false;
    [SerializeField] public bool IsUsing = false;

    GameObject ParentShop;

    private void Start()
    {
        ColiderToShop = gameObject.GetComponent<BoxCollider>();
        RigidbodyToShop = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (ParentShop && transform.parent && ParentShop != transform.parent || !ParentShop && transform.parent)
        {
            if (transform.parent && transform.parent.tag == "SlotForShopInWeapon")
            {
                IsUsing = true;
                InInventory = false;
                ParentShop = transform.parent.gameObject;
                if (ColiderToShop) ColiderToShop.enabled = false;
                if (RigidbodyToShop) Destroy(RigidbodyToShop);
            }
            else if(transform.parent && transform.parent.tag == "UnloadingSlot") IsUsing = false;

            if (transform.parent && transform.parent.tag == "UnloadingSlot")
            {
                InInventory = true;
                IsUsing = false;
                ParentShop = transform.parent.gameObject;
                if (ColiderToShop) ColiderToShop.enabled = false;
                if (RigidbodyToShop) Destroy(RigidbodyToShop);
            }
            else if(!transform.parent )
            {
                InInventory = false;
                IsUsing = false;
                
                if (ColiderToShop) ColiderToShop.enabled = true;
            }
        }
    }
}
