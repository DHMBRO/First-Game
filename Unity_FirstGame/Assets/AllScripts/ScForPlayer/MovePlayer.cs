using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float Force = 1.0f;
    [SerializeField] ForceMode MyForceMode;
    [SerializeField] Rigidbody MyRigidbody;

    [SerializeField] protected Transform CameraTransform;
    [SerializeField] protected float Sens = 1.0f;


    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Plane"))
        {
            Debug.Log("Yes");
        }
    }

    void Update()
    {
        float MoveVertical = Input.GetAxis("Vertical");
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveRotationY = Input.GetAxis("Mouse X");

        Vector3 Trque = new Vector3(0.0f, MoveRotationY * Sens, 0.0f);
        MyRigidbody.AddRelativeTorque(Trque, ForceMode.Force);
        

        //transform.rotation = Quaternion.Euler(0.0f, CameraTransform.eulerAngles.y, 0.0f);

        if (Input.GetKey(KeyCode.W))
        {
            MyRigidbody.drag = 0f;
            Vector3 ForceFronte = new Vector3(0.0f, 0.0f, MoveVertical * Force);
            MyRigidbody.AddRelativeForce(ForceFronte, MyForceMode);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            MyRigidbody.drag = 0f;
            Vector3 ForceBack = new Vector3(0.0f, 0.0f, MoveVertical * Force);
            MyRigidbody.AddRelativeForce(ForceBack, MyForceMode);

        }

        else if (Input.GetKey(KeyCode.A))
        {
            MyRigidbody.drag = 0f;
            Vector3 ForceRight = new Vector3(MoveHorizontal * Force, 0.0f, 0.0f);
            MyRigidbody.AddRelativeForce(ForceRight,MyForceMode);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            MyRigidbody.drag = 0f;
            Vector3 ForceLeft = new Vector3(MoveHorizontal * Force, 0.0f, 0.0f);
            MyRigidbody.AddRelativeForce(ForceLeft,MyForceMode);

        }
        else
        {
            MyRigidbody.drag = 500.0f;
        }

    }
}
