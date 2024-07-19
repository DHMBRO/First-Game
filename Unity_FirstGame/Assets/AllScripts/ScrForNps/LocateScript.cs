using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LocateScript : MonoBehaviour
{
    
    public BaseInformationScript Target = null;
    private HeadScript MyHeadScript;
    [SerializeField] private string WhatImLooking;
    [SerializeField] private RaycastHit HitResult;
    [SerializeField] private float SpeedForMove;
    [SerializeField] private float MaxDistatzeForAgr;
    public PatrolScriptNavMesh ZombiePatrolScript;
    private StelthScript TargetStelsScript;
    [SerializeField] float VisionAngle = 60.0f;
    List<BaseInformationScript> Targets;
    protected HpScript MyHpScript;
    protected float FullVisionDistance = 1.5f;
    public InfScript MyInfo;
    void Start()
    {
        MyHpScript = gameObject.GetComponent<HpScript>();
        Targets = new List<BaseInformationScript>();
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
        MyHeadScript = gameObject.GetComponent<HeadScript>();
        MyInfo = gameObject.GetComponent<InfScript>();
    }
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        BaseInformationScript BaseInfo = other.GetComponent<BaseInformationScript>();
        if (BaseInfo && BaseInfo.HealthScript && MyHpScript)
        {
            if (BaseInfo.HealthScript.State != MyHpScript.State)
            {
                if (BaseInfo.HealthScript.MyLive == HpScript.Live.Alive)
                {
                    Targets.Add(BaseInfo);
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
        BaseInformationScript BaseInfo = other.GetComponent<BaseInformationScript>();
        if (BaseInfo && Targets.Contains(BaseInfo))
        {
            Targets.Remove(BaseInfo);
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
        DefineMyTarget();
        return Target && CanISee(Target.gameObject);
    }

    public bool CanISee(GameObject TestTarget, bool CheckAliveOnly = true)
    {
        if (!TestTarget)
        {
            return false;
        }
        BaseInformationScript BaseInfo = TestTarget.GetComponent<BaseInformationScript>();
        if (!BaseInfo)
        {
            return false;
        }
        if (CheckAliveOnly)
        {
            HpScript Hp = TestTarget.GetComponent<HpScript>();
            if (!Hp || !Hp.IsAlive())
            {
                return false;
            }
        }


        float CurrentAgrDistance = MaxDistatzeForAgr;
        IlluminationController TestTargetIllumin = TestTarget.GetComponentInParent<IlluminationController>();
        if (TestTargetIllumin)
        {
            CurrentAgrDistance = MaxDistatzeForAgr * TestTargetIllumin.GetIlluminatiLvl();
        }


        StelthScript TestTargetStelsScript = TestTarget.GetComponent<StelthScript>();
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
            Debug.DrawRay(MyHeadScript.GetHeadPosition(), gameObject.transform.forward, Color.blue);

            Vector3 Rotate = TestTarget.transform.position - transform.position;
            Vector3 RotateHead = BaseInfo.MyHeadScript.GetHeadPosition() - MyHeadScript.GetHeadPosition();
            Ray HeadForward = new Ray(MyHeadScript.GetHeadPosition(), RotateHead);

            MyHeadScript.Head.transform.rotation = Quaternion.LookRotation(RotateHead);
            RaycastHit[] HitResults = Physics.RaycastAll(HeadForward, CurrentAgrDistance);
            List<RaycastHit> HitresultsList = new List<RaycastHit>(HitResults);

            HitresultsList.Sort((p1, p2) => p1.distance.CompareTo(p2.distance));


            Debug.Log(" angles: " + AngleToTestTarget + " <= " + VisionAngle + " : " + (AngleToTestTarget <= VisionAngle));

            foreach (RaycastHit HitResult in HitresultsList)
            {
                Debug.Log(HitResult.collider.gameObject.name + " " + HitResult.distance);
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

    public void DefineMyTarget()
    {
        //
        float MinDistance = float.MaxValue;
        BaseInformationScript NewTarget = Target;
        for(int i = Targets.Count - 1; i >= 0; i--)
        {
            HpScript Hp = Targets[i].GetComponent<HpScript>();
            if (!Hp.IsAlive())
            {
                Targets.RemoveAt(i);
            }
        }
        foreach (BaseInformationScript SingleTarget in Targets)
        {
            if (CanISee(SingleTarget.gameObject))
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
            
            if (Target && Target.HealthScript && Target.HealthScript.IsAlive())
            {
                return;
            }
            Targets.Remove(Target);
            Target = null;
            DefineMyTarget();
        }


    }
}
