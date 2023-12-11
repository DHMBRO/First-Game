using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LocateScript : EnemyController
{
    [SerializeField] private Transform Head;
    public GameObject Target = null;
    [SerializeField] EnemyState State;
    [SerializeField] private string WhatImLooking;
    [SerializeField] private RaycastHit HitResult;
    [SerializeField] private float SpeedForMove;
    [SerializeField] private float MaxDistatzeForAgr;
    public PatrolScriptNavMesh ZombiePatrolScript;
    private StelthScript TargetStelsScript;
    [SerializeField] float VisionAngle = 40.0f;
    List<GameObject> Targets;

    void Start()
    {
        Targets = new List<GameObject>();
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();

    }
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<EnemyController>())
        {
            HpScript TargetHpScript = other.gameObject.GetComponent<HpScript>();
            LocateScript EnemyLocateScript = other.gameObject.GetComponent<LocateScript>();
            if (EnemyLocateScript && TargetHpScript)
            {
                if (State != EnemyLocateScript.State)
                {
                    if (TargetHpScript.MyLive == HpScript.Live.Alive)
                    {
                        Targets.Add(other.gameObject);
                        DefineMyTarget();
                    }
                }
            }

        }

    }
    /* private void OnTriggerStay(Collider other)
     {

         if (other.gameObject.GetComponentInParent<EnemyController>())
         {
             LocateScript EnemyLocateScript = other.gameObject.GetComponent<LocateScript>();
             if (State != EnemyLocateScript.State)
             {
                 Target = other.gameObject;
             }
         }
     }*/
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Target)
        {
            Targets.Remove(Target);
            DefineMyTarget();
        }
    }
    public bool CanISeeTarget()
    {

        if (!Target)
        {
            return false;
        }



        if (TargetStelsScript)
        {
            if (TargetStelsScript.Stelth)
            {
                Target = null;
                TargetStelsScript = null;
                return false;
            }
        }

        if (IsObjectFromBehinde(Target))
        {
            return false;
        }
        

        float AngleToTarget = Vector3.Angle(gameObject.transform.forward, Target.transform.position - gameObject.transform.position);
        // Debug.Log("Angel To Target = " + AngleToTarget);    
        if (AngleToTarget <= VisionAngle)
        {
            Vector3 Rotate = Target.transform.position - transform.position;
            Vector3 RotateHead = Target.transform.position - Head.position;
            Ray HeadForward = new Ray(Head.transform.position, RotateHead);

            Head.transform.rotation = Quaternion.LookRotation(RotateHead);
            // RaycastHit[] HitResults = Physics.RaycastAll(HeadForward, MaxDistatzeForAgr);
            //  foreach (RaycastHit HitResult in HitResults)
            RaycastHit Hitres;
            if (Physics.Raycast(HeadForward, out Hitres, MaxDistatzeForAgr))
            {
                if (Hitres.collider.gameObject == Target || Hitres.collider.gameObject.transform.root.gameObject == Target)
                {
                    return true;
                }
            }
            if ((gameObject.transform.position - Target.transform.position).magnitude <= 2.5f)
            {
                return true;
            }
        }
      return false;
    }
    public void RelocateTarget()
    {

        if ((ZombiePatrolScript.ZombieNavMesh.destination - Target.transform.position).magnitude >= 3.0f)
        {
            ZombiePatrolScript.ZombieNavMesh.SetDestination(Target.transform.position);
        }
    }

   
    public GameObject WhatForvardToMe(GameObject Object)
    {
        RaycastHit Hitres;
        if (Physics.Raycast(Object.transform.position, Object.transform.forward, out Hitres))
        {
            RaycastHit Hitresult;
            if (Physics.Raycast(Object.transform.position, Object.transform.forward - Hitres.collider.gameObject.transform.position, out Hitresult))
            {
                if (Hitresult.collider.gameObject == Hitres.collider.gameObject)
                {
                    return Hitres.collider.gameObject;
                }

            }

        }
        return null;
    }
    public bool IsObjectFromBehinde(GameObject Target)
    {
        float AngleToBackTarget = Vector3.Angle(-gameObject.transform.forward, -Target.transform.position - gameObject.transform.position);
        if (AngleToBackTarget <= 30.0f)
        {

            return true;
        }
        return false;

    }
    // ??????? ?? 2(?? ???? ?? ??,????) ??????? ???????? ?? ???? ?? ???? ??????
    public void DefineMyTarget()
    {

        float MinDistance = float.MaxValue;
        GameObject NewTarget = Target;
        foreach (GameObject SingleTarget in Targets)
        {


            float CurenntDis = (SingleTarget.transform.position - gameObject.transform.position).magnitude;
            if (CurenntDis < MinDistance)
            {
                NewTarget = SingleTarget;
                MinDistance = CurenntDis;
            }


        }
        Target = NewTarget;

    }
    public void ValidateTarget()
    {
        if (Target)
        {
            HpScript TTargetHpScript = Target.GetComponent<HpScript>();
            if (Target && TTargetHpScript && TTargetHpScript.IsAlive())
            {
                return;
            }
            Targets.Remove(Target);
            Target = null;
            DefineMyTarget();
        }


    }
}
