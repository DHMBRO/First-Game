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
    public GameObject StartPos;
    public PointControllScript PointScript;
    public int CurrentPoint = 0;
    public ILogic(InfScript NewOwner) 
    {
        InfOwner = NewOwner;
        gameObj = NewOwner.gameObject;

        Locate = gameObj.GetComponent<LocateScript>();
        Attack = gameObj.GetComponent<AttackMethod>();
        Patrol = gameObj.GetComponent<PatrolScriptNavMesh>();
        PointScript = gameObj.GetComponent<PointControllScript>();

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
    public void DefineState()
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
            InfOwner.SetState(new PatrolState(InfOwner));
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
        if (DoISeeEnemy())
        {
            InfOwner.SetState(new ChaseState(InfOwner));
        }
        else if (InfOwner.IHearSomething())
        {
            InfOwner.SetState(new CheckPositionState(InfOwner));
        }
        else 
        {    
            if (Patrol.IsReachTarget())
            {
                CurrentPoint = PointScript.SearchNextPosition(CurrentPoint);
                Patrol.MoveTo(PointScript.Points[CurrentPoint].transform.position);      
            }
        }
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
           Locate.RelocateTarget();
           Patrol.MoveTo(Locate.Target.transform.position);
        }
        else
        {
            DefineState();
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
        if (Patrol.IsReachTarget())
        {
            InfOwner.NullInterest();
            DefineState();
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
            DefineState();
        }
    }
}