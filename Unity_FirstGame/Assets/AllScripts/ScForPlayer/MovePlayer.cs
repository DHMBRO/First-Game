using System;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float JumpForce = 1.0f;
    [SerializeField] protected float Sens = 1.0f;

    [SerializeField] ForceMode MyForceMode;

    [SerializeField] Rigidbody MyRigidbody;

    [SerializeField] protected Transform CameraTransform;

    //bool DontJumping = true;
    //bool chengebutton = false;

    int JumpCount;
    [SerializeField] public float Speed;
    [SerializeField] public float SpeedRun;

    float MoveHorizontal;
    float MoveVertical;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        float maxSpeed = Speed;
    }

    
    public void Move()
    {
        MoveVertical = Input.GetAxisRaw("Vertical") * 100000;
        MoveHorizontal = Input.GetAxisRaw("Horizontal") * 100000;
        
        //transform.rotation = Quaternion.Euler(0f, CameraTransform.rotation.y, 0f);
        //Vector3 ForceBack = transform.forward * MoveVertical * Speed;
        
        Vector3 ForceBack = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;
        Vector3 ForrceForward = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;
        
        MyRigidbody.velocity = new Vector3(0, MyRigidbody.velocity.y, 0);


        MyRigidbody.AddRelativeForce(ForceBack * Speed, MyForceMode);
        if(Input.GetKey(KeyCode.LeftShift)) MyRigidbody.AddRelativeForce(ForrceForward * SpeedRun, MyForceMode);


        /*
        if (MyRigidbody.velocity.magnitude - MyRigidbody.velocity.y != 0f)
        {
            MyRigidbody.velocity = MyRigidbody.velocity.normalized * Speed;
        }
        */
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit Result = new RaycastHit();
            Ray RayForJump = new Ray(transform.position, -transform.up);
            Debug.DrawRay(transform.position, -transform.up * 1.01f, Color.blue);
            if (Physics.Raycast(RayForJump, out RaycastHit HitResult, 1.01f))
            {
                Result = HitResult;
                if (Result.collider)
                {
                    if (Result.collider.tag != "")
                    {
                        JumpCount = 7;
                    }
                }
            }

        }
        if (JumpCount > 0)
        {
            MyRigidbody.AddRelativeForce(new Vector3(0f, 1f * JumpForce, 0f), ForceMode.Force);
            JumpCount--;
        }
    } 
}