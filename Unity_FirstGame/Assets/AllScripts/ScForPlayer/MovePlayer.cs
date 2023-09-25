using System;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] public PlayerControler ControlerPlayer;

    //[SerializeField] float JumpForce = 1.0f;
    [SerializeField] protected float Sens = 1.0f;

    [SerializeField] ForceMode MyForceMode;

    [SerializeField] Rigidbody MyRigidbody;
    [SerializeField] Camera CameraScr;

    [SerializeField] protected Transform CameraTransform;

    [SerializeField] int JumpCount;

    [SerializeField] private float SpeedAiming;
    [SerializeField] private float SpeedMove;    
    [SerializeField] private float SpeedRun;

    
    float MoveHorizontal;
    float MoveVertical;
    float yVelocity = 0.0f;
    
    [SerializeField] float smooth = 0.1f;
    

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        
    }

    

    public void Move()
    {
        MoveVertical = Input.GetAxisRaw("Vertical");
        MoveHorizontal = Input.GetAxisRaw("Horizontal");

        //transform.rotation = Quaternion.Euler(0f, CameraTransform.rotation.y, 0f);
        //Vector3 ForceBack = transform.forward * MoveVertical * Speed;

        Vector3 ForceAxis = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;
        Vector3 ForceForward = new Vector3(0.0f, 0.0f, SpeedMove);
        //Vector3 ForceBack = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Math.Abs(MoveVertical + MoveVertical) > 0.01f)
        {
            if (!ControlerPlayer.Aiming)
            {
                Quaternion Forward = Quaternion.LookRotation(ForceAxis);

                Quaternion TargetRotation = Forward * CameraScr.transform.rotation;

                TargetRotation.x = 0;
                TargetRotation.z = 0;

                float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation.eulerAngles.y, ref yVelocity, smooth);
                
                transform.rotation = Quaternion.Euler(0, yAngle, 0);

                //Vector3 MoveForward = new Vector3(0.0f, 0.0f, SpeedMove);

                MyRigidbody.AddRelativeForce(ForceForward, ForceMode.Force);
                if (Input.GetKey(KeyCode.LeftShift)) MyRigidbody.AddRelativeForce(ForceForward * SpeedRun, ForceMode.Force);

            }
            else
            {
                MyRigidbody.AddRelativeForce(ForceAxis * SpeedAiming, MyForceMode);
                if (Input.GetKey(KeyCode.LeftShift)) MyRigidbody.AddRelativeForce(ForceAxis * SpeedRun, MyForceMode);
                
            }
        }

        MyRigidbody.velocity = new Vector3(0, MyRigidbody.velocity.y, 0);
        


        //transform.rotation = TargetRotation;
        /*
        if (MyRigidbody.velocity.magnitude - MyRigidbody.velocity.y != 0f)
        {
            MyRigidbody.velocity = MyRigidbody.velocity.normalized * Speed;
        }
        */
    }


    /*
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
    */
}