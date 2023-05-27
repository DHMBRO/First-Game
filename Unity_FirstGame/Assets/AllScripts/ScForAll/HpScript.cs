using UnityEngine;
using UnityEngine.UI;

public class HpScript : MonoBehaviour
{
    [SerializeField] public float HealthPoint = 100;
    [SerializeField] private Image UiHp;

    public Live MyLive;

    public enum Live 
    {
        Alive,
        NotAlive
    }

    void Update()
    {
        Debug.Log(HealthPoint);
    }
   
    void HealHp(float Hp,float Heal,float MaxHp) 
    {
        if (MyLive == Live.Alive)
        {
            if ((Hp + Heal) >= MaxHp)
            {
                Hp = MaxHp;
            }
            else if ((Hp + Heal) < MaxHp)
            {
                Hp += Heal;
            }
        }
        if (Hp > 0.0f && UiHp) OutPutHp(Hp, UiHp);
    }
    
    void InflictingDamage(float Hp, float Damage, float MinHp)
    {
        if (MyLive == Live.Alive)
        {
            if ((Hp - Damage) <= MinHp)
            {
                Hp = MinHp;
            }
            else if ((Hp - Damage) > MinHp)
            {
                Hp -= Damage;
            }
        }
        if (Hp >= 0.0f && UiHp) OutPutHp(Hp, UiHp);
    }

    void OutPutHp(float HpNow, Image HpByUi)
    {
        HpByUi.fillAmount = HpNow;
    }

}
