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

    float MoveHorizontal;
    float MoveVertical;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        float maxSpeed = Speed;
    }

    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        MyRigidbody.velocity = new Vector3(0, 0, 0);
        //transform.rotation = Quaternion.Euler(0f, CameraTransform.rotation.y, 0f);
        MoveVertical = Input.GetAxisRaw("Vertical") * 100000;
        MoveHorizontal = Input.GetAxisRaw("Horizontal") * 100000;
        //Vector3 ForceBack = transform.forward * MoveVertical * Speed;
        Vector3 ForceBack = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;

        MyRigidbody.AddRelativeForce(ForceBack * Speed, MyForceMode);
        if (MyRigidbody.velocity.magnitude != 0f)
        {
            MyRigidbody.velocity = MyRigidbody.velocity.normalized * Speed;
        }
    }
    void Jump()
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