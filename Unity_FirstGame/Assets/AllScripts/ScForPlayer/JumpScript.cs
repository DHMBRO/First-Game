using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{

    [SerializeField] private GameObject ResultRay;
    [SerializeField] private Rigidbody PlayerRigidbody;

    [SerializeField] private int Counter = 0;
    
    [SerializeField] private float DistanzeRay = 0.01f;
    //x[SerializeField] private float PowerForJump = 10.0f;           
    
    [SerializeField] private bool CanWork = true;
    //[SerializeField] private bool CanJump = true;    
    
    void Start()
    {
        
    }

    void Update()
    {
        JumpRay();
        
    }
    
    void JumpRay()
    {
        if (Counter == 0 && CanWork)
        {
            Ray RayForCheckJump = new Ray(transform.position, transform.forward * DistanzeRay);
            if (Physics.Raycast(RayForCheckJump, out RaycastHit Hitresult))
            {
                ResultRay = Hitresult.collider.gameObject;
                Debug.DrawRay(transform.position, transform.forward * DistanzeRay, Color.red);                
                Debug.Log(ResultRay);
                Debug.Log(ResultRay.gameObject.tag);

            }
            
            CanWork = false;
            //CanJump = true;

        }
    }
}
