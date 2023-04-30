using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateScript : MonoBehaviour
{
    [SerializeField] private Transform Head;
    [SerializeField] private Transform HeadPoint;
    [SerializeField] private GameObject Target;    

    [SerializeField] private string WhatImLooking;
    [SerializeField] private RaycastHit HitResult; 
    
    [SerializeField] private float SpeedForMove;
    [SerializeField] private float MaxDistatzeForAgr;

    
    void Start()
    {        
        if (Target)
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {              
        if (other.gameObject.CompareTag("Player01"))
        {
            Target = other.gameObject;            
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {            
            Target = null; 
        }
    }

    void MoveTo()
    {               
        //gameObject.transform.localPosition += gameObject.transform.forward * SpeedForMove;

    }
    
    void Update()
    {
        
        
        
        if (Target)
        {            
            Vector3 Rotate = Target.transform.position - transform.position;
            Ray Detection = new Ray(Head.transform.position, Target.transform.position);
            
            if (Physics.Raycast(Detection, out HitResult))
            {
                Debug.DrawLine(Head.transform.position, Target.transform.position, Color.yellow);
                WhatImLooking = HitResult.collider.gameObject.tag;

                if (Physics.Raycast(transform.position, transform.forward * MaxDistatzeForAgr + transform.position))
                {
                    Debug.DrawLine(transform.position, transform.forward * MaxDistatzeForAgr + transform.position, Color.red);
                    transform.rotation = Quaternion.LookRotation(Rotate);
                }
            }
            
        }
    }
              
}

