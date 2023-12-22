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
    [SerializeField] protected GameObject PointToKillMe;

    void Start()
    {
        HpScript = this.gameObject.GetComponent<HpScript>();
        Locate = this.gameObject.GetComponent<LocateScript>();
        Patrol = this.gameObject.GetComponent<PatrolScriptNavMesh>();
        Attack = this.gameObject.GetComponent<AttackMethod>();
        SoundTaker = this.gameObject.GetComponent<SoundTakerScript>();
      //  Animator = this.gameObject.GetComponentInParent<Animator>();
        SetState(new PatrolState(this));

    }

    void Update()
    {
        if (CurrentState != null)
        { 
            CurrentState.Update();
        }

    }
    public bool Alive()
    {
        return HpScript.IsAlive();
    }
    public bool DoIHaveATarget()
    {
        if (Locate.Target)
        {
            return true;
        }
        return false;
    }
    public bool CanAttack()
    {
        return ((Patrol.ZombieNavMesh.remainingDistance <= Attack.AttackDistance) &&
                            ((Locate.Target.transform.position - Patrol.ZombieNavMesh.destination).magnitude <= Attack.GoingDistance));

    }
    public float HowMuchHpIHave()
    { 
        return HpScript.HealthPoint;
    }
    public bool CanISeeEnemy()
    {
        return false; //Locate.CanISeeTarget();
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
    public void SetStopped() //Stealth Method
    {
        Patrol.ZombieNavMesh.isStopped = true;
    }
    public void StealthKillCast(GameObject Killer)//Stealth Method TO ALL
    {
        SetStopped();
        Killer.transform.position = PointToKillMe.transform.position;//
        StelthDead();
        
        //SetPlayerAnimation("StealthKill");
    }
    void StelthDead()   //Stealth Method
    {
        SetAnimation("StelthDead");
        SetStopped();
    }
  
}
