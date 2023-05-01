using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{    
    [SerializeField] GameObject[] Points = new GameObject[1];
    
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
