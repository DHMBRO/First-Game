using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ILogic 
{
    public InfScript InfOwner;
    public GameObject gameObj;
    public LocateScript Locate;
    public AttackMethod Attack;
    public PatrolScriptNavMesh Patrol;
    public ILogic(InfScript NewOwner) 
    {
        InfOwner = NewOwner;
        gameObj = NewOwner.gameObject;

        Locate = gameObj.GetComponent<LocateScript>();
        Attack = gameObj.GetComponent<AttackMethod>();
        Patrol = gameObj.GetComponent<PatrolScriptNavMesh>();

    }
    abstract public void Update();
    
    public bool DoISeeEnemy()
    {
        return InfOwner.CanISeeEnemy();
    }
    public bool CanIAttack()
    {
        return InfOwner.CanAttack();
    }
    public void States()
    {
        if (DoISeeEnemy())
        {
            if (CanIAttack())
            {
                InfOwner.SetState(new AttackState(InfOwner));
            }
            else
            {
                InfOwner.SetState(new ChaseState(InfOwner));
            }
        }
        else if  (InfOwner.IHearSomething())
        {
            InfOwner.SetState(new CheckPositionState(InfOwner));
        }
        else
        {
               //SetSatatePatrol 
        }
      
    }
}
public class PatrolState : ILogic
{
  
    public PatrolState(InfScript NewOwner):base(NewOwner)
    {
       
    }
    override public void Update()
    {

    }
}
public class ChaseState : ILogic
{
    public ChaseState(InfScript NewOwner) : base(NewOwner)
    {
            
    }

    override public void Update()
    {
        if (DoISeeEnemy())
        {
            Patrol.MoveTo(Locate.Target.transform.position);
        }
    }

}

public class CheckPositionState : ILogic
{
    public CheckPositionState(InfScript NewOwner) : base(NewOwner)
    {
       
    }
    override public void Update()
    {
        Patrol.CheckPosition(InfOwner.UNeedToCheckThis());
        if (!Locate.Target)
        {
            States();
        }
    }
}



public class AttackState : ILogic
{
    public AttackState(InfScript NewOwner) : base(NewOwner)
    {
            
    }
    override public void Update()
    {
        Attack.Attack(Locate.Target);
        if (!Locate.Target)
        {
            States();
        }
    }
}