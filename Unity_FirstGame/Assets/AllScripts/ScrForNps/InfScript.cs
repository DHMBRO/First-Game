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
   

    void Start()
    {
        HpScript = this.gameObject.GetComponent<HpScript>();
        Locate = this.gameObject.GetComponent<LocateScript>();
        Patrol = this.gameObject.GetComponent<PatrolScriptNavMesh>();
        Attack = this.gameObject.GetComponent<AttackMethod>();
        SoundTaker = this.gameObject.GetComponent<SoundTakerScript>();
        SetState(new PatrolState(this));
    }

    void Update()
    {
        if (CurrentState != null)
        { 
            CurrentState.Update();
        }

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
    public void GoPosition()//TODO FINISH 
    {
        Patrol.GoToNextPos();
    }
}
