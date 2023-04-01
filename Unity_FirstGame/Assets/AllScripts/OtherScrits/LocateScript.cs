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
       
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& !StelsScript.StelsOn)
        {
            Agr = true;
            PlayerTransform = other.transform;
            Debug.Log("Located");
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
       Agr = false;
    }
    void Update()
    {
       

        if (Agr && PlayerTransform && Rigidbody && !StelsScript.StelsOn)
        {

            Rigidbody.isKinematic = false;
            
            transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.transform.position, Time.deltaTime);
           /* Vector3 Vector3Target = new Vector3(0.0f, PlayerTransform.transform.position.y, 0.0f);
            transform.Rotate(Vector3Target);*/
            Debug.Log("yanechmo");
        }
    }
}
