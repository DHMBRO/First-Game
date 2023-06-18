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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(ZombiePatrolScript.ZombieNavMesh.destination + Vector3.up * 2, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ZombieLocateScript.Target.transform.position + Vector3.up * 2, 0.5f);
    }
    void Update()
    {
        
        if (IsLive)
        {
            if (IsLive && ZombieLocateScript && ZombieAttackScript && ZombiePatrolScript)
            {

                //перевірка чи я не відійшов від дестенейшена!
                if (ZombieLocateScript.CanISeeTarget() && IsLive) 
                {
                    Debug.Log("I SEE Target");
                    if (IsLive && ZombiePatrolScript.ZombieNavMesh.remainingDistance <= ZombieAttackScript.AttackDistance &&
                            (ZombieLocateScript.Target.transform.position - ZombiePatrolScript.ZombieNavMesh.destination).magnitude <= ZombieAttackScript.GoingDistance)
                    {
                        Debug.Log("WORK");

                        ZombieAttackScript.DoCloseAttack(ZombieLocateScript.Target);
                    }
                    else
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
