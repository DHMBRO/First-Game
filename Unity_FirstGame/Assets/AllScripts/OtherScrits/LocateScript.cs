using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateScript : MonoBehaviour
{
    [SerializeField] private Transform Head;    
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

        CapsuleCollider capsuleCollider = other.gameObject.GetComponent<CapsuleCollider>();

        if (capsuleCollider && !capsuleCollider.isTrigger)
        {
            if (other.gameObject.CompareTag("Player01"))
            {
                Target = other.gameObject;
            }
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        CapsuleCollider capsuleCollider = other.gameObject.GetComponent<CapsuleCollider>();

        if (capsuleCollider && !capsuleCollider.isTrigger)
        {
            if (other.gameObject.CompareTag("Player01"))
            {
                Target = null;
            }
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
            Vector3 RotateHead = Target.transform.position - Head.position;

            Ray HeadForward = new Ray(Head.transform.position, Head.forward * MaxDistatzeForAgr);

            Head.transform.rotation = Quaternion.LookRotation(RotateHead);

            if (Physics.Raycast(HeadForward, out HitResult))
            {
                Debug.DrawLine(Head.transform.position, Head.forward * MaxDistatzeForAgr + Head.position, Color.red);
                WhatImLooking = HitResult.collider.gameObject.tag;

                if (WhatImLooking == "Player01")
                {
                    Debug.Log("Yes");
                    transform.rotation = Quaternion.LookRotation(Rotate);
                    transform.localPosition += transform.forward * SpeedForMove;
                }
            }                        
        }
    }              
    
    

}

