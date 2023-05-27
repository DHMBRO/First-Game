using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpScript : MonoBehaviour
{
    public Live MyLive;
    [SerializeField] public float HealthPoint = 100;
    
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

        }
      
    }
    
}
