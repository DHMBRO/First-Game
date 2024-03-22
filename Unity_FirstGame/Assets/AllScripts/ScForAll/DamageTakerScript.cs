using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakerScript : MonoBehaviour
{
    protected HpScript MyHpScript;
    void Start()
    {
        MyHpScript = GetComponent<HpScript>();
    }

   
    void Update()
    {
        if (MyHpScript)
        {

        }
    }
    public void TakeDamage(float Hp, float Damage)
    {
        if (Damage >= Hp)
        {
            MyHpScript.MyLive = HpScript.Live.NotAlive;
        }
        else if (Damage < Hp)
        {
            Hp =- Damage;
        }
    }
}
