using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpScript : MonoBehaviour
{
    public Live MyLive;
    [SerializeField] public float HealthPoint = 100;
    public float MaxHp = 100;
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

    }
    void SetHp(float NewHp)
    {
        HealthPoint = NewHp;
        Debug.Log(this.name = "HalthPoint ==== " + HealthPoint);
    }
    public void HealHp(float Heal) 
    {
        if (MyLive == Live.Alive)
        {
            if ((HealthPoint + Heal) >= MaxHp)
            {
                SetHp(MaxHp);
            }
            else if ((HealthPoint + Heal) < MaxHp)
            {
                SetHp(HealthPoint + Heal);
            }

        }
      
    }

    public void TakeDamage(float Damage)
    {
        if (Damage >= HealthPoint)
        {
            MyLive = HpScript.Live.NotAlive;
        }
        else if (Damage < HealthPoint)
        {
            SetHp(HealthPoint - Damage);
            
        }
    }
}
