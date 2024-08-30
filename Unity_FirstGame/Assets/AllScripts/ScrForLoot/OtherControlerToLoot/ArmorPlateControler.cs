using UnityEngine;

public class ArmorPlateControler : MethodsFromDevelopers, IUsebleInterFace
{
    [SerializeField] public LevelObject LevelArmorPlate;
    
    //Paremters For Work
    [SerializeField] public float MaxHp;
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
        ArmorControler ControlerArmor = null;

        if (ControlerSlots && ControlerSlots.MyArmor)
        {
            ControlerArmor = ControlerSlots.MyArmor.GetComponent<ArmorControler>();
        }

        if (!ControlerArmor || !ControlerArmor.SlotToArmorPlates) return false;
        
        if (ControlerArmor.ControlerArmorPlates.Count < ControlerArmor.ArmorPlatesCanUse+1)
        {
            Result = true;
        }

        return Result;
    }

    public void Use(GameObject Target, ScrSaveAndGiveInfo InfoLoot, UseAndDropTheLoot SelectObj)
    {
        SlotControler ControlerSlots = Target.GetComponent<SlotControler>();
        ArmorControler ControlerArmor = ControlerSlots.MyArmor.GetComponent<ArmorControler>();

        ControlerArmor.ControlerArmorPlates.Add(this);
        PutObjects(transform, ControlerArmor.SlotToArmorPlates, false);
        
        SelectObj.GetComponent<UiControler>().InterfaceControler();
        SelectObj.DeleteReferenceToLoot();

        SelectObj.ObjectToUse = null;
    }


}
