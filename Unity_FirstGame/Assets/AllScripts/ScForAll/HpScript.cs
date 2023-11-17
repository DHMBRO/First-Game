using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HpScript : MonoBehaviour
{
    [SerializeField] private Image UiHp;
    [SerializeField] private TextMeshProUGUI ProzentHealPoint;

    [SerializeField] public float HealthPoint = 10;
    [SerializeField] private float MaxHp;
    

    public Live MyLive = Live.Alive;
    
    public enum Live
    {
        Alive,
        NotAlive
    }

    private void Start()
    {
        if (UiHp && ProzentHealPoint) OutPutHp(HealthPoint, UiHp, ProzentHealPoint);
    }

    public void InflictingDamage(float Damage)
    {
        if (MyLive == Live.Alive)
        {
            MinusHp(Damage);
        }
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
        if (HealthPoint >= 0.0f && UiHp && ProzentHealPoint) OutPutHp(HealthPoint, UiHp, ProzentHealPoint);
    }

    public void HealHp(float Heal)
    {
        if (MyLive == Live.Alive)
        {
            PlusHp(Heal);
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
        if (HealthPoint > 0.0f && UiHp && ProzentHealPoint) OutPutHp(HealthPoint, UiHp, ProzentHealPoint);
    }


    private void OutPutHp(float HpNow, Image HpByUi, TextMeshProUGUI HpByText)
    {
        HpNow /= 100.0f;
        HpByUi.fillAmount = HpNow;
        HpNow *= 100;
        string HpinString = HpNow.ToString();
        HpByText.text = HpinString + "%";
    }

    public bool IsAlive() 
    {
        
        if (HealthPoint <= 0)
        {
            return false;
        }
        if (HealthPoint > 0)
        {
            return true;
        }
        return false;
    }
    public void InstanceKill()
    {
        InflictingDamage(HealthPoint);
    }
}