using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HpScript : MonoBehaviour
{
    [SerializeField] private Image UiHp;
    [SerializeField] private TextMeshProUGUI ProzentHealPoint;
    [SerializeField] public EnemyState State;
    [SerializeField] public float HealthPoint = 10;
    [SerializeField] public float MaxHp;
    protected InfScript Info;

    public Live MyLive = Live.Alive;
    
    public enum Live
    {
        Alive,
        NotAlive
    }

    private void Start()
    {

        Info = GetComponentInParent<InfScript>();
        if (!Info)
        {
            Debug.Log(gameObject.name + " InfoScr = Null" );
        }
        if (UiHp && ProzentHealPoint) OutPutHp(HealthPoint, UiHp, ProzentHealPoint);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            InstanceKill();
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
            MyLive = Live.NotAlive;
                Info.SetAnimation("Death");
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
        return HealthPoint > 0;
    }
    public void InstanceKill()
    {
        InflictingDamage(HealthPoint*2f);
    }
}