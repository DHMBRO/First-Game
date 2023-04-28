using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateScript : MonoBehaviour
{    
    [SerializeField] public bool Agr;
    [SerializeField] public Transform Head; 

    [SerializeField] public GameObject Player;
    [SerializeField] protected Rigidbody Rigidbody;
    [SerializeField] public StelsScript StelsScript;

    [SerializeField] private float SpeedForMove = 0.01f;
    [SerializeField] private float MaxDistance = 10.0f;

    
    void Start()
    {
        Rigidbody = gameObject.GetComponent<Rigidbody>();   
        
    }

    private void OnTriggerEnter(Collider other)
    {              
        if (other.gameObject.CompareTag("Player01") && !StelsScript.StelsOn)
        {
            Agr = true;
            Player = other.gameObject;
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
        
        if (Player) 
        {
            Vector3 target = Player.transform.position - gameObject.transform.position;
            Ray ForwardZombie = new Ray(Head.position, Head.forward);

            if (Physics.Raycast(transform.position, Player.transform.position))            
            {               
                Debug.DrawLine(transform.position, Player.transform.position, Color.yellow);                    
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
