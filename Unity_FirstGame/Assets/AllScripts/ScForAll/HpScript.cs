using UnityEngine;
using UnityEngine.UI;

public class HpScript : MonoBehaviour
{
    [SerializeField] public float HealthPoint = 100;
    //[SerializeField] private Image UiHp;
    [SerializeField] public float MaxHp;
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
       // if (HealthPoint > 0.0f && UiHp) OutPutHp(HealthPoint, UiHp);
    }
    
    public void InflictingDamage(float Damage)
    {
        if (MyLive == Live.Alive)
        {
            MinusHp(Damage);
        }
        //if (HealthPoint >= 0.0f && UiHp) OutPutHp(HealthPoint, UiHp);
    }

    void OutPutHp(float HpNow, Image HpByUi)
    {
        HpByUi.fillAmount = HpNow;
    }

}
