using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UiInventory : MonoBehaviour
{
    [SerializeField] Image[] Slots = new Image[4];
    
    [SerializeField] Sprite None;
    [SerializeField] int Count = 0;

    [SerializeField] private Inventory PlayerInventory;

    private void Start()
    {
        
    }

    private void WriteBackPack()
    {
        if (PlayerInventory)
        {
            if (PlayerInventory.SpritesForBackPack[Count] != null) Slots[Count].sprite = PlayerInventory.SpritesForBackPack[Count];
            else Slots[Count].sprite = None;

            if (PlayerInventory.SpritesForBackPack[Count += 1] != null) Slots[Count += 1].sprite = PlayerInventory.SpritesForBackPack[Count += 1];
            else Slots[Count += 1].sprite = None;

            if (PlayerInventory.SpritesForBackPack[Count += 2] != null) Slots[Count += 2].sprite = PlayerInventory.SpritesForBackPack[Count += 2];
            else Slots[Count += 2].sprite = None;

        }
        else Debug.Log("Reference to Inventory not set");
    }

    public void Up()
    {
        if (Count - 1 >= 0) Count = Count - 1;
        Debug.Log("Count: " + Count);
        WriteBackPack();
    }

    public void Down()
    {
        Count++;
        Debug.Log("Count: " + Count);
        WriteBackPack();
    }

    public void AddSpriteInInventory(GameObject ObjectToPickUp)
    {
        ObjectToPickUp.GetComponent<IImage>();



    } 

}
