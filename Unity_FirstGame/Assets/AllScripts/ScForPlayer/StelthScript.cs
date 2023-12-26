using UnityEngine;

public class StelthScript : MonoBehaviour
{
    public bool Stelth;
    
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bach") && Input.GetKey(KeyCode.X))
        {
            Stelth = true;            
        }
        else if(other.gameObject.CompareTag("Bach") && !Input.GetKey(KeyCode.X))
        {
            Stelth = false;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Stelth = false;
       
    }


}
