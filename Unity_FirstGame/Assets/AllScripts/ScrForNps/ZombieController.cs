﻿using System.Collections;
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

    }
    void Update()
    {

        if (IsLive)
        {
            /*Debug.Log("Reaming  " + ZombiePatrolScript.ZombieNavMesh.remainingDistance);
            Debug.Log("Attack Dist" + ZombieAttackScript.AttackDistance);
            Debug.Log(" GoingDist " + (ZombieAttackScript.GoingDistance));
            Debug.Log("RealDist " + ((ZombieLocateScript.Target.transform.position - ZombiePatrolScript.ZombieNavMesh.destination).magnitude));
            Debug.Log("If attack " + ((ZombiePatrolScript.ZombieNavMesh.remainingDistance <= ZombieAttackScript.AttackDistance)));
            Debug.Log("If going " + ((ZombieLocateScript.Target.transform.position - ZombiePatrolScript.ZombieNavMesh.destination).magnitude <= ZombieAttackScript.GoingDistance));
            Debug.Log("-------------------------------------");*/
            if (IsLive && ZombieLocateScript && ZombieAttackScript && ZombiePatrolScript)
            {
                if (ZombieLocateScript.CanISeeTarget() && IsLive )
                {
                    
                    /*Debug.Log("canISeeTarget"); */

                    if (IsLive && ZombiePatrolScript.ZombieNavMesh.remainingDistance <= ZombieAttackScript.AttackDistance &&
                            (ZombieLocateScript.Target.transform.position - ZombiePatrolScript.ZombieNavMesh.destination).magnitude <= ZombieAttackScript.GoingDistance)
                    {
                       /*Debug.Log("Attack!!!"); */


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