using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakScriptPrototype : MonoBehaviour
{
    [SerializeField] GameObject BreakingHammer;
    private float AnimTime = 3.0f;
    private Rigidbody PlayerRB;
    public CamFirstFace CamFirstFaceScript;
    private GameObject Wall;
    void Start()
    {
        CamFirstFaceScript = GetComponent<CamFirstFace>();
           PlayerRB = GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider other)
    {
      
  

        if (other.gameObject.CompareTag("Breakful") && gameObject != null /*&& ObjectInHand == BreakingHammer */&& PlayerRB != null && Input.GetKey(KeyCode.E))
        {
            //animation
            Destroy(other.gameObject,AnimTime);//3 - amnim time
            
        }   
    }

    void Update()
    {
        
    }
}
