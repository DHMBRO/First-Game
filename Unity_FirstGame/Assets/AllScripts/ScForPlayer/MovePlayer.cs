using System;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float JumpForce = 1.0f;
    [SerializeField] protected float Sens = 1.0f;

    [SerializeField] ForceMode MyForceMode;

    [SerializeField] Rigidbody MyRigidbody;
    [SerializeField] Camera CameraScr;

    [SerializeField] protected Transform CameraTransform;

    //bool DontJumping = true;
    //bool chengebutton = false;

    int JumpCount;
    [SerializeField] public float Speed;
    [SerializeField] public float SpeedRun;

    float MoveHorizontal;
    float MoveVertical;

    float yVelocity = 0.0f;
    [SerializeField] float smooth = 0.1f;
    

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        float maxSpeed = Speed;
    }

    private void Update()
    {
        Move();
        
    }


    public void Move()
    {
        MoveVertical = Input.GetAxisRaw("Vertical");
        MoveHorizontal = Input.GetAxisRaw("Horizontal");

        //transform.rotation = Quaternion.Euler(0f, CameraTransform.rotation.y, 0f);
        //Vector3 ForceBack = transform.forward * MoveVertical * Speed;

        //Vector3 ForceBack = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;
        if (Math.Abs(MoveVertical + MoveVertical) > 0.01f)
        {
            Vector3 ForceForward = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;
            Quaternion Forward = Quaternion.LookRotation(ForceForward);

            Quaternion TargetRotation = Forward * CameraScr.transform.rotation;
            TargetRotation.x = 0;
            TargetRotation.z = 0;

            float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation.eulerAngles.y, ref yVelocity, smooth);


            transform.rotation = Quaternion.Euler(0, yAngle, 0);

        }



        //transform.rotation = TargetRotation;



        //MyRigidbody.velocity = new Vector3(0, MyRigidbody.velocity.y, 0);


        //MyRigidbody.AddRelativeForce(ForrceForward * Speed, MyForceMode);
        //if(Input.GetKey(KeyCode.LeftShift)) MyRigidbody.AddRelativeForce(ForrceForward * SpeedRun, MyForceMode);


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