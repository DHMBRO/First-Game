using UnityEngine;

public class ArmorPlateControler : MonoBehaviour
{
    [SerializeField] public LevelObject LevelArmorPlate;
    
    [SerializeField] public float CurrentHp;
    [SerializeField] public float CurrentPercentAbsortionDamage;


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
    
    
}
