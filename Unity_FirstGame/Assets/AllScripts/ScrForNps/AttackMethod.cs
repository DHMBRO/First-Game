using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMethod : MonoBehaviour
{
    protected float AttackDelay = 3.0f;
    protected float AttackTime = 3.5f;
    protected bool  CanAttack;
    protected float ZombieDamage = 5;
    [SerializeField] protected float AttackDistance;
    protected DamageTakerScript DamageTakerScript;
    protected HpScript TargetHpScript;
    protected LocateScript ZombieLocateScript;
    protected PatrolScriptNavMesh ZombiePatrolScript;

    void Start()
    {

        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
        ZombieLocateScript = gameObject.GetComponent<LocateScript>();
        TargetHpScript = ZombieLocateScript.Target.GetComponent<HpScript>();
    }
    void Update()
    {
        
    }
        
    public void DoCloseAttack()
    {
        if (Time.time >= AttackTime && ZombiePatrolScript.ZombieNavMesh.remainingDistance < AttackDistance && TargetHpScript)
        {
            DamageTakerScript.TakeDamage(TargetHpScript.HealthPoint,ZombieDamage);
            AttackTime = AttackDelay + Time.time;
            
        }
       
    }
}
