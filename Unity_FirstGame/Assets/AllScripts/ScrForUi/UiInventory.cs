using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UiInventory : MonoBehaviour
{
    [SerializeField] Image[] Slots = new Image[4];
    
    [SerializeField] public Sprite None;
    [SerializeField] public int Count = 0;

    [SerializeField] private Inventory PlayerInventory;

    private void WriteBackPack()
    {
        if (PlayerInventory)
        {
            for (int i = 0;i < 4;i++)
            {
                Slots[i].sprite = None;
            }

            for (int i = Count, j = 0;i < Count + 4 && j < Count + 4;i++,j++)
            {
                Slots[j].sprite = PlayerInventory.SpritesForBackPack[i];
                
                //Debug.Log("Count: " + Count);
                //Debug.Log("J: " + j);
                //Debug.Log("I: " + i);
            }

        }   
        else Debug.Log("Reference to Inventory not set");
    }

    

    public void WriteSprite()
    {
        WriteBackPack();
    }

    public void Up()
    {
        if (Count - 1 >= 0) Count = Count - 1;
        //Debug.Log("Count: " + Count);
        WriteBackPack();
    }

    public void Down()
    {
        Count++;
        //Debug.Log("Count: " + Count);
        WriteBackPack();
    }

    public void AddSpriteInInventory(GameObject ObjectToPickUp)
    {
        ObjectToPickUp.GetComponent<IImage>();



    } 

}
