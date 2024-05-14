using UnityEngine;
using System.Collections.Generic;

public class ArmorControler : MonoBehaviour, IEquipment, IDamageAbsrption
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
    [SerializeField] public Transform SlotToArmorPlates;
    [SerializeField] Vector3 OffSetDontUse;
    [SerializeField] Vector3 OffSetUse;

    //References For Work
    [SerializeField] public List<ArmorPlateControler> ControlerArmorPlates = new List<ArmorPlateControler>();
    [SerializeField] public int ArmorPlatesCanUse;


    private void Start()
    {
        if(ChangeOffSet) ChangePosition(Use);

        switch (LevelArmor)
        {
            case LevelObject.FirstLevel:
                ArmorPlatesCanUse = 1; 
                break;
            case LevelObject.SecondLevel:
                ArmorPlatesCanUse = 2;
                break;
            case LevelObject.ThirdLevel:
                ArmorPlatesCanUse = 3;
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

        if (PlatesInArmorVest > ArmorPlatesCanUse)
        {
            for (int i = PlatesInArmorVest; i >= ArmorPlatesCanUse; i--)
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

    public float ReturnNewDamage(float Damage)
    {
        if (ControlerArmorPlates.Count > 0)
        {
            if (ControlerArmorPlates[ControlerArmorPlates.Count - 1].CurrentHp > Damage)
            {
                ControlerArmorPlates[ControlerArmorPlates.Count - 1].CurrentHp -= Damage;
                Damage = 0.0f;
                
            }
            else 
            {
                Damage -= ControlerArmorPlates[ControlerArmorPlates.Count - 1].CurrentHp;

                ControlerArmorPlates[ControlerArmorPlates.Count - 1].CurrentHp = 0.0f;
                Destroy(ControlerArmorPlates[ControlerArmorPlates.Count - 1].gameObject);
                ControlerArmorPlates.RemoveAt(ControlerArmorPlates.Count - 1);

                if (ControlerArmorPlates.Count > 0)
                { 
                    ControlerArmorPlates[ControlerArmorPlates.Count - 1].CurrentHp -= Damage;
                    Damage = 0.0f;
                }
            }
                

        } 



        return Damage;
    }

}
