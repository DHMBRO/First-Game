using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    [SerializeField] private GameObject ResultMeasured;
    [SerializeField] private Rigidbody PlayerRigidbody;
    [SerializeField] private float RayForJump = 0.01f;
    [SerializeField] private float PowerForJump = 3.0f; 
    private bool JumpAwalable;
    
    void Start()
    {
        
    }


    void Update()
    {
        JumpRay();
        if (ResultMeasured && PlayerRigidbody)
        {
           JumpAwalable = true;
           Debug.Log(JumpAwalable+ "JumpAwwalable");
           if (Input.GetKey(KeyCode.Space))
           {
                Vector3 Jump = new Vector3(0.0f, 1.0f * PowerForJump, 0.0f);
                PlayerRigidbody.AddRelativeForce(Jump,ForceMode.Force);
           }
        }
    }
    void JumpRay()
    {
        Ray RayForCheckJump = new Ray(transform.position, transform.forward * RayForJump);
        if (Physics.Raycast(RayForCheckJump, out RaycastHit Hitresult))
        {
            ResultMeasured = Hitresult.collider.gameObject;
            Debug.Log("ray work");
            
        }
        
    }
}
