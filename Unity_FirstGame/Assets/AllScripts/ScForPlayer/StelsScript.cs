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
        if (other.gameObject.CompareTag("Bach") && Input.GetKeyDown(KeyCode.X))
        {
            StelsOn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        StelsOn = false;
    }
    void Update()
    {        
    }

}
