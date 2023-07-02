using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UiInventory : MonoBehaviour
{
    [SerializeField] Image[] Slots = new Image[4];
    [SerializeField] List<IImage> ListToBackpack = new List<IImage>();

    [SerializeField] Sprite None;
    [SerializeField] int Count = 0;

    [SerializeField] private ReferenseForAllLoot ReferenseForLoot;

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

    private void WriteBackPack()
    {
        for (int i = 0; i < ListToBackpack.Count + 3; i++)
        {
            //ListToBackpack.Add();
        }


    }

}
