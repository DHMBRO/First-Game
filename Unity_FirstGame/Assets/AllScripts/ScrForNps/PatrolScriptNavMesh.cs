using UnityEngine;
using UnityEngine.AI;

public class PatrolScriptNavMesh : MonoBehaviour
{
    [SerializeField] public PointControllScript MyPointControllScript;
    protected int CurrentPoint;
    public int StartPosIndex = 0;
    LocateScript ZombieLocateScript;
    public NavMeshAgent ZombieNavMesh;
    GameObject MoveTarget;
    public enum State
    {
        Moving,
        Idle
    }
    public State MyState = State.Idle;

    void Start()
    {

        ZombieLocateScript = gameObject.GetComponent<LocateScript>();
        ZombieNavMesh = gameObject.GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        //Debug.Log(ZombieNavMesh.isStopped);
    }

    public void Patroling()
    {
        if (MyState == State.Idle)
        {
            StateIdle();
        }
        else if (MyState == State.Moving)
        {
            StateMoving();
        }

    }

    public void MoveTo(GameObject Target, bool IsPatrol = false)
    {
        if (!IsPatrol)
        {
            MoveTarget = Target;
        }
        ZombieNavMesh.SetDestination(Target.transform.position);
        MyState = State.Moving;
    }
    
    public void StateIdle()
    {
        if (!MoveTarget)
        {
            MoveTo(MyPointControllScript.Points[CurrentPoint], true);
            MyState = State.Moving;
        }
    }

    void StateMoving()
    {
        if (ZombieNavMesh.remainingDistance < 1.0f)
        {
            if (MoveTarget)
            {
                MyState = State.Idle;
                MoveTarget = null;  
            }
            else
            { 
                CurrentPoint = MyPointControllScript.SearchNextPosition(CurrentPoint);
                MoveTo(MyPointControllScript.Points[CurrentPoint], true);
            }
        }
        else
        {
            if (ZombieNavMesh.isStopped)
            {
                
            }
        }
       
    }
    void ReturnToPatrol()
    {
        if (!ZombieLocateScript.Target)
        {

            CurrentPoint = MyPointControllScript.SearchNextPosition(CurrentPoint);
            MoveTo(MyPointControllScript.Points[CurrentPoint], true);
        }

    }
    protected void CheckPosition(Vector3 CheckingPosition)
    {
        MoveTo(CheckingPosition);
    }
}