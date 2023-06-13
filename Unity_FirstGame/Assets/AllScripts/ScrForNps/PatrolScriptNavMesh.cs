using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolScriptNavMesh : MonoBehaviour
{
    [SerializeField] public PointControllScript MyPointControllScript;
    protected int CurrentPoint ;
    public int StartPosIndex = 0;
    
    public NavMeshAgent ZombieNavMesh;

    enum State
    {       
        Moving,
        Idle
    }
    State MyState = State.Idle;

    void Start()
    {        
       ZombieNavMesh = gameObject.GetComponent<NavMeshAgent>();
    }


    void Update()                       
    {
        
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

    public void MoveTo(GameObject Target)
    {
        ZombieNavMesh.SetDestination(Target.transform.position);
        MyState = State.Moving;
    }

    public void StateIdle()
    {
        MoveTo(MyPointControllScript.Points[CurrentPoint]);
        MyState = State.Moving;
    }

    void StateMoving()
    {
        if (ZombieNavMesh.remainingDistance < 1.0f)
        {
            CurrentPoint = MyPointControllScript.SearchNextPosition(CurrentPoint);
            MyState = State.Idle;
        }
        else
        {
            if (ZombieNavMesh.isStopped)
            {
                MyState = State.Idle;
            }
        }
    }
    
}
