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
    
    // ������ �� ������� ����� ������ ����� ��� ������ �������
   
        
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
            if (InfOwner.MyType == InfScript.Types.Camper) // ���� 
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
            else if (InfOwner.MyType == InfScript.Types.Patroller) // ����
            {
                if (DoISeeEnemy())
                {
                    if (CanIAttack())
                    {

                        //Debug.Log("ATTACK");

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

    private float MoveTimePoint = 0f;
    private bool IsPatroling = false;
    public PatrolState(InfScript NewOwner):base(NewOwner)
    {
        MoveTimePoint = Time.time;
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
            Patrol.ZombieNavMesh.speed = 1.5f;
            if (Patrol.IsReachTarget())
            {
                if (IsPatroling)
                {
                    IsPatroling = false;
                    MoveTimePoint = Time.time + InfOwner.WaitingTime;
                }
                gameObj.transform.rotation = Quaternion.LookRotation(Vector3.Lerp(gameObj.transform.forward, -gameObj.transform.forward, 0.1f));  
                if (Time.time > MoveTimePoint)
                {
                    StartPatroling(true);
                }  
            }
            else if (!IsPatroling || !Patrol.ZombieNavMesh.hasPath)
            {
                StartPatroling(false);
            }
        }
    }

    void StartPatroling(bool FindNextPoint)
    {
        IsPatroling = true;
        InfOwner.SetFloatToAnim("CurrentSpeed", 0.5f);
        if (FindNextPoint) 
        {
            CurrentPoint = InfOwner.PointController.SearchNextPosition(CurrentPoint);
        }
        Patrol.MoveTo(InfOwner.PointController.Points[CurrentPoint].transform.position);
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
            Patrol.ZombieNavMesh.speed = 5.0f;
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
            InfOwner.SetState(new CheckingLastPositionState(InfOwner));
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
            Patrol.ZombieNavMesh.speed = 3.5f;
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
                Quaternion NewRotation = Quaternion.RotateTowards(gameObj.transform.rotation, Quaternion.LookRotation(Locate.Target.transform.position - gameObj.transform.position),InfOwner.RotationSpeed * Time.deltaTime);
                gameObj.transform.rotation = NewRotation;
                Debug.Log("Work");
                //Gun Rotate
                //Debug.DrawLine(Locate.Target.MyHeadScript.GetHeadPosition(), Attack.GunPos.transform.position, Color.red);
                Vector3 DirectionToTarget = Locate.Target.MyHeadScript.GetHeadPosition() - Attack.GunPos.transform.position;        
                Quaternion DirectionToTargetQ = Quaternion.LookRotation(DirectionToTarget).normalized;

                
                Attack.GunPos.transform.rotation = Quaternion.RotateTowards(Attack.GunPos.transform.rotation, DirectionToTargetQ, 50.0f * Time.deltaTime);
                Attack.StartAttack(Locate.Target.gameObject);
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
            InfOwner.SetState(new CheckingLastPositionState(InfOwner)); // TODO  Check Last Position of enemy
        }
    }
}
public class CheckingLastPositionState : ILogic
{
    public CheckingLastPositionState(InfScript NewOwner) : base(NewOwner)
    {

    }
    override public void Update()
    {
        Patrol.MoveTo(InfOwner.LastKnownEnemyPosition);
        if (DoISeeEnemy())
        {
            InfOwner.SetState(new ChaseState(InfOwner));
            return;
        }
        if (Patrol.IsReachTarget())
        {   
            InfOwner.SetState(new PatrolState(InfOwner));
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
        if (!DoISeeEnemy())
        {
            InfOwner.Watch(InfOwner.PointController);
        }
        if (InfOwner.IHearSomething())
        {
            InfOwner.SetState(new CamperCheckNoice(InfOwner));
        }
        if (DoISeeEnemy())
        {
            InfOwner.SetState(new FollowTargetState(InfOwner)); 
        }
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
            
        } 
        if (DoISeeEnemy()) 
        {
           //follow target(only watching)
            Quaternion NewRotation = Quaternion.RotateTowards(gameObj.transform.rotation, Quaternion.LookRotation(Locate.Target.transform.position - gameObj.transform.position), 1.0f);
            gameObj.transform.rotation = NewRotation;
        }
         
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
                Attack.StartAttack(Locate.Target.gameObject);
                //Vector3 TargetLocation = Quaternion.RotateTowards(Attack.GunPos.transform.rotation,
                //Quaternion.LookRotation(InfOwner.GetTargetHead() - Attack.GunPos.transform.position), 0.1f * Time.deltaTime).eulerAngles;

                //Body Rotate
                Quaternion NewRotation = Quaternion.RotateTowards(gameObj.transform.rotation, Quaternion.LookRotation(Locate.Target.transform.position - gameObj.transform.position),InfOwner.RotationSpeed * Time.deltaTime);
                gameObj.transform.rotation = NewRotation;

                //Gun Rotate
                Debug.DrawLine(Locate.Target.MyHeadScript.GetHeadPosition(), Attack.GunPos.transform.position, Color.red);
                Vector3 DirectionToTarget = Locate.Target.MyHeadScript.GetHeadPosition() - Attack.GunPos.transform.position;
                Quaternion DirectionToTargetQ = Quaternion.LookRotation(DirectionToTarget).normalized;
                
                Attack.GunPos.transform.rotation = Quaternion.RotateTowards(Attack.GunPos.transform.rotation, DirectionToTargetQ, 50.0f * Time.deltaTime);

              //  Attack.TerroristWeaponScript.gameObject.transform.rotation = Quaternion.RotateTowards(Attack.GunPos.transform.rotation,
                // Quaternion.LookRotation(InfOwner.GetTargetHead() - Attack.GunPos.transform.position), 10.0f * Time.deltaTime);
              //  Debug.DrawLine(Attack.GunPos.transform.position, Attack.GunPos.transform.position +Attack.AttackDistance* Attack.TerroristWeaponScript.gameObject.transform.forward , color: Color.blue,0.1f );
                // Attack.TerroristWeaponScript.gameObject.transform.rotation = 
                // Quaternion.Euler(Attack.TerroristWeaponScript.gameObject.transform.eulerAngles + new Vector3(45.0f, -90.0f, 0.0f));
               
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
            InfOwner.ReturnToDefaultRotation();
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
            InfOwner.NullInterest();
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
public class SpinChecking : ILogic
{
    Quaternion TargetRotation;
    Quaternion StartRotation;
    bool IsIRotatingToStart = false;
    bool DoISpining = true;
    float TimeToSpin;
    public SpinChecking(InfScript NewOnwer) : base(NewOnwer)
    {
        TargetRotation = Quaternion.LookRotation(-gameObj.transform.forward);
        StartRotation = Quaternion.LookRotation(gameObj.transform.forward);
        TimeToSpin = Time.time + InfOwner.WaitingTime;
    }

    public override void Update()
    {
        if (DoISpining)
        {
            UpdateRotation();
        }
        else 
        {
            TimeToRest();
        }

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
        else if(InfOwner.IHearSomething())
        {
            InfOwner.SetState(new CheckPositionState(InfOwner));
        }
    }

    void UpdateRotation()
    {
        Quaternion CurrentTargetRotation = IsIRotatingToStart ? StartRotation : TargetRotation;
        gameObj.transform.rotation = Quaternion.RotateTowards(Quaternion.LookRotation(gameObj.transform.forward), CurrentTargetRotation,
            InfOwner.SpiningSpeed * Time.deltaTime);

        if (Vector3.Angle(gameObj.transform.forward, CurrentTargetRotation.eulerAngles) < 1.0f) 
        {
            if (IsIRotatingToStart)
            {
                InfOwner.SetState(new PatrolState(InfOwner));
            }
            else 
            {
                IsIRotatingToStart = true;
                DoISpining = false;
            }

        }
    }
    void TimeToRest()
    {
        if (Time.time >= TimeToSpin)
        {
            DoISpining = true;
        }
    }


}