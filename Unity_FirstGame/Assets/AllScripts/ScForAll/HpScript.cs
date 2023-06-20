using UnityEngine;
using UnityEngine.UI;

public class HpScript : MonoBehaviour
{


    [SerializeField] private Image UiHp;
    [SerializeField] private Text ProzentHealPoint;

    [SerializeField] private float HealthPoint = 100;
    [SerializeField] private float MaxHp;
    ZombieController ControlerForZombie;

    public Live MyLive;

    public enum Live
    {
        Alive,
        NotAlive
    }

    private void Update()
    {
        //Debug.Log("Heal Point: " + HealthPoint);
        if (HealthPoint <= 0.0f)
        {
            ControlerForZombie = gameObject.GetComponent<ZombieController>();
            if (ControlerForZombie) ControlerForZombie.IsLive = false;

            //Debug.Log("Your or your target is dead !");
        }
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


    private void OutPutHp(float HpNow, Image HpByUi, Text HpByText)
    {
        HpNow /= 100.0f;
        HpByUi.fillAmount = HpNow;
        HpNow *= 100;
        string HpinString = HpNow.ToString();
        HpByText.text = HpinString + "%";
    }

}