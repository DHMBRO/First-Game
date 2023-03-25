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
    bool dontInJamp = true;
    int jumpcount;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plane"))
        {
            dontInJamp = true;
        }
    }

    void Update()
    {
        float MoveVertical = Input.GetAxis("Vertical");
        float MoveHorizontal = Input.GetAxis("Horizontal");

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
            MyRigidbody.AddRelativeForce(ForceRight, MyForceMode);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            MyRigidbody.drag = 0f;
            Vector3 ForceLeft = new Vector3(MoveHorizontal * Force, 0.0f, 0.0f);
            MyRigidbody.AddRelativeForce(ForceLeft, MyForceMode);

        }
        else
        {
            if (dontInJamp)
            {
                MyRigidbody.drag = 500.0f;
            }
            else
            {
                MyRigidbody.drag = 0f;
            }
        }
        if (Input.GetKey(KeyCode.Space) && dontInJamp)
        {
            MyRigidbody.drag = 0f;
            jumpcount = 100;
            dontInJamp = false;
        }
        if (jumpcount > 0.0f)
        {
            Vector3 addForce = new Vector3(0.00f, 2f * Force, 0f);
            MyRigidbody.AddRelativeForce(addForce, MyForceMode);
            jumpcount--;
        }
        Debug.Log(dontInJamp);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("No1");
        if (other.gameObject.CompareTag("Plane"))
        {
            Debug.Log("No");
            dontInJamp = false;
        }
    }
}