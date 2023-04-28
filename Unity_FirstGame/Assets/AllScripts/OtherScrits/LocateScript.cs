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
    [SerializeField] private float MaxDistance = 14.0f;

    
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
        if (other.gameObject.CompareTag("Player01"))
        {
            Target = other.gameObject;
            StelsScript = Target.gameObject.GetComponent<StelsScript>();

            if (StelsScript && !StelsScript.StelsOn)
            {
                Agr = true;                
            }
            
            
            
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

            if (Physics.Raycast(transform.position, Target.transform.position))            
            {               
                Debug.DrawLine(transform.position, Target.transform.position, Color.yellow);                    
                transform.rotation = Quaternion.LookRotation(target);

                
            }
            if (Physics.Raycast(ForwardZombie, out RaycastHit HitResult01, MaxDistance))
            {
                Debug.DrawLine(Head.position, Head.forward * MaxDistance + Head.position, Color.green);

                if (Agr && HitResult01.collider.gameObject.tag == "Player01" && Rigidbody && !StelsScript.StelsOn)
                {
                    Rigidbody.isKinematic = false;
                    gameObject.transform.localPosition += gameObject.transform.forward * SpeedForMove;

                    
                }
                else if (HitResult01.collider.gameObject.tag != "Player01")
                {
                    Target = null;
                    Rigidbody = null;
                    StelsScript = null;
                    Agr = false;

                }
                Debug.Log(HitResult01.collider.gameObject.tag);
            }

        }                        


    }
}
