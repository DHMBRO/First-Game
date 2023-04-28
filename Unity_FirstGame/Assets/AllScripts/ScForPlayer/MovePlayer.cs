using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] float JumpForce = 1.0f;
    [SerializeField] protected float Sens = 1.0f;

    [SerializeField] ForceMode MyForceMode;

    [SerializeField] Rigidbody MyRigidbody;

    [SerializeField] protected Transform CameraTransform;

    bool DontJumping = true;
    //bool chengebutton = false;

    int JumpCount;
    public int Speed;

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
            if (!DontJumping)
            {
                DontJumping = true;                
            }
        }
    }

    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        //transform.rotation = Quaternion.Euler(0f, CameraTransform.rotation.y, 0f);
        MoveVertical = Input.GetAxisRaw("Vertical");
        MoveHorizontal = Input.GetAxisRaw("Horizontal");
        //Vector3 ForceBack = transform.forward * MoveVertical * Speed;
        Vector3 ForceBack = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;
        Vector3 RBVel;

        MyRigidbody.AddRelativeForce(ForceBack * Speed, MyForceMode);
        float maxSpeed = Speed * ForceBack.magnitude;
        RBVel = MyRigidbody.velocity;
        Vector3 Dir =  RBVel.normalized;
        MyRigidbody.velocity = Dir* maxSpeed;
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && DontJumping)
        {
            MyRigidbody.isKinematic = false;
            JumpCount = 100;
            DontJumping = false;
        }
        if (JumpCount > 0.0f)
        {
            Vector3 addForce = new Vector3(0.00f, 2f * JumpForce, 0f);
            MyRigidbody.AddRelativeForce(addForce, MyForceMode);
            JumpCount--;
        }
    } 
}