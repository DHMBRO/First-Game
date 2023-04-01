using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public GameObject result;
    public bool JumpAwalable;
    void Start()
    {
        
    }


    void Update()
    {
        JumpRay();
        if (result != null)
        {
           JumpAwalable = true;
            Debug.Log(JumpAwalable+ "JumpAwwalable");
        }
    }
    Ray JumpRay()
    {
        Ray RayForCheckJump = new Ray(transform.position, transform.forward * 10);
        if (Physics.Raycast(RayForCheckJump, out RaycastHit Hitresult))
        {
            result = Hitresult.collider.gameObject;
            Debug.Log("ray work");
            
        }
        return RayForCheckJump;
    }
}
