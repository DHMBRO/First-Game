using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StelsScript : MonoBehaviour
{
    public bool StelsOn;
    
    
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bach") && Input.GetKey(KeyCode.X))
        {
            StelsOn = true;            
        }
        else if(other.gameObject.CompareTag("Bach") && !Input.GetKey(KeyCode.X))
        {

            StelsOn = false;            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        StelsOn = false;
        //Debug.Log(StelsOn+"123");
    }
    void Update()
    {        
    }

}
