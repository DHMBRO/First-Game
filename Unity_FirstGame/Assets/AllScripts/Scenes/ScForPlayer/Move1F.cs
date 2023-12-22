using UnityEngine;

public class Move1F : MonoBehaviour
{
    [SerializeField] private float SpeedForMove;
    [SerializeField] private float SpeedForRun;
    [SerializeField] private float PowerForJump;
    [SerializeField] private bool CanJump;    

    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Rigidbody RigidbodyForPlayer;

    int JumpCount = 0;
    [SerializeField] private float DistanceForRayJump;

    void Start()
    {
        RigidbodyForPlayer = gameObject.GetComponent<Rigidbody>();
        PlayerTransform = gameObject.GetComponent<Transform>();
    }
        


    public void Move()
    {
        if (PlayerTransform)
        {
            MoveForwardAndBack(PlayerTransform);
            MoveLeftAndForward(PlayerTransform);
            MoveRightAndForward(PlayerTransform);
            if(RigidbodyForPlayer) Jump();
        }

    }

    void MoveForwardAndBack(Transform PlayerTransform)
    {
        if (Input.GetKey(KeyCode.W))
        {
            PlayerTransform.localPosition += transform.forward * SpeedForMove;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                PlayerTransform.localPosition += transform.forward * SpeedForRun;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            PlayerTransform.localPosition += -transform.forward * SpeedForMove;
        }
    }
    
    void MoveLeftAndForward(Transform PlayerTransform)
    {
        if (Input.GetKey(KeyCode.A))
        {
            PlayerTransform.localPosition += -transform.right * SpeedForMove;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            PlayerTransform.localPosition += transform.forward * SpeedForMove;
            if (Input.GetKey(KeyCode.A))
            {
                PlayerTransform.localPosition += -transform.right * SpeedForMove;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            PlayerTransform.localPosition += -transform.right* SpeedForMove;
            if (Input.GetKey(KeyCode.W))
            {
                PlayerTransform.localPosition += transform.forward * SpeedForMove;
            }
        }

    }

    void MoveRightAndForward(Transform PlayerTransform)
    {
        if (Input.GetKey(KeyCode.D))
        {
            PlayerTransform.localPosition += transform.right * SpeedForMove;
        }

        if (Input.GetKey(KeyCode.W))
        {
            PlayerTransform.localPosition += transform.forward * SpeedForMove;
            if (Input.GetKey(KeyCode.D))
            {
                PlayerTransform.localPosition += transform.right * SpeedForMove;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            PlayerTransform.localPosition += transform.right * SpeedForMove;
            if (Input.GetKey(KeyCode.W))
            {
                PlayerTransform.localPosition += transform.forward * SpeedForMove;
            }
        }
    }
    
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit Result = new RaycastHit();
            Ray RayForJump = new Ray(transform.position, -transform.up);
            Debug.DrawRay(transform.position, -transform.up * DistanceForRayJump, Color.blue);
            if (Physics.Raycast(RayForJump, out RaycastHit HitResult, DistanceForRayJump))
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
            RigidbodyForPlayer.AddRelativeForce(new Vector3(0f, 1f * PowerForJump, 0f), ForceMode.Force);
            JumpCount--;
        }
    }

}
