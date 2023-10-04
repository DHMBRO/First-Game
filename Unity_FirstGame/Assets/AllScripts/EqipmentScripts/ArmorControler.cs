using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorControler : MonoBehaviour, IEquipment
{
    [SerializeField] public int LevelArmor;
    
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
    

    private void Start()
    {
        if(ChangeOffSet) ChangePosition(Use);

        if (LevelArmor == 1)
        {

        }
        else if (LevelArmor == 2)
        {

        }
        else if (LevelArmor == 3)
        {

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
