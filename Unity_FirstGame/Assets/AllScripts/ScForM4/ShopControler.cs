using UnityEngine;

public class ShopControler : MonoBehaviour
{
    [SerializeField] public byte MaxAmmoInShop;

    private void Start()
    {
        if (gameObject.CompareTag("ShopM249"))
        {
            MaxAmmoInShop = 100;
        }
        else if (gameObject.CompareTag("ShopM4"))
        {
            MaxAmmoInShop = 30;
        }
        else if (gameObject.CompareTag("ShopAK47"))
        {
            MaxAmmoInShop = 30;
        }
        else if (gameObject.CompareTag("ShopM1911"))
        {
            MaxAmmoInShop = 10;
        }
        
    }
}
