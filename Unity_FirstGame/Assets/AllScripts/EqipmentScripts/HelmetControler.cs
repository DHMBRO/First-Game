using UnityEngine;

public class HelmetControler : MonoBehaviour, IDamageAbsrption
{

    [SerializeField] public LevelObject LevelHelmet;
    [SerializeField] public bool Use;
    [SerializeField] float Health = 100.0f;

    private void Start()
    {
        switch (LevelHelmet)
        {
            case LevelObject.FirstLevel:
                Health = 100.0f;
                break;
            case LevelObject.SecondLevel:
                Health = 150.0f;
                break;
            case LevelObject.ThirdLevel:
                Health = 225.0f;
                break;
        }
    }

    public float ReturnNewDamage(float Damage)
    {
        if (Health > Damage)
        {
            Health -= Damage;
            Damage = 0.0f;
        }
        else
        {
            Damage -= Health;
            Health = 0.0f;
        }

        return Damage;
    }

}
