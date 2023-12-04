using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI BackUpWidget;
    protected LocateScript ZombieLocateScript;
    protected AttackMethod ZombieAttackScript;
    protected PatrolScriptNavMesh ZombiePatrolScript;
    protected HpScript ZombieHpScript;
   

    void Start()
    {
        
        ZombieHpScript = gameObject.GetComponent<HpScript>();
        ZombieLocateScript = gameObject.GetComponent<LocateScript>();
        ZombieAttackScript = gameObject.GetComponent<AttackMethod>();
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
    }

    private void OnDrawGizmos()
    {

    }
    void Update()
    {

        if (ZombieHpScript.IsAlive())
        {
            /*Debug.Log("Reaming  " + ZombiePatrolScript.ZombieNavMesh.remainingDistance);
            Debug.Log("Attack Dist" + ZombieAttackScript.AttackDistance);
            Debug.Log(" GoingDist " + (ZombieAttackScript.GoingDistance));
            Debug.Log("RealDist " + ((ZombieLocateScript.Target.transform.position - ZombiePatrolScript.ZombieNavMesh.destination).magnitude));
            Debug.Log("If attack " + ((ZombiePatrolScript.ZombieNavMesh.remainingDistance <= ZombieAttackScript.AttackDistance)));
            Debug.Log("If going " + ((ZombieLocateScript.Target.transform.position - ZombiePatrolScript.ZombieNavMesh.destination).magnitude <= ZombieAttackScript.GoingDistance));
            Debug.Log("-------------------------------------");*/
            if (ZombieLocateScript && ZombieAttackScript && ZombiePatrolScript)
            {
                ZombieLocateScript.ValidateTarget();
                if (ZombieLocateScript.CanISeeTarget())
                {
                    ZombieLocateScript.RelocateTarget();
                    //Debug.Log("canISeeTarget");

                    if (ZombieHpScript.IsAlive() && ZombiePatrolScript.ZombieNavMesh.remainingDistance <= ZombieAttackScript.AttackDistance &&
                            (ZombieLocateScript.Target.transform.position - ZombiePatrolScript.ZombieNavMesh.destination).magnitude <= ZombieAttackScript.GoingDistance)
                    {
                        //Debug.Log("Attack!!!");

                        ZombiePatrolScript.ZombieNavMesh.isStopped = true;
                        ZombieAttackScript.Attack(ZombieLocateScript.Target);
    

                    }
                    else
                    {
                        ZombiePatrolScript.ZombieNavMesh.isStopped = false;
                        ZombiePatrolScript.MoveTo(ZombieLocateScript.Target.transform.position);
                    }

                }

                else if (ZombieHpScript.IsAlive())
                {
                    
                    ZombiePatrolScript.ZombieNavMesh.isStopped = false;
                    ZombiePatrolScript.Patroling();
                }

            }
        }
        else

        {

            ZombiePatrolScript.ZombieNavMesh.isStopped = true;
            
        }
        
      //  BackUpWidget.text = ZombieLocateScript.CanISeeTarget().ToString();
    }
    
}