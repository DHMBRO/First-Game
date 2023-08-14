using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UiInventory : MonoBehaviour
{
    [SerializeField] Image[] SlotsToInventory = new Image[4];
    [SerializeField] public List<Sprite> SpritesForBackPack = new List<Sprite>();

    [SerializeField] public Sprite None;
    [SerializeField] public int Count = 0;

    [SerializeField] public Inventory PlayerInventory;
    
    private void Start()
    {
        if (!PlayerInventory) Debug.Log("Not set PlayerInventory");

        if (None)
        {
            for (int i = 0; i < 100; i++)
            {
                SpritesForBackPack.Add(None);
                //Debug.Log("SpritesForBackPack.Count" + SpritesForBackPack.Count);
            }
        }
        else Debug.Log("Not set None");

    }

    private void DeleteImage()
    {
        if (None)
        {
            for (int i = 0; i < 100; i++)
            {
                SpritesForBackPack[i] = None;
                
            }
        }
        else Debug.Log("Not set None");

    }


    private void WriteBackPack()
    {
        List<ScrForAllLoot> ImageForLoot = new List<ScrForAllLoot>();
        
        for (int i = 0;i < PlayerInventory.InfoForSlots.Count;i++)
        {
            ImageForLoot.Add(PlayerInventory.InfoForSlots[i].ObjectToInstantiate.GetComponent<ScrForAllLoot>());
        }
        


        if (PlayerInventory)
        {
            for (int i = 0;i < 4;i++)
            {
                SlotsToInventory[i].sprite = None;
            }
            DeleteImage();

            for (int i = 0;i < ImageForLoot.Count; i++)
            {
                SpritesForBackPack[i] = ImageForLoot[i].SpriteForLoot;
            }

            for (int i = Count, j = 0;i < Count + 4 && j < Count + 4;i++,j++)
            {
                //Debug.Log("Count: " + Count);
                //Debug.Log("J: " + j);
                //Debug.Log("I: " + i);

                //Debug.Log("SlotsToInventory.Length: " + SlotsToInventory.Length);
                //Debug.Log("PlayerInventory.SpritesForBackPack.Count:" + SpritesForBackPack.Count);
                
                SlotsToInventory[j].sprite = SpritesForBackPack[i];
                
                
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
