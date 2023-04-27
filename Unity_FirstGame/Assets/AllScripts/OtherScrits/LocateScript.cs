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
    

    void Start()
    {
        Rigidbody = gameObject.GetComponent<Rigidbody>();   
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
      
        if (other.gameObject.CompareTag("Player01") && !StelsScript.StelsOn )
        {
            Agr = true;
            Player = other.gameObject;
            StelsScript = other.gameObject.GetComponent<StelsScript>();
            
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {



        }
    }
    void Update()
    {
        if (Player) 
        {
            if (Physics.Raycast(gameObject.transform.position, Player.transform.position))
            {
               
                Debug.DrawLine(gameObject.transform.position, Player.transform.position, Color.yellow);
            }
            Debug.DrawLine(gameObject.transform.position, Player.transform.position, Color.yellow);
            Vector3 target = Player.transform.position - gameObject.transform.position;
            if (Agr && Player.transform && Rigidbody && StelsScript.StelsOn == false)
            {

                Rigidbody.isKinematic = false;

                transform.localPosition += transform.forward * SpeedForMove;
                transform.rotation = Quaternion.LookRotation(target);
            }
        }
        
        
        
    }
}
