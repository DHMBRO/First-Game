using UnityEngine;
using UnityEngine.AI;
public class PatrolScriptNavMesh : MonoBehaviour
{
    [SerializeField] public PointControllScript MyPointControllScript;
    public int CurrentPoint;
    public int StartPosIndex = 0;
    LocateScript ZombieLocateScript;
    public NavMeshAgent ZombieNavMesh;
    Vector3 MoveTarget;
    public bool NeedCheckPosition = false;
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
           /* StateIdle();*/
        }
        else if (MyState == State.Moving)
        {
            StateMoving();
        }
    }
    public void MoveTo(Vector3 Target)
    {
        /*
          if (!IsPatrol)
          {
              MoveTarget = Target;
          }
          NeedCheckPosition = !IsPatrol;
        */
        ZombieNavMesh.SetDestination(Target);
        MyState = State.Moving;
    }

  /*  public void StateIdle()
    {
        if (!NeedCheckPosition)
        {
            MoveTo(MyPointControllScript.Points[CurrentPoint].transform.position, true);
            MyState = State.Moving;
        }
    }*/
    void StateMoving()
    {
        if (ZombieNavMesh.remainingDistance < 1.0f)
        {
            if (NeedCheckPosition)
            {
                MoveTo(MoveTarget);
            }
            else
            {
                GoToNextPos();
            }
        }
        else
        {
            if (ZombieNavMesh.isStopped)
            {

            }
        }

    }
    public void ReturnToPatrol()
    {
        MoveTo(MyPointControllScript.Points[CurrentPoint].transform.position);
    }
    
    public void CheckPosition(Vector3 CheckingPosition)
    {
        MoveTo(CheckingPosition);
        MyState = State.Moving;
    }
    public bool IsReachTarget()
    {
        return ZombieNavMesh.hasPath && ZombieNavMesh.remainingDistance < 2.0f;
    }
    public void GoToNextPos()
    {
        CurrentPoint = MyPointControllScript.SearchNextPosition(CurrentPoint);
        MoveTo(MyPointControllScript.Points[CurrentPoint].transform.position);
    }
}
