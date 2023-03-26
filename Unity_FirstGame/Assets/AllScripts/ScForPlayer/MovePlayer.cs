using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float Force = 1.0f;
    [SerializeField] float JumpForce = 1.0f;
    [SerializeField] ForceMode MyForceMode;
    [SerializeField] Rigidbody MyRigidbody;
    [SerializeField] protected Transform CameraTransform;
    [SerializeField] protected float Sens = 1.0f;
    bool dontInJamp = true;
    int jumpcount;
    bool chengebutton = false;
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            if (!dontInJamp)
            {
                dontInJamp = true;
                Debug.Log("1" + dontInJamp);
            }
        }
    }
    void Update()
    {
        float MoveVertical = Input.GetAxis("Vertical");
        float MoveHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (dontInJamp)
            {
                MyRigidbody.drag = 500000.0f;
            }
            else
            {
                MyRigidbody.drag = 0f;
            }
            chengebutton = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (dontInJamp)
            {
                MyRigidbody.drag = 500000.0f;
            }
            else
            {
                MyRigidbody.drag = 0f;
            }
            chengebutton = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if (dontInJamp)
            {
                MyRigidbody.drag = 500000.0f;
            }
            else
            {
                MyRigidbody.drag = 1f;
            }
            chengebutton = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            if (dontInJamp)
            {
                MyRigidbody.drag = 500000.0f;
            }
            else
            {
                MyRigidbody.drag = 1f;
            }
            chengebutton = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (!chengebutton)
            {
                MyRigidbody.drag = 1f;
                Vector3 ForceFronte = new Vector3(0.0f, 0.0f, MoveVertical * Force);
                MyRigidbody.AddRelativeForce(ForceFronte, MyForceMode);
            }
            chengebutton = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (!chengebutton)
            {
                MyRigidbody.drag = 1f;
                Vector3 ForceBack = new Vector3(0.0f, 0.0f, MoveVertical * Force);
                MyRigidbody.AddRelativeForce(ForceBack, MyForceMode);
            }
            chengebutton = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!chengebutton)
            {
                MyRigidbody.drag = 1f;

                Vector3 ForceRight = new Vector3(MoveHorizontal * Force, 0.0f, 0.0f);
                MyRigidbody.AddRelativeForce(ForceRight, MyForceMode);
            }
            chengebutton = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!chengebutton)
            {
                MyRigidbody.drag = 1f;
                Vector3 ForceLeft = new Vector3(MoveHorizontal * Force, 0.0f, 0.0f);
                MyRigidbody.AddRelativeForce(ForceLeft, MyForceMode);
            }
            chengebutton = false;
        }
        else
        {
            if (dontInJamp)
            {
                MyRigidbody.drag = 500.0f;
            }
            else
            {
                MyRigidbody.drag = 1f;
            }
            chengebutton = false;
        }
        if (Input.GetKey(KeyCode.Space) && dontInJamp)
        {
            MyRigidbody.drag = 1f;
            jumpcount = 100;
            dontInJamp = false;
        }
        if (jumpcount > 0.0f)
        {
            Vector3 addForce = new Vector3(0.00f, 2f * JumpForce, 0f);
            MyRigidbody.AddRelativeForce(addForce, MyForceMode);
            jumpcount--;
        }
    }
}