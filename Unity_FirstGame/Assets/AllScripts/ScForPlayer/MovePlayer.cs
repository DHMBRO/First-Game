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
    int JumpCount;
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
                MyRigidbody.isKinematic = true;
            }
            else
            {
                MyRigidbody.isKinematic = false;
            }
            chengebutton = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (dontInJamp)
            {
                MyRigidbody.isKinematic = true;
            }
            else
            {
                MyRigidbody.isKinematic = false;
            }
            chengebutton = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if (dontInJamp)
            {
                MyRigidbody.isKinematic = true;
            }
            else
            {
                MyRigidbody.isKinematic = false;
            }
            chengebutton = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            if (dontInJamp)
            {
                MyRigidbody.isKinematic = true;
            }
            else
            {
                MyRigidbody.isKinematic = false;
            }
            chengebutton = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (!chengebutton)
            {
                MyRigidbody.isKinematic = false;
                Vector3 ForceFronte = new Vector3(0.0f, 0.0f, MoveVertical * Force);
                MyRigidbody.AddRelativeForce(ForceFronte, MyForceMode);
            }
            chengebutton = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (!chengebutton)
            {
                MyRigidbody.isKinematic = false;
                Vector3 ForceBack = new Vector3(0.0f, 0.0f, MoveVertical * Force);
                MyRigidbody.AddRelativeForce(ForceBack, MyForceMode);
            }
            chengebutton = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!chengebutton)
            {
                MyRigidbody.isKinematic = false;

                Vector3 ForceRight = new Vector3(MoveHorizontal * Force, 0.0f, 0.0f);
                MyRigidbody.AddRelativeForce(ForceRight, MyForceMode);
            }
            chengebutton = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!chengebutton)
            {
                MyRigidbody.isKinematic = false;
                Vector3 ForceLeft = new Vector3(MoveHorizontal * Force, 0.0f, 0.0f);
                MyRigidbody.AddRelativeForce(ForceLeft, MyForceMode);
            }
            chengebutton = false;
        }
        else
        {
            if (dontInJamp)
            {
                MyRigidbody.isKinematic = true;
            }
            else
            {
                MyRigidbody.isKinematic = false;
            }
            chengebutton = false;
        }
        if (Input.GetKey(KeyCode.Space) && dontInJamp)
        {
            MyRigidbody.isKinematic = false;
            JumpCount = 100;
            dontInJamp = false;
        }
        if (JumpCount > 0.0f)
        {
            Vector3 addForce = new Vector3(0.00f, 2f * JumpForce, 0f);
            MyRigidbody.AddRelativeForce(addForce, MyForceMode);
            JumpCount--;
        }        
    }        
}