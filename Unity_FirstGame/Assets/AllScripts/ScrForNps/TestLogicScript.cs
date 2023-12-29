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
        if (InfOwner.Alive())
        {
            if (DoISeeEnemy())
            {
                if (CanIAttack())
                {
                   
                    Debug.Log("ATTACK");
                    InfOwner.SetState(new AttackState(InfOwner));
                }
                else
                {
                    
                    InfOwner.SetState(new ChaseState(InfOwner));
                }
            }
            else if (InfOwner.IHearSomething())
            {
              
                InfOwner.SetState(new CheckPositionState(InfOwner));
            }
            else
            {
                
                InfOwner.SetState(new PatrolState(InfOwner));
            }
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
                 InfOwner.SetFloatToAnim("CurrentSpeed", 0.5f);
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
            InfOwner.SetFloatToAnim("CurrentSpeed", 1.0f);
            Locate.RelocateTarget();
            Patrol.MoveTo(Locate.Target.transform.position);
            if (CanIAttack())
            {
                InfOwner.SetState(new AttackState(InfOwner));
            }
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
        InfOwner.SetFloatToAnim("CurrentSpeed", 0.5f);
        Patrol.CheckPosition(InfOwner.UNeedToCheckThis());
        //if (Patrol.IsReachTarget())
        

        if (DoISeeEnemy())
        {
            InfOwner.SetState(new ChaseState(InfOwner));
            InfOwner.NullInterest();
        }
        else
        {
            if (Patrol.IsReachTarget())
            {
                InfOwner.SetState(new PatrolState(InfOwner));
                InfOwner.NullInterest();
            }

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
       
        if (DoISeeEnemy())
        {
            if (CanIAttack())
            {
                Patrol.ZombieNavMesh.isStopped = true;
                Attack.Attack(Locate.Target);
            }
            else
            {
                Patrol.ZombieNavMesh.isStopped = false;
                InfOwner.SetState(new ChaseState(InfOwner));
            }
        }
        else
        {
            Patrol.ZombieNavMesh.isStopped = false;
            InfOwner.SetState(new PatrolState(InfOwner)); // TODO  Check Last Position of enemy
        }
    }
}