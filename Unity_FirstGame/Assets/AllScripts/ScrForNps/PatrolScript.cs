using UnityEngine;
using UnityEngine.AI;
public class PatrolScript : MonoBehaviour
{
    [SerializeField] public PointControllScript MyPointControllScript;
    protected int CurrentPoint;
    public int StartPosIndex = 0;
    LocateScript ZombieLocateScript;
    public NavMeshAgent ZombieNavMesh;
    Vector3 MoveTarget;
    public bool NeedCheckPosition = false;
    public enum State
    {
        Patrol,
        NoPatrol
    }
    public State MyState = State.Patrol;
    void Start()
    {
        ZombieLocateScript = gameObject.GetComponent<LocateScript>();
        ZombieNavMesh = gameObject.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
       
    }

    public void MoveTo(Vector3 Target)
    {
        
        ZombieNavMesh.SetDestination(Target);
        
    }

    void Patrol()
    {
        CurrentPoint = MyPointControllScript.SearchNextPosition(CurrentPoint);
        MoveTo(MyPointControllScript.Points[CurrentPoint].transform.position);
    }
    void ReturnToPatrol() // вернення до патролу
    {
        if (!ZombieLocateScript.Target)
        {
            CurrentPoint = MyPointControllScript.SearchNextPosition(CurrentPoint);
            MoveTo(MyPointControllScript.Points[CurrentPoint].transform.position);
        }
    }
    public void CheckPosition(Vector3 CheckingPosition) // перевірити позицію
    {
        MoveTo(CheckingPosition);
    }
}