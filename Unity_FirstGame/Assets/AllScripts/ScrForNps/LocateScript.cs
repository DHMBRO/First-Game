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
    public PatrolScriptNavMesh ZombiePatrolScript;
    private StelsScript TargetStelsScript;

    void Start()
    {
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();

    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {

            /*if (Vector3.Angle(gameObject.transform.forward, other.gameObject.transform.position- gameObject.transform.position) >= 150)
            {
                
            }*/
            Target = other.gameObject;


                TargetStelsScript = Target.GetComponentInParent<StelsScript>();

                
            

        }
    
    }
    private void OnTriggerStay(Collider other)
    {
        if (Target)
        {
            RelocateTarget();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {

            Target = null;
            TargetStelsScript = null;
            CanISeeTarget();
        }
    }


    public bool CanISeeTarget()
    {
        
        if (!Target)
        {
            
            return false;

        }
        float AngleToTarget = Vector3.Angle(gameObject.transform.forward, Target.transform.position - gameObject.transform.position);
        Debug.Log("AngleTo Target "+AngleToTarget );
        if (AngleToTarget <= 150/2)
        {
            
            if (TargetStelsScript)
            {
                if (TargetStelsScript.Stels)
                {
                    Target = null;
                    TargetStelsScript = null;
                    return false;
                }


            }
            Vector3 Rotate = Target.transform.position - transform.position;
            Vector3 RotateHead = Target.transform.position - Head.position;

            Ray HeadForward = new Ray(Head.transform.position, Head.forward * MaxDistatzeForAgr);

            Head.transform.rotation = Quaternion.LookRotation(RotateHead);

            RaycastHit[] HitResults = Physics.RaycastAll(HeadForward, MaxDistatzeForAgr);
            foreach (RaycastHit HitResult in HitResults)
            {
                if (HitResult.collider.gameObject == Target || HitResult.collider.gameObject.transform.root == Target)
                {
                    Debug.Log("3");
                    return true;
                }

            }

        }
         
       
        else if ((gameObject.transform.position - Target.transform.position).magnitude > 2.5f)
             
        {
            return true;
        }
        else 
        {
            return false;
        }
        
        return false;
    }
    public void RelocateTarget()
    {
        if (ZombiePatrolScript.ZombieNavMesh && Target)             // The new 
        {
            if ((ZombiePatrolScript.ZombieNavMesh.destination - Target.transform.position).magnitude >= 3.0f)
            {

                ZombiePatrolScript.ZombieNavMesh.SetDestination(Target.transform.position);

            }

        }



    }
    
}