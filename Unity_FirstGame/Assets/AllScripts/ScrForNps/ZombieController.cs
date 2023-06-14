using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    protected LocateScript ZombieLocateScript;
    protected AttackMethod ZombieAttackScript;
    protected PatrolScriptNavMesh ZombiePatrolScript;
    void Start()
    {
        ZombieLocateScript = gameObject.GetComponent<LocateScript>();
        ZombieAttackScript = gameObject.GetComponent<AttackMethod>();
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
    }

    
    void Update()
    {
        if (ZombieLocateScript && ZombieAttackScript && ZombiePatrolScript)
        {

            if (ZombieLocateScript.CanISeeTarget())
            {
                
                if (ZombiePatrolScript.ZombieNavMesh.remainingDistance <= ZombieAttackScript.AttackDistance && 
                    (ZombieLocateScript.Target.transform.position - ZombiePatrolScript.ZombieNavMesh.destination).magnitude <= ZombieAttackScript.GoingDistance)
                {
                    ZombieAttackScript.DoCloseAttack(ZombieLocateScript.Target);
                }
                else 
                {
                    ZombiePatrolScript.MoveTo(ZombieLocateScript.Target);
                }
            }
            else  
            {
                ZombiePatrolScript.Patroling(); 
            }
        }
        


    }
}
