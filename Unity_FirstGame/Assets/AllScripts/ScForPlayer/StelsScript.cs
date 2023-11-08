using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StelsScript : MonoBehaviour
{
    public bool Stels;
    
    void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bach") && Input.GetKey(KeyCode.X))
        {
            Stels = true;            
        }
        else if(other.gameObject.CompareTag("Bach") && !Input.GetKey(KeyCode.X))
        {
            Stels = false;            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Stels = false;
       
    }
    void Update()
    {
        //Debug.Log(Stels + "Стелс");
    }

}
