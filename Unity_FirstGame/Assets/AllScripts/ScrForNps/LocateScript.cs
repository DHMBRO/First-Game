using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LocateScript : MonoBehaviour
{
    [SerializeField] private Transform Head;
    public GameObject Target = null;
    
    [SerializeField] private string WhatImLooking;
    [SerializeField] private RaycastHit HitResult;
    [SerializeField] private float SpeedForMove;
    [SerializeField] private float MaxDistatzeForAgr;
    public PatrolScriptNavMesh ZombiePatrolScript;
    private StelthScript TargetStelsScript;
    [SerializeField] float VisionAngle = 60.0f;
    List<GameObject> Targets;
    protected HpScript MyHpScript;
    protected float FullVisionDistance = 1.5f;
    
    void Start()
    {
        MyHpScript = gameObject.GetComponent<HpScript>();
        Targets = new List<GameObject>();
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();

    }
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        HpScript TargetHpScript = other.gameObject.GetComponent<HpScript>();
        if (TargetHpScript)
        {
            if (TargetHpScript.State != MyHpScript.State)
            {
                if (TargetHpScript.MyLive == HpScript.Live.Alive)
                {
                    Targets.Add(other.gameObject);
                    DefineMyTarget();
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
        if (Targets.Contains(other.gameObject))
        {
            Targets.Remove(other.gameObject);
            DefineMyTarget();
        }
    }
 
    public void RelocateTarget()
    {
        if ((ZombiePatrolScript.ZombieNavMesh.destination - Target.transform.position).magnitude >= 3.0f)
        {
            ZombiePatrolScript.ZombieNavMesh.SetDestination(Target.transform.position);
        }
    }
    //ToTarget
    public bool CanISeeTarget()
    {
        return CanISee(Target);
    }
    protected bool CanISee(GameObject TestTarget)
    {

        if (!TestTarget)
        {
            return false;
        }
        HpScript Hp = TestTarget.GetComponent<HpScript>();
        if (Hp)
        {
            if (!Hp.IsAlive())
            {
                return false;
            }
        }

        StelthScript TestTargetStelsScript =  TestTarget.GetComponent<StelthScript>();
        if (TestTargetStelsScript)
        {
            if (TestTargetStelsScript.Stelth)
            {
                TestTarget = null;
                TestTargetStelsScript = null;
                return false;
            }
        }

        /* 
         if (IsObjectFromBehinde(Target))
         {
             return false;
         }
        */


        float AngleToTestTarget = Vector3.Angle(gameObject.transform.forward, TestTarget.transform.position - gameObject.transform.position);

        if (AngleToTestTarget <= VisionAngle)
        {

            Vector3 Rotate = TestTarget.transform.position - transform.position;
            Vector3 RotateHead = TestTarget.transform.position - Head.position;
            Ray HeadForward = new Ray(Head.transform.position, RotateHead);

            Head.transform.rotation = Quaternion.LookRotation(RotateHead);
            RaycastHit[] HitResults = Physics.RaycastAll(HeadForward, MaxDistatzeForAgr);
            foreach (RaycastHit HitResult in HitResults)
            {
                if (HitResult.collider.gameObject.transform.root.gameObject == gameObject)
                {
                    continue;
                }
                if (HitResult.collider.gameObject == TestTarget || HitResult.collider.gameObject.transform.root.gameObject == TestTarget)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        if ((TestTarget.transform.position - gameObject.transform.position).magnitude < 1.5f)
        {
            return true;
        }
        return false;

    }

   
    public GameObject WhatForvardToMe(GameObject Object)
    {
        RaycastHit Hitres;
        if (Physics.SphereCast(Object.transform.position, 0.5f, Object.transform.forward, out Hitres, LayerMask.GetMask("Obj Layer")))
        {
            return Hitres.collider.gameObject.transform.root.gameObject;
        }
        return null;
    }
    public bool IsObjectFromBehinde(GameObject Target)
   {
        float AngleToBackTarget = Vector3.Angle(-gameObject.transform.forward, Target.transform.position - gameObject.transform.position);
        if (AngleToBackTarget <= 40.0f) 
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
        for(int i = Targets.Count - 1; Targets.Count >= 0; i-- )
        {
            HpScript Hp = Targets[i].GetComponent<HpScript>();
            if (!Hp.IsAlive())
            {
                Targets.RemoveAt(i);
            }
        }
        foreach (GameObject SingleTarget in Targets)
        {
            if (CanISee(SingleTarget))
            {
                float CurenntDis = (SingleTarget.transform.position - gameObject.transform.position).magnitude;
                if (CurenntDis < MinDistance)
                {
                    NewTarget = SingleTarget;
                    MinDistance = CurenntDis;
                }
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
