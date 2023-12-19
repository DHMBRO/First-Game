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
    protected Animator Animator;

    void Start()
    {
        HpScript = this.gameObject.GetComponent<HpScript>();
        Locate = this.gameObject.GetComponent<LocateScript>();
        Patrol = this.gameObject.GetComponent<PatrolScriptNavMesh>();
        Attack = this.gameObject.GetComponent<AttackMethod>();
        SoundTaker = this.gameObject.GetComponent<SoundTakerScript>();
        Animator = this.gameObject.GetComponent<Animator>();
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
    public void SetFloatToAnim()
    {

    }
}
