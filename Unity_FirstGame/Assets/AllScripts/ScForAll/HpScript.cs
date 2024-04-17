using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HpScript : MonoBehaviour
{
    [SerializeField] public UpdateState StateDelegate;

    [SerializeField] private Image UiHp;
    [SerializeField] Transform ObjectForCopy;
    [SerializeField] private TextMeshProUGUI ProzentHealPoint;
    [SerializeField] private RagdollControler MyRagdollControler;

    protected PatrolScriptNavMesh MyNavMesh;
    protected InfScript Info;

    [SerializeField] public float HealthPoint = 10.0f;
    [SerializeField] public float MaxHp;
    
    [SerializeField] public EnemyState State;
    [SerializeField] public Live MyLive = Live.Alive; 
    
    [SerializeField] public bool StelthKill = false;
    [SerializeField] private bool WorkWithRagdollControler = false;
    [SerializeField] private bool WorkWithLightControler = false;

    
    public enum Live
    {
        Alive,
        NotAlive
    }

    private void Start()
    {
       
        MyNavMesh = gameObject.GetComponent<PatrolScriptNavMesh>();
        Info = gameObject.GetComponentInParent<InfScript>();
        if (!Info && State != EnemyState.Player)
        {
            //Debug.Log(gameObject.name + " InfoScr = Null" );
        }
        if (UiHp && ProzentHealPoint) OutPutHp(HealthPoint, UiHp, ProzentHealPoint);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.G) && State != EnemyState.Player)
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
        if ((HealthPoint - Damage) <= 0.0f)
        {
            Debug.Log("1");
            
            HealthPoint = 0;
            MyLive = Live.NotAlive;

            if (StateDelegate != null)
            {
                Debug.Log("2");
                StateDelegate(false);
            }


            if (State != EnemyState.Another)
            {
                if(MyNavMesh && MyNavMesh.enabled)
                {
                    MyNavMesh.ZombieNavMesh.isStopped = true;
                }

                if (StelthKill == false)
                {
                    Info.SetAnimation("Death");
                    Debug.Log("Call animation is working");
                }
            }

            if (WorkWithRagdollControler && MyRagdollControler)
            {
               // CallRagdollControler();
               // Invoke("CallRagdollControler", 6.117f);
               //MyRagdollControler.SetRagdol(true);

            }
        }
        else if ((HealthPoint - Damage) > 0.0f)
        {
            HealthPoint -= Damage;
            if(WorkWithRagdollControler && MyRagdollControler)
            {
                MyRagdollControler.SetRagdollDelegat(false);
            }
        }

        if (HealthPoint >= 0.0f && UiHp && ProzentHealPoint) OutPutHp(HealthPoint, UiHp, ProzentHealPoint);
        
        if (MyLive == Live.NotAlive && WorkWithLightControler)
        {
            GetComponentInParent<LightControl>().EliminateTheLight();
            
        }

    }

    public void CallRagdollControler()
    {
        Debug.Log("CallRagdollControler is working");


        if (!ObjectForCopy)
        {
            return;
        }
        if (WorkWithRagdollControler && MyRagdollControler)
        {
            MyRagdollControler.SetRagdol(true);
        }
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
        return HealthPoint > 0.0f;
    }

    public void InstanceKill()
    {   
        InflictingDamage(HealthPoint * 2.0f);
        Debug.Log("InstanceKill <---");
    }
}