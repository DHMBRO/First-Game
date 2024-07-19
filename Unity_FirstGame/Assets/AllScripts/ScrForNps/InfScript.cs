using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfScript : MonoBehaviour
{
    protected HpScript HpScript;
    protected LocateScript Locate;
    protected PatrolScriptNavMesh Patrol;
    public AttackMethod Attack;
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
    [SerializeField] public float WaitingTime;
    public Vector3 InterestPosition;
    public List<GameObject> Allies;
    public float SpiningSpeed = 10.0f;
    protected AlliesCheckingScript AlliesCheckingScript;
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
        AlliesCheckingScript = GetComponent<AlliesCheckingScript>();
        //Animator = this.gameObject.GetComponentInParent<Animator>();

        if (MyType == Types.Patroller)
        {
            SetState(new PatrolState(this));
        }
        else
        {
            SetState(new GuardState(this));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Bullet BulletScr = collision.gameObject.GetComponent<Bullet>();
        if (BulletScr && InterestPosition != Vector3.zero)
        {
           InterestPosition = BulletScr.LauncherBullet.transform.position;
        }
    }
    void Update()
    {

        //Debug.Log(CurrentState.GetType().Name);
        if (CurrentState != null && Alive())
        {
            CurrentState.Update();
        }
        //Debug.Log(CanISeeEnemy() + " Enemy Vision " + gameObject.name);
    }
   
    public void Watch(PointControllScript PointContr)
    {
        if (!PointContr) return;

        //Debug.DrawLine(Attack.GunPos.transform.forward, Attack.GunPos.transform.forward*10, color:Color.blue);
        Vector3 DirectionToTarget = PointContr.GetPosByIndex(PointContr.CurrentPointIndex) - Attack.GunPos.transform.position;
        Quaternion DirectionToTargetQ = Quaternion.LookRotation(DirectionToTarget).normalized;
        if (Vector3.Angle(Attack.GunPos.transform.forward, DirectionToTarget) > 0.1f)//
        {
            Attack.GunPos.transform.rotation = Quaternion.RotateTowards(Attack.GunPos.transform.rotation, DirectionToTargetQ, RotationSpeed * Time.deltaTime);
            gameObject.transform.rotation = Quaternion.LookRotation(Vector3.Lerp(gameObject.transform.forward, PointController.gameObject.transform.forward, RotationSpeed * Time.deltaTime));
        } 
        else
        {
            PointContr.CurrentPointIndex = PointContr.SearchNextPosition(PointContr.CurrentPointIndex);
        }
    }

    public void ReturnToDefaultRotation()
    {
        Vector3 NewRotation = new Vector3(0.0f, Vector3.Lerp(gameObject.transform.forward, PointController.gameObject.transform.root.transform.forward, RotationSpeed * Time.deltaTime).y, 0.0f);
        gameObject.transform.rotation = Quaternion.LookRotation(NewRotation);
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
        //Debug.Log("New State = " + NewState);
    }
    public bool IHearSomething()
    {
        return SoundTaker.IHearSomething || AlliesCheckingScript.ISeeDeadPeople;
    }
   
    public void NullInterest()
    {
        SoundTaker.NullInterest();
        AlliesCheckingScript.NullInterest();
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
        //Debug.Log(Message);
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
    
    /*public Transform WhatIShouldToCheck() 
    {
        foreach(Transform Position in InterestPositions)
        {
            //InterestPositions ;

        }
    }*/
    public Vector3 UNeedToCheckThis()
    {
        return InterestPosition;
    } 
}
