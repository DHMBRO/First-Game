using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    [SerializeField] private List<Transform> Point = new List<Transform>();
    [SerializeField] public Dictionary<string, Transform> Route = new Dictionary<string, Transform>();   

    [SerializeField] float SpeedForMove = 10.0f;
    [SerializeField] float Delay = 5.0f;
    [SerializeField] float MovingTime;
    
    

    void Start()
    {
        
    }

    void Update()
    {
        if (gameObject.transform.position == Points[0].transform.position)
        {
            MovingTime = Time.time + Delay;
            
        }
        else if (gameObject.transform.position == Points[1].transform.position)
        {
            MovingTime = Time.time + Delay;
            
        }



    }
}
