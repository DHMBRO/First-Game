using UnityEngine;
using UnityEngine.UI;
public class HpScript : MonoBehaviour
{
    public Live MyLive;
    [SerializeField] public float HealthPoint = 100;
    [SerializeField] private Image HpUi; 



    public enum Live 
    {
        Alive,
        NotAlive
    }

    void Start()
    {
       
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
            OutPutHp(Hp);
        }
      
    }
    
    void OutPutHp(float HpNow)
    {
        HpUi.fillAmount = HpNow;
    } 


}
