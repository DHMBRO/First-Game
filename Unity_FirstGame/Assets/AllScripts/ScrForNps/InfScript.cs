using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfScript : MonoBehaviour
{
    protected HpScript HpScript;
    protected LocateScript Locate;
    protected PatrolScriptNavMesh Patrol;
    protected AttackMethod Attack;
    protected ILogic CurrentState;
    protected SoundTakerScript SoundTaker;
    [SerializeField] protected Animator Animator;
    [SerializeField] public GameObject PointToKillMe;
    [SerializeField] public GameObject PointToShoot;
    public LayerMask ZombiLayerMask;
    [SerializeField] public PointControllScript PointController;
    public BaseInformationScript BaseInfoScript;
    [SerializeField] public float RotationSpeed;
    [SerializeField] public Types MyType;
    

    public enum Types
    {
        Camper,
        Patroller //&
    }
   
    void Start()
    {

        BaseInfoScript = this.gameObject.GetComponent<BaseInformationScript>();
        HpScript = this.gameObject.GetComponent<HpScript>();
        Locate = this.gameObject.GetComponent<LocateScript>();
        Patrol = this.gameObject.GetComponent<PatrolScriptNavMesh>();
        Attack = this.gameObject.GetComponent<AttackMethod>();
        SoundTaker = this.gameObject.GetComponent<SoundTakerScript>();
       
        //Animator = this.gameObject.GetComponentInParent<Animator>();
        
        if(MyType == Types.Patroller)
        {
            SetState(new PatrolState(this));
        }
        else
        {
            SetState(new GuardState(this));
        }
    }

    void Update()
    {

        Debug.Log(CurrentState.GetType().Name);
        if (CurrentState != null && Alive())
        {
            CurrentState.Update();
        }
        //Debug.Log(CanISeeEnemy() + " Enemy Vision " + gameObject.name);
    }
 
    public void Watch(PointControllScript PointContr)
    {
        //Debug.DrawLine(Attack.GunPos.transform.forward, Attack.GunPos.transform.forward*10, color:Color.blue);
        Vector3 DirectionToTarget = PointContr.GetPosByIndex(PointContr.CurrentPointIndex) - Attack.GunPos.transform.position;
        Quaternion DirectionToTargetQ = Quaternion.LookRotation(DirectionToTarget).normalized;
        if (Vector3.Angle(Attack.GunPos.transform.forward, DirectionToTarget) > 0.1f)//
        {
            Attack.GunPos.transform.rotation = Quaternion.RotateTowards(Attack.GunPos.transform.rotation, DirectionToTargetQ, RotationSpeed * Time.deltaTime);
        } 
        else
        {
            PointContr.CurrentPointIndex = PointContr.SearchNextPosition(PointContr.CurrentPointIndex);
        }
    }


    public bool Alive()
    {
        return HpScript.IsAlive();
    }
    public bool DoIHaveATarget()
    {  
       return Locate.Target;
    }
    public bool CanAttack()
    {
        // return ((Patrol.ZombieNavMesh.remainingDistance <= Attack.AttackDistance) &&
        //               ((Locate.Target.transform.position - Patrol.ZombieNavMesh.destination).magnitude <= Attack.GoingDistance));
        return ((Locate.Target.gameObject.transform.position - gameObject.transform.position).magnitude <= Attack.AttackDistance);
    }
    public float HowMuchHpIHave()
    { 
        return HpScript.HealthPoint;
    }
    public bool CanISeeEnemy()
    {
       return Locate.CanISeeTarget();
    }
    public void SetState(ILogic NewState)
    {
       CurrentState = NewState;
    }
    public bool IHearSomething()
    {
        return SoundTaker.IHearSomething;
    }
    public Vector3 UNeedToCheckThis()
    {
        return SoundTaker.InerestPos;
    }
    public void NullInterest()
    {
        SoundTaker.NullInterest();
    }
    public void GoPosition() 
    {
        Patrol.GoToNextPos();
    }
    public void SetAnimation(string Trigger)
    {
        Animator.SetTrigger(Trigger);
    }
    public void SetFloatToAnim(string Trigger,float Value)
    {
        Animator.SetFloat(Trigger, Value);
    }
    public void SetStopped()
    {
        Patrol.ZombieNavMesh.isStopped = true;
    }
    public void StelthDead()   
    {
        SetAnimation("StelthDead");
        SetStopped();
    }
    public void OnStealthKillEnd(string Message)
    {
        Debug.Log(Message);
    }
    public Vector3 PointToAim()
    {
        return PointToShoot.transform.position;
    }
    public bool IsObjectFromBehinde(GameObject Object) 
    {
        return Locate.IsObjectFromBehinde(Object);
    }
    public Vector3 GetTargetHead()
    {
       return BaseInfoScript.MyHeadScript.GetHeadPosition();
    }
}
