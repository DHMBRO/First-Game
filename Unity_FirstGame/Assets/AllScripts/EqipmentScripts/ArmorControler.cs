using UnityEngine;
using System.Collections.Generic;

public class ArmorControler : MonoBehaviour, IEquipment
{
    [SerializeField] public LevelObject LevelArmor;

    [SerializeField] public Transform SlotShop01;
    [SerializeField] public Transform SlotShop02;
    [SerializeField] public Transform SlotShop03;
    [SerializeField] public Transform SlotBackPack;

    [SerializeField] public Transform SlotPistol01;
    [SerializeField] public Transform SlotKnife01;

    public bool Use;
    public bool ChangeOffSet;

    [SerializeField] Transform Armor;
    [SerializeField] Vector3 OffSetDontUse;
    [SerializeField] Vector3 OffSetUse;

    //References For Work
    [SerializeField] public List<ArmorPlateControler> ControlerArmorPlates = new List<ArmorPlateControler>();
    [SerializeField] public int SlotsCanUse;


    private void Start()
    {
        if(ChangeOffSet) ChangePosition(Use);

        switch (LevelArmor)
        {
            case LevelObject.FirstLevel:
                SlotsCanUse = 1; 
                break;
            case LevelObject.SecondLevel:
                SlotsCanUse = 2;
                break;
            case LevelObject.ThirdLevel:
                SlotsCanUse = 3;
                break;
        }


    }

    private void Update()
    {
        int PlatesInArmorVest = 0;

        for (int i = 0; i < ControlerArmorPlates.Count; i++)
        {
            if (ControlerArmorPlates[i] == null)
            {
                ControlerArmorPlates.RemoveAt(i);
            }
            else PlatesInArmorVest++;
        }

        if (PlatesInArmorVest > SlotsCanUse)
        {
            for (int i = PlatesInArmorVest; i >= SlotsCanUse; i--)
            {
                if(i < ControlerArmorPlates.Count) ControlerArmorPlates.RemoveAt(i);
            }
        }
    }


    public void ChangePosition(bool Use)
    {
        this.Use = Use;

        if (!ChangeOffSet) return;

        if (Use)
        {
            Armor.transform.position = OffSetUse;
        }
        else
        {
            Armor.transform.position = OffSetDontUse;
        }

    
    }


}
