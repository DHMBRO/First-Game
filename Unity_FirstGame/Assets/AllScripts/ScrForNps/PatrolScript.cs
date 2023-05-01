using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    [SerializeField] private List<Transform> Point = new List<Transform>();    
    [SerializeField] public Dictionary<string, Transform> Route = new Dictionary<string, Transform>();

    
    [SerializeField] private float Delay = 15.0f;
    [SerializeField] private float MovingTime;    
    [SerializeField] private float SpeedForMove = 10.0f;
    private byte Counter = 0;
    
    void Start()
    {
        Route.Add("Point1", Point[0]);
        Route.Add("Point2", Point[1]);
    }

    
    void Update()
    {
        Vector3 RotateToPint01 = Point[0].transform.position - transform.position;
        Vector3 RotateToPint02 = Point[1].transform.position - transform.position;

        bool InPoint01 = transform.position == Point[0].position;
        bool InPoint02 = transform.position == Point[1].position;

        bool CanMoveToPoint01 = false;
        bool CanMoveToPoint02 = false;

        Debug.Log("1");
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("2");
            if (InPoint01 && Counter == 0)
            {
                transform.rotation = Quaternion.LookRotation(RotateToPint02);
                CanMoveToPoint02 = true;
                Counter = 1;
                Debug.Log("3");
            }
            else if (InPoint02 && Counter == 0)
            {
                transform.rotation = Quaternion.LookRotation(RotateToPint01);
                CanMoveToPoint01 = true;
                Counter = 1;
                Debug.Log("4");
            }
        }
        
        //xif (true) 
        {
            transform.localPosition += transform.forward * SpeedForMove;
            
            Debug.Log("IsMoving !");
        }
        
        if (InPoint02) 
        {
            CanMoveToPoint02 = false;
            Debug.Log("MoveToPoint02 = false");
        }
        if (InPoint01) 
        {
            CanMoveToPoint01 = false;
            Debug.Log("MoveToPoint01 = false");
        }
        if (Input.GetKeyUp(KeyCode.G) && Counter == 1)
        {
            Counter = 0;
        }    

    }

    

    
}
