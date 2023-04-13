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
            Debug.Log(StelsOn+"789");
        }
        else if(other.gameObject.CompareTag("Bach") && !Input.GetKey(KeyCode.X))
        {

            StelsOn = false;
            Debug.Log(StelsOn+"456");
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
