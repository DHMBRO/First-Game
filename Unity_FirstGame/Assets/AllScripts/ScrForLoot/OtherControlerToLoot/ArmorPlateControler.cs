using UnityEngine;

public class ArmorPlateControler : MonoBehaviour, IUsebleInterFace
{
    [SerializeField] public LevelObject LevelArmorPlate;
    
    //Paremters For Work
    [SerializeField] private float MaxHp;
    [SerializeField] public float CurrentHp;
    [SerializeField] public float CurrentPercentAbsortionDamage;
    //Parameters For UI
    [SerializeField] public float CurrentHpUi;



    void Start()
    {
        switch (LevelArmorPlate)
        {
            case LevelObject.FirstLevel:
                break;
            case LevelObject.SecondLevel:
                break;
            case LevelObject.ThirdLevel:
                break;
            
        }

    }


    public bool Audit(GameObject Target, ScrSaveAndGiveInfo InfoLoot, UseAndDropTheLoot SelectObj)
    {
        bool Result = false;
        SlotControler ControlerSlots = Target.GetComponent<SlotControler>();
        ArmorControler ControlerArmor = ControlerSlots.MyArmor.GetComponent<ArmorControler>();


        if (!ControlerSlots.MyArmor) return false;

        //for (int i = 0;i < ;i++)
        {

        }


        return Result;
    }

    public void Use(GameObject Target, ScrSaveAndGiveInfo InfoLoot, UseAndDropTheLoot SelectObj)
    {
        SlotControler ControlerSlots = Target.GetComponent<SlotControler>();
        ArmorControler ControlerArmorPlate = ControlerSlots.MyArmor.GetComponent<ArmorControler>();

        

    }


}
