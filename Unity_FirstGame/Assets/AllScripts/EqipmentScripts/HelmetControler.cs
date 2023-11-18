using UnityEngine;

public class HelmetControler : MonoBehaviour
{

    [SerializeField] public LevelEquipment LevelHelmet;
    [SerializeField] public bool Use;

    private void Start()
    {
        switch (LevelHelmet)
        {
            case LevelEquipment.FirstLevel:
                break;
            case LevelEquipment.SecondLevel:
                break;
            case LevelEquipment.ThirdLevel:
                break;
            default:
                break;
        }

    }


    void Update()
    {
        
    }
}
