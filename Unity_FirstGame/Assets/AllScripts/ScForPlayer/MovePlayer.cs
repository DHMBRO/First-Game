using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] public PlayerControler ControlerPlayer;
    [SerializeField] Rigidbody MyRigidbody;
    [SerializeField] CapsuleCollider ColiderCapsulePlayer;
    [SerializeField] Camera CameraScr;
    
    [SerializeField] Transform CameraTransform;

    [SerializeField] List<GameObject> AllCollisionBonesPlayer = new List<GameObject>();
    [SerializeField] ForceMode MyForceMode;

    [SerializeField] public float CurrentSpeed = 0.0f;
    [SerializeField] private float SpeedWalk = 9000.0f;
    [SerializeField] private float SpeedStelth = 8000.0f;
    [SerializeField] private float SpeedRun = 28000.0f;

    [SerializeField] private float HeightWenStand = 3.0f;
    [SerializeField] private float HeightWneSatDown = 1.5f;

    [SerializeField] int JumpCount;
    [SerializeField] bool CanJump = false;

    [SerializeField] float JumpForce = 1.0f;
    //[SerializeField] float DeleyJump = 1.0f; 

    float MoveHorizontal;
    float MoveVertical;
    float yVelocity = 0.0f;
    
    [SerializeField] float smooth = 0.1f;
    

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
        ControlerPlayer = GetComponent<PlayerControler>();

        if (!CameraScr) Debug.Log("Not set CameraScr");
        
    }
    
    public void ControlCapsuleColider(bool SatDown)
    {
        if(!ColiderCapsulePlayer)
        {
            Debug.Log("Not set ColiderCapsulePlayer");
            return;
        }

        if (SatDown)
        {
            ColiderCapsulePlayer.center = new Vector3(0.0f, HeightWneSatDown / 2.0f, 0.0f);
            ColiderCapsulePlayer.height = HeightWneSatDown;
        }
        else
        {
            ColiderCapsulePlayer.center = new Vector3(0.0f, HeightWenStand / 2.0f, 0.0f);
            ColiderCapsulePlayer.height = HeightWenStand;
        }
    }

    public bool AuditToStandUp()
    {
        Vector3 StartPosForRay = transform.position + transform.up * HeightWneSatDown;
        bool Result = false;

        if (Physics.Raycast(StartPosForRay, transform.up, out RaycastHit HitResult, HeightWenStand))
        {
            if (HitResult.articulationBody != null)
            {
                Result = true;
            }
            else 
            {
                for (int i = 0;i < AllCollisionBonesPlayer.Count;i++)
                {
                    if(HitResult.collider.name == AllCollisionBonesPlayer[i].name)
                    {
                        Result = true;
                    }
                }

                if(Result == false)
                {
                    Debug.Log(HitResult.collider.name);
                    return Result;
                }
            }
        }
        else Result = true;

        return Result; 
    }

    public void RotateBodyPlayer(SpeedLegsPlayer WhatSpeedPlayerLegs)
    {
        MoveVertical = Input.GetAxisRaw("Vertical");
        MoveHorizontal = Input.GetAxisRaw("Horizontal");

        //transform.rotation = Quaternion.Euler(0f, CameraTransform.rotation.y, 0f);
        //Vector3 ForceBack = transform.forward * MoveVertical * Speed;
        //Vector3 ForceBack = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;

        Vector3 ForceAxis = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftShift))
        {
            if (ControlerPlayer.WhatPlayerHandsDo != HandsPlayer.AimingForDoSomething && ControlerPlayer.WhatSpeedPlayerLegs != SpeedLegsPlayer.Null) //Rotate Body Player
            {
                Quaternion Forward = Quaternion.LookRotation(ForceAxis);
                Quaternion TargetRotation = Forward * CameraScr.transform.rotation;

                TargetRotation.x = 0;
                TargetRotation.z = 0;

                float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation.eulerAngles.y, ref yVelocity, smooth);
                transform.rotation = Quaternion.Euler(0, yAngle, 0);
               
            }




            switch (WhatSpeedPlayerLegs)
            {
                case SpeedLegsPlayer.CrouchWalk:
                    Move(SpeedStelth);
                    break;
                case SpeedLegsPlayer.Walk:
                    Move(SpeedWalk);
                    break;
                case SpeedLegsPlayer.Run:
                    Move(SpeedRun);
                    break;
                default:
                    CurrentSpeed = 0.0f;
                    break;
            }

            /*

            case ModeMovement.Go:
                    Movement(SpeedGo, false);
                    break;
                case ModeMovement.Stelth:
                    Movement(SpeedStels, false);
                    break;
                case ModeMovement.Run:
                    Movement(SpeedRun, false);
                    break;
                case ModeMovement.StelsAndAiming:
                    Movement(SpeedAiming, true);
                    break;
                default:
                    CurrentSpeed = 0.0f;
                    break;

            */

            void Move(float CurrentSpeedToMove/*, bool Aiming*/)
            {
                if (ControlerPlayer.WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething)
                {
                    MyRigidbody.AddRelativeForce(ForceAxis * CurrentSpeed * Time.deltaTime, MyForceMode);
                }
                else
                {
                    MyRigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, 1.0f * CurrentSpeedToMove * Time.deltaTime), MyForceMode);
                }
                CurrentSpeed = CurrentSpeedToMove;

            }

        }

        MyRigidbody.velocity = new Vector3(0, MyRigidbody.velocity.y, 0);
        
    }


    
    public void Jump()
    {
        if (!CanJump) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyRigidbody.AddRelativeForce(new Vector3(0.0f, 1.0f * JumpForce, 0.0f), ForceMode.Force);
            ControlerPlayer.IsJuming = true;
            //Debug.Log(ControlerPlayer.Juming);
        }
        else ControlerPlayer.IsJuming = false;
    }


    private void OnCollisionStay(Collision collision)
    {
        CanJump = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        CanJump = false;
    }
}