using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float Force = 1.0f;
    [SerializeField] ForceMode MyForceMode;
    [SerializeField] Rigidbody MyRigidbody;

    [SerializeField] protected Transform CameraTransform;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {

        }    
    }
    
    void Update()
    {
        float MoveVertical = Input.GetAxis("Vertical");
        float MoveHorizontal = Input.GetAxis("Horizontal");

        transform.rotation = Quaternion.Euler(0.0f, CameraTransform.eulerAngles.y, 0.0f);
        
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
