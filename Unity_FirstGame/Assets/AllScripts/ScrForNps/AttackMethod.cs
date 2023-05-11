using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMethod : MonoBehaviour
{
    protected float AttackDelay = 3.0f;
    protected float AttackTime = 3.5f;
    protected bool  CanAttack;
    [SerializeField] protected float AttackDistance;

    protected LocateScript ZombieLocateScript;
    protected PatrolScriptNavMesh ZombiePatrolScript;

    void Start()
    {
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
        ZombieLocateScript = gameObject.GetComponent<LocateScript>();
    }
    void Update()
    {
        
    }
        
    public void DoCloseAttack(/*Damage*/)
    {
        if (Time.time >= AttackTime && ZombiePatrolScript.ZombieNavMesh.remainingDistance < AttackDistance)
        {
            //TargetHp -= Damage
            AttackTime = AttackDelay + Time.time;
            Debug.Log("Attack");
        }
       
    }
}
