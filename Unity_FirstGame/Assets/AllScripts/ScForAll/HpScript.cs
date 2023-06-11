using UnityEngine;
using UnityEngine.UI;

public class HpScript : MonoBehaviour
{
    [SerializeField] private float HealthPoint = 100;
    [SerializeField] private float MaxHp;

    [SerializeField] private Image UiHp;
    [SerializeField] private Text ProzentHealPoint;

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
    void MinusHp(float Damage)
    {
        if ((HealthPoint - Damage) <= 0)
        {
            HealthPoint = 0;
        }
        else if ((HealthPoint - Damage) > 0)
        {
            HealthPoint -= Damage;
        }
    }
    void PlusHp(float Heal)
    {
        if ((HealthPoint + Heal) >= MaxHp)
        {
            HealthPoint = MaxHp;
        }
        else if ((HealthPoint + Heal) < MaxHp)
        {
            HealthPoint += Heal;
        }
    }
    public void HealHp(float Heal) 
    {
        if (MyLive == Live.Alive)
        {
            PlusHp(Heal);
        }
        if (HealthPoint > 0.0f && UiHp && ProzentHealPoint) OutPutHp(HealthPoint, UiHp, ProzentHealPoint);
    }
    
    public void InflictingDamage(float Damage)
    {
        if (MyLive == Live.Alive)
        {
            MinusHp(Damage);
        }
        if (HealthPoint >= 0.0f && UiHp && ProzentHealPoint) OutPutHp(HealthPoint, UiHp, ProzentHealPoint);
    }

    void OutPutHp(float HpNow, Image HpByUi, Text HpByText)
    {
        HpByUi.fillAmount = HpNow;
        
    }

}
