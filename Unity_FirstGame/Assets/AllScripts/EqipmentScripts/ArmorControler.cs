using UnityEngine;

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
    //[SerializeField] public 


    private void Start()
    {
        if(ChangeOffSet) ChangePosition(Use);

    }

    void ArmorInCanvas()
    {
        

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
