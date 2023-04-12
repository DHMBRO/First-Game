using UnityEngine;

public class Move1F : MonoBehaviour
{
    [SerializeField] private float SpeedForMove;
    [SerializeField] private float PowerForJump;
    [SerializeField] private bool CanJump;

    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Rigidbody RigidbodyForPlayer;

    int JumpCount = 0;

    void Start()
    {
        RigidbodyForPlayer = gameObject.GetComponent<Rigidbody>();
        PlayerTransform = gameObject.GetComponent<Transform>();
    }
        


    public void Move()
    {
        if (PlayerTransform)
        {
            if (Input.GetKey(KeyCode.W))
            {
                PlayerTransform.transform.localPosition += transform.forward * SpeedForMove;
            }            
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                PlayerTransform.transform.localPosition += transform.forward * SpeedForMove;
                PlayerTransform.transform.localPosition += transform.right * SpeedForMove;
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                PlayerTransform.transform.localPosition += transform.forward * SpeedForMove;
                PlayerTransform.transform.localPosition += -transform.right * SpeedForMove;
            }                                    
            else if (Input.GetKey(KeyCode.S))            
            {
                PlayerTransform.transform.localPosition -= transform.forward * SpeedForMove;
            }                                                            
            else if (Input.GetKey(KeyCode.D))
            {
                PlayerTransform.transform.localPosition += transform.right * SpeedForMove;
            }                                                
            else if (Input.GetKey(KeyCode.A))
            {
                PlayerTransform.transform.localPosition += -transform.right * SpeedForMove;
            }
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
            if(JumpCount > 0)
            {
                RigidbodyForPlayer.AddRelativeForce(new Vector3(0f, 1f * PowerForJump,0f), ForceMode.Force);
                JumpCount--;
            }
        }

    }
}
