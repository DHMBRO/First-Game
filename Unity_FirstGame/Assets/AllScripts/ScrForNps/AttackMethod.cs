using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMethod : MonoBehaviour
{
    protected float AttackDelay = 3.0f;
    protected float AttackTime = 3.5f;
    protected bool CanAttack;
    protected float ZombieDamage = 5;
    [SerializeField] public float AttackDistance;
    [SerializeField] public float GoingDistance;
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
        
    public void DoCloseAttack(GameObject Target)
    {

        if (Time.time >= AttackTime )
        {

            Target.GetComponent<HpScript>()?.InflictingDamage(ZombieDamage);
            AttackTime = AttackDelay + Time.time;
            
        }
       
    }
}
