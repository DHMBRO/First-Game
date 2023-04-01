using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move1F : MonoBehaviour
{
    [SerializeField] private float SpeedForMove;
    [SerializeField] private float PowerForJump;
    [SerializeField] private bool CanJump;

    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Rigidbody RigidbodyForPlayer;

    [SerializeField] private float TimeForjump;
    [SerializeField] private float JumpDeley = 2.0f;


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
            else if (Input.GetKey(KeyCode.Space) && RigidbodyForPlayer && TimeForjump >= Time.time)
            {
                Vector3 Jumping = new Vector3(0.0f, 1.0f * PowerForJump, 0.0f);
                RigidbodyForPlayer.AddRelativeForce(Jumping, ForceMode.Force);
                TimeForjump = Time.time + JumpDeley;
            }   
        }                
    }
}
