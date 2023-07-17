using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiInventory : MonoBehaviour
{
    [SerializeField] Image[] SlotsToInventory = new Image[4];
    
    [SerializeField] public Sprite None;
    [SerializeField] public int Count = 0;

    [SerializeField] private Inventory PlayerInventory;

    [SerializeField] private HpScript ScriptHp;
    [SerializeField] private Image HpInUi;
    [SerializeField] private TextMeshProUGUI HealPointInProzent;

    private void Start()
    {
        if (ScriptHp)
        {
            //ScriptHp.UiHp = HpInUi;
            //ScriptHp.ProzentHealPoint = HealPointInProzent;
            
        }
        else Debug.Log("Not set ScriptHp");
    }

    private void WriteBackPack()
    {
        if (PlayerInventory)
        {
            for (int i = 0;i < 4;i++)
            {
                SlotsToInventory[i].sprite = None;
            }

            for (int i = Count, j = 0;i < Count + 4 && j < Count + 4;i++,j++)
            {
                if(i < PlayerInventory.SpritesForBackPack.Count) SlotsToInventory[j].sprite = PlayerInventory.SpritesForBackPack[i];
                
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
