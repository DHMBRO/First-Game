using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    protected LocateScript ZombieLocateScript;
    protected AttackMethod ZombieAttackScript;
    protected PatrolScriptNavMesh ZombiePatrolScript;
    public bool IsLive = true;

    void Start()
    {
        ZombieLocateScript = gameObject.GetComponent<LocateScript>();
        ZombieAttackScript = gameObject.GetComponent<AttackMethod>();
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
    }

    
    void Update()
    {
        if (IsLive)
        {
            if (IsLive && ZombieLocateScript && ZombieAttackScript && ZombiePatrolScript)
            {

                if (ZombieLocateScript.CanISeeTarget() && IsLive)
                {

                    if (IsLive && ZombiePatrolScript.ZombieNavMesh.remainingDistance <= ZombieAttackScript.AttackDistance &&
                        (ZombieLocateScript.Target.transform.position - ZombiePatrolScript.ZombieNavMesh.destination).magnitude <= ZombieAttackScript.GoingDistance)
                    {
                        ZombieAttackScript.DoCloseAttack(ZombieLocateScript.Target);
                    }
                    else if (IsLive)
                    {
                        ZombiePatrolScript.MoveTo(ZombieLocateScript.Target);
                    }
                }
                else if (IsLive)
                {
                    ZombiePatrolScript.Patroling();
                }
            }
        }
        else
        
        {

            ZombiePatrolScript.ZombieNavMesh.isStopped = true;
        }
    }
}
