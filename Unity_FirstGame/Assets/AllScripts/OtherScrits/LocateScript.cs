using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateScript : MonoBehaviour
{
    RaycastHit HitRes;
    public bool Agr;
    [SerializeField] public StelsScript StelsScript;
    [SerializeField] protected Rigidbody Rigidbody;
    public GameObject Player;
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

    
    void Update()
    {
        Vector3 target = Player.transform.position - gameObject.transform.position;
        Ray ForwardZombie = new Ray(transform.position, transform.forward * MaxDistance);
        
        if (Player) 
        {
            if (Physics.Raycast(transform.position, Player.transform.position))            
            {               
                Debug.DrawLine(transform.position, Player.transform.position, Color.yellow);                    
                transform.rotation = Quaternion.LookRotation(target);

                if(Physics.Raycast(ForwardZombie, out RaycastHit HitResult, MaxDistance))
                {                    
                    Debug.DrawLine(transform.position, transform.forward * MaxDistance, Color.yellow);

                    if (Agr && HitResult.collider.gameObject.CompareTag("Player01") && Rigidbody && !StelsScript.StelsOn)
                    {
                        Rigidbody.isKinematic = false;
                        transform.localPosition += transform.forward * SpeedForMove;                        
                    }
                
                }            
            }            

        }                        

    }
}
