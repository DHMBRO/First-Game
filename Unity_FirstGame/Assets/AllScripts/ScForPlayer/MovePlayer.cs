using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float Force = 1.0f;
    [SerializeField] ForceMode MyForceMode;
    [SerializeField] Rigidbody MyRigidbody;
            
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
    } 
    
    void Update()
    {
        float MoveVertical = Input.GetAxis("Vertical");
        float MoveHorizontal = Input.GetAxis("Horizontal");
    
        if (MoveVertical == 1.0f)
        {
            MyRigidbody.drag = 0.05f;
            Vector3 ForceFronte = new Vector3(0.0f,0.0f,MoveVertical * Force);
            MyRigidbody.AddRelativeForce(ForceFronte, MyForceMode);

        }
        else if (MoveVertical != 1.0f && MoveVertical != -1.0f && MoveHorizontal != 1.0f && MoveHorizontal != -1.0f)
        {
            MyRigidbody.drag = 100.0f;
        }

        else if (MoveVertical == -1.0f)
        {
            MyRigidbody.drag = 0.05f;
            Vector3 ForceBack = new Vector3(0.0f, 0.0f, MoveVertical * Force);
            MyRigidbody.AddRelativeForce(ForceBack, MyForceMode);

        }

        if (MoveHorizontal == 1.0f)
        {
            MyRigidbody.drag = 0.05f;
            Vector3 ForceRight = new Vector3(MoveHorizontal * Force, 0.0f, 0.0f);
            MyRigidbody.AddRelativeForce(ForceRight,MyForceMode);

        }
        else if (MoveHorizontal == -1.0f)
        {
            MyRigidbody.drag = 0.05f;
            Vector3 ForceLeft = new Vector3(MoveHorizontal * Force, 0.0f, 0.0f);
            MyRigidbody.AddRelativeForce(ForceLeft,MyForceMode);

        }

    }
}
