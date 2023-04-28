using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMethod : MonoBehaviour
{
    float AttackDelay = 3.0f;
    float AttackTime = 3.5f;
    bool CanAttack;
    private float TargetHp = 100;
    private float ZombieDamage;
    [SerializeField] LocateScript LocateScript;
    float Damage = 10.0f;
    void Start()
    {
          
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {

            CanAttack = true;
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {

            CanAttack = false;

        }
    }
    public void DoCloseAttack(float Damage,Vector3 NpcForvard,Vector3 PlayerTransform)
    {
        
        if(Time.time >= AttackTime)
        {
            AttackTime = AttackDelay + Time.time;
            TargetHp -= Damage;
            
        }
    }
    
    void Update()
    {
        if (CanAttack)
        {
            //DoCloseAttack(Damage,LocateScript.gameObject.transform.forward,LocateScript.Target.transform.position - LocateScript.gameObject.transform.position);
           
        }
    }
}
