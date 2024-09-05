using UnityEngine;

public class HelmetControler : MonoBehaviour, IDamageAbsrption
{

    [SerializeField] public LevelObject LevelHelmet;
    [SerializeField] public bool Use;
    [SerializeField] float MultiplerOfDamage = 1.0f;

    private void Start()
    {
        switch (LevelHelmet)
        {
            case LevelObject.FirstLevel:
                MultiplerOfDamage = 0.9f;
                break;
            case LevelObject.SecondLevel:
                MultiplerOfDamage = 0.7f;
                break;
            case LevelObject.ThirdLevel:
                MultiplerOfDamage = 0.5f;
                break;
        }
    }

    public float ReturnNewDamage(float Damage)
    {
        Damage *= MultiplerOfDamage;

        return Damage;
    }

}
