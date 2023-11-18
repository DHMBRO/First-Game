using UnityEngine;

public class BackPackContorler : MonoBehaviour
{
    [SerializeField] public LevelEquipment LevelBackPack;

    //[SerializeField] public int LevelBackPack;
    [SerializeField] public float CurrentMaxMass;
    [SerializeField] public float CurrentMass;

    [SerializeField] float MaxMassTo1L = 12.0f;
    [SerializeField] float MaxMassTo2L = 17.0f;
    [SerializeField] float MaxMassTo3L = 25.0f;

    
    void Start()    
    {
        switch (LevelBackPack)
        {
            case LevelEquipment.FirstLevel:
                CurrentMaxMass = MaxMassTo1L;
                break;
            
            case LevelEquipment.SecondLevel:
                CurrentMaxMass = MaxMassTo2L;
                break;

            case LevelEquipment.ThirdLevel:
                CurrentMaxMass = MaxMassTo3L;
                break;
        }

    }
    
    
}
