using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateScript : MonoBehaviour
{
    [SerializeField] private Transform Head;    
    public GameObject Target = null;    

    [SerializeField] private string WhatImLooking;
    [SerializeField] private RaycastHit HitResult; 
    
    [SerializeField] private float SpeedForMove;
    [SerializeField] private float MaxDistatzeForAgr;
    PatrolScriptNavMesh ZombiePatrolScript;

    
    void Start()
    {
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
        
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


   
    
    void Update()
    {
        
        
    }              

 
    public bool CanISeeTarget()
    {
        
        if (!Target)
        {
            return false;
           
        }
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
                return true;
                
            }

        }
        return false;
        
    }
    

}
