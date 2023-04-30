using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    
    [SerializeField] GameObject[] Points = new GameObject[1];
    float SpeedForMove = 10.0f;
    float Delay  = 5.0f;
    float MovingTime;
    void Start()
    {
        
    }

    void Update()
    {
        if (gameObject.transform.position == Points[0].transform.position)
        {
            MovingTime = Time.time + Delay;
            if (Time.time >= MovingTime)
            { 
                while (gameObject.transform.position != Points[1].transform.position)
                {
                    gameObject.transform.localPosition = Points[1].transform.position * SpeedForMove;
                }
            }
           

        }
        else if (gameObject.transform.position == Points[1].transform.position)
        {
            MovingTime = Time.time + Delay;
            if (Time.time >= MovingTime)
            {
                while (gameObject.transform.position != Points[0].transform.position)
                {
                    gameObject.transform.localPosition = Points[0].transform.position * SpeedForMove;
                }
            }

        }

    }
}
