using UnityEngine;

public class ArmorPlateControler : MonoBehaviour
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

    private void Update()
    {
        CurrentHpUi = ((100 / MaxHp) * CurrentHp) / 100;
        //Debug.Log("Current UI heal point: " + CurrentHpUi);
        
    }


}
