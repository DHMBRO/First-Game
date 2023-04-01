using UnityEngine;

public class Move1F : MonoBehaviour
{
    [SerializeField] private float SpeedForMove;
    [SerializeField] private float PowerForJump;
    [SerializeField] private bool CanJump;

    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Rigidbody RigidbodyForPlayer;

    

    void Start()
    {
        RigidbodyForPlayer = gameObject.GetComponent<Rigidbody>();
        PlayerTransform = gameObject.GetComponent<Transform>();
    }
        

    void Update()
    {
        if (PlayerTransform)
        {
            if (Input.GetKey(KeyCode.W))
            {
                PlayerTransform.transform.localPosition += transform.forward * SpeedForMove;
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
            else if (Input.GetKey(KeyCode.Space))
            {

            }
        }                
    }
}
