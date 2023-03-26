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
    bool donJumping = true;
    int JumpCount;
    bool chengebutton = false;
    float MoveHorizontal;
    float MoveVertical;
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            if (!donJumping)
            {
                donJumping = true;
                Debug.Log("1" + donJumping);
            }
        }
    }
    void Update()
    {
        Addition();
        Move();
        Jump();
    }
    void Addition()
    {
        if (Input.GetKeyUp(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            if (donJumping)
            {
                MyRigidbody.isKinematic = true;
            }
            else
            {
                MyRigidbody.isKinematic = false;
            }
            chengebutton = true;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKeyUp(KeyCode.S))
        {
            if (donJumping)
            {
                MyRigidbody.isKinematic = true;
            }
            else
            {
                MyRigidbody.isKinematic = false;
            }
            chengebutton = true;
        }
        else if (Input.GetKeyUp(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            if (donJumping)
            {
                MyRigidbody.isKinematic = true;
            }
            else
            {
                MyRigidbody.isKinematic = false;
            }
            chengebutton = true;
        }
        else if (Input.GetKeyUp(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            if (donJumping)
            {
                MyRigidbody.isKinematic = true;
            }
            else
            {
                MyRigidbody.isKinematic = false;
            }
            chengebutton = true;
        }
    }
    void Move()
    {
        MoveVertical = Input.GetAxis("Vertical");
        MoveHorizontal = Input.GetAxis("Horizontal");
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
            if (donJumping)
            {
                MyRigidbody.isKinematic = true;
            }
            else
            {
                MyRigidbody.isKinematic = false;
            }
            chengebutton = false;
        }
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && donJumping)
        {
            MyRigidbody.isKinematic = false;
            JumpCount = 100;
            donJumping = false;
        }
        if (JumpCount > 0.0f)
        {
            Vector3 addForce = new Vector3(0.00f, 2f * JumpForce, 0f);
            MyRigidbody.AddRelativeForce(addForce, MyForceMode);
            JumpCount--;
        }        
    }        
}