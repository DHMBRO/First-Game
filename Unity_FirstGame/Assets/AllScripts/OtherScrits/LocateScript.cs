using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateScript : MonoBehaviour
{
    public bool Agr;
    [SerializeField] public StelsScript StelsScript;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] protected Rigidbody Rigidbody;
    void Start()
    {
        Rigidbody = gameObject.GetComponent<Rigidbody>();   
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {
            StelsScript = other.gameObject.GetComponent<StelsScript>();
            PlayerTransform = other.gameObject.GetComponent<Transform>();
            
        }
        if (other.gameObject.CompareTag("Player01") && !StelsScript.StelsOn)
        {
            Agr = true;
            PlayerTransform = other.transform;
            
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
       Agr = false;
    }
    void Update()
    {
       

        if (Agr && PlayerTransform && Rigidbody && StelsScript.StelsOn == false)
        {

            Rigidbody.isKinematic = false;
            
            transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.transform.position, Time.deltaTime);
           
        }
    }
}
