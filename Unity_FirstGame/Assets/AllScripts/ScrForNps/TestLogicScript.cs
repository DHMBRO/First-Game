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
    [SerializeField] public Types MyType = Types.Patroller;
    [SerializeField] GameObject[] Points;// незнаю на рахунок цього масиву точок для стейту Вотчінг
    public enum Types
    {
        Camper,
        Patroller //&
    }
        
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
            if (MyType == Types.Camper) // зміни 
            {
                if (DoISeeEnemy())
                {
                    if (CanIAttack())
                    {
                        InfOwner.SetState(new AttackState(InfOwner));
                    }
                    else
                    {
                        InfOwner.SetState(new FollowTargetState(InfOwner));
                    }
                }
                else 
                {
                    InfOwner.SetState(new GuardState(InfOwner));
                }
                if (InfOwner.IHearSomething())
                {
                    InfOwner.SetState(new CamperCheckNoice(InfOwner));
                }


            }
            else if (MyType == Types.Patroller) // зміни
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
                Attack.StartAttack(Locate.Target);
            }
            else
            {
                Attack.StopAttack();
                Patrol.ZombieNavMesh.isStopped = false;
                InfOwner.SetState(new ChaseState(InfOwner));
            }
        }
        else
        {
            Attack.StopAttack();
            Patrol.ZombieNavMesh.isStopped = false;
            InfOwner.SetState(new PatrolState(InfOwner)); // TODO  Check Last Position of enemy
        }
    }
}
public class GuardState : ILogic
{
    public GuardState(InfScript NewOwner) : base(NewOwner)
    {
    }
    public override void Update()
    {
        
        

    }
}
public class FollowTargetState : ILogic
{
    public FollowTargetState(InfScript NewOwner) : base(NewOwner)
    {
    }
    public override void Update()
    {
        if (!DoISeeEnemy())
        {
            InfOwner.SetState(new GuardState(InfOwner));
            return;
        }
       //Rotation *y* only
        Vector3 NewRotation = Vector3.Lerp(gameObj.transform.forward, Locate.Target.transform.position, 0.1f);
        gameObj.transform.rotation.SetLookRotation(NewRotation); // Tips* Залежність від часу(DeltaTime)
        if (CanIAttack())
        {
            InfOwner.SetState(new CamperAttackState(InfOwner));
        }
    }
}

public class CamperAttackState : ILogic
{
    public CamperAttackState(InfScript NewOwner) : base(NewOwner) 
    { 

    }
    override public void Update()
    {
        if (DoISeeEnemy())
        {
            if (CanIAttack())
            {
                Attack.StartAttack(Locate.Target);
            }
            else
            {
                Attack.StopAttack();
                InfOwner.SetState(new FollowTargetState(InfOwner));
            }
        }
        else
        {
            Attack.StopAttack();
            InfOwner.SetState(new GuardState(InfOwner)); 
        }
        
    }
}
public class CamperCheckNoice : ILogic
{
    public CamperCheckNoice (InfScript NewOnwer) : base(NewOnwer)
    {

    }
    public override void Update()
    {
        if(InfOwner.IHearSomething())
        {
           //lerp
           gameObj.transform.rotation.SetLookRotation(InfOwner.UNeedToCheckThis());
        }
        else if (DoISeeEnemy())
        {
            InfOwner.SetState(new FollowTargetState(InfOwner));

        }
        else
        {
            InfOwner.SetState(new GuardState(InfOwner));
        }

    }
}