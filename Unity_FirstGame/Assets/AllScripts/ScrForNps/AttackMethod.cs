using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMethod : MonoBehaviour
{
    protected float ZombieDamage = 5;
    [SerializeField] public float AttackDistance;
    [SerializeField] public float GoingDistance;
    [SerializeField] protected float AttackDelay = 3.0f;
    [SerializeField] protected float AttackTime = 3.5f;

    protected bool CanAttack;

    protected HpScript TargetHpScript;
    protected LocateScript ZombieLocateScript;
    protected PatrolScriptNavMesh ZombiePatrolScript;

    void Start()
    {
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
        ZombieLocateScript = gameObject.GetComponent<LocateScript>();
        if (ZombieLocateScript.Target) TargetHpScript = ZombieLocateScript.Target.GetComponent<HpScript>();
    }

    void Update()
    {
        if (ZombieLocateScript.Target) TargetHpScript = ZombieLocateScript.Target.GetComponent<HpScript>();
    }

    public void DoCloseAttack(GameObject Target)
    {
        if (Time.time >= AttackTime)
        {
            if (Target.GetComponentInParent<HpScript>())
            {
                Target.GetComponentInParent<HpScript>()?.InflictingDamage(ZombieDamage);
                
                AttackTime = AttackDelay + Time.time;
            }
        }

    }

}