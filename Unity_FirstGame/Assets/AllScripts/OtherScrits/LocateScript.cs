using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateScript : MonoBehaviour
{    
    [SerializeField] public bool Agr;
    [SerializeField] public Transform Head; 

    [SerializeField] public GameObject Target;
    [SerializeField] protected Rigidbody Rigidbody;
    [SerializeField] public StelsScript StelsScript;

    [SerializeField] private float SpeedForMove = 0.01f;
    [SerializeField] private float MaxDistance = 10.0f;

    
    void Start()
    {
        Rigidbody = gameObject.GetComponent<Rigidbody>();
        if (Target)
        {
            StelsScript = Target.GetComponent<StelsScript>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {              
        if (!Target && other.gameObject.CompareTag("Player01") && !StelsScript.StelsOn)
        {
            Agr = true;
            Target = other.gameObject;
            StelsScript = other.gameObject.GetComponent<StelsScript>();                    
        }
       
    }

    void MoveTo()
    {
        Rigidbody.isKinematic = false;
        gameObject.transform.localPosition += gameObject.transform.forward * SpeedForMove;

    }
    
    void Update()
    {
        
        if (Target) 
        {
            Vector3 target = Target.transform.position - gameObject.transform.position;
            Ray ForwardZombie = new Ray(Head.position, Head.forward);

            StelsScript = Target.GetComponent<StelsScript>();

            if (Physics.Raycast(transform.position, Target.transform.position))            
            {               
                Debug.DrawLine(transform.position, Target.transform.position, Color.yellow);                    
                transform.rotation = Quaternion.LookRotation(target);

                if(Physics.Raycast(ForwardZombie, out RaycastHit HitResult, MaxDistance))
                {                    
                    Debug.DrawLine(Head.position, Head.forward * MaxDistance + Head.position, Color.green);

                    if (Agr && HitResult.collider.gameObject.CompareTag("Player01") && Rigidbody && !StelsScript.StelsOn)
                    {
                        Rigidbody.isKinematic = false;
                        gameObject.transform.localPosition += gameObject.transform.forward * SpeedForMove;                        
                    }
                
                }            
            }            

        }                        

    }
}
