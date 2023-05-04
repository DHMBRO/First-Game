using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    [SerializeField] private List<Transform> Point = new List<Transform>();    
    [SerializeField] public Dictionary<string, Transform> Route = new Dictionary<string, Transform>();

    
    [SerializeField] private float Delay = 15.0f;
    [SerializeField] private float MovingTime;    
    [SerializeField] private float SpeedForMove = 10.0f;
        
    
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

        bool MoveToPoint01 = false;
        bool MoveToPoint02 = false;

        if (Time.time >= MovingTime && InPoint02)
        {
            transform.rotation = Quaternion.LookRotation(RotateToPint02);
            MoveToPoint02 = true;
        }
        else if(Time.time >= MovingTime && InPoint01)
        {                
            transform.rotation = Quaternion.LookRotation(RotateToPint01);
            MoveToPoint01 = true;
        }
        
        if(InPoint02 == true && MoveToPoint02 == true)
        {
            MovingTime = Time.time + Deley;            
            MoveToPoint02 = false;            
        }
        else if(InPoint01 == true && MoveToPoint01 == true)
        {
            MovingTime = Time.time + Deley;
            MoveToPoint01 = false;            
        }

        if (MoveToPoint02 && MoveToPoint01)
        {
            transform.localPosition += transform.forward * SpeedForMove; // SetD
        }
        
    }

    

    
}
