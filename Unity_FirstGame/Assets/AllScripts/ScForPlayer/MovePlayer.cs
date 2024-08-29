using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] public PlayerControler ControlerPlayer;
    [SerializeField] Rigidbody MyRigidbody;
    [SerializeField] CapsuleCollider ColiderCapsulePlayer;
    [SerializeField] Camera CameraScr;
    
    [SerializeField] List<GameObject> AllCollisionBonesPlayer = new List<GameObject>();
    [SerializeField] ForceMode MyForceMode;

    [SerializeField] public float CurrentSpeed = 0.0f;
    [SerializeField] private float SpeedWalk = 9000.0f;
    [SerializeField] private float SpeedStelth = 4500.0f;
    [SerializeField] private float SpeedRun = 28000.0f;

    [SerializeField] private float HeightWenStand = 3.0f;
    [SerializeField] private float HeightWneSatDown = 1.5f;

    [SerializeField] int JumpCount;
    [SerializeField] bool CanJump = false;

    [SerializeField] float JumpForce = 1.0f;
    
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
                    return Result;
                }
            }
        }
        else Result = true;

        return Result; 
    }

    public void RotateBodyPlayer()
    {
        MoveVertical = Input.GetAxisRaw("Vertical");
        MoveHorizontal = Input.GetAxisRaw("Horizontal");

        Vector3 ForceAxis = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;

        if (ControlerPlayer.WhatPlayerHandsDo != HandsPlayer.AimingForDoSomething && ControlerPlayer.WhatSpeedPlayerLegs != SpeedLegsPlayer.Null) //Rotate Body Player
        {
            Quaternion Forward = Quaternion.LookRotation(ForceAxis);
            Quaternion TargetRotation = Forward * CameraScr.transform.rotation;

            TargetRotation.x = 0;
            TargetRotation.z = 0;

            float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation.eulerAngles.y, ref yVelocity, smooth);
            transform.rotation = Quaternion.Euler(0, yAngle, 0);

        }

        CurrentSpeed = GetSpeedOfPlayer();

        if (CurrentSpeed != 0.0f)
        {
            if (ControlerPlayer.WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething)
            {
                MyRigidbody.AddRelativeForce(ForceAxis * CurrentSpeed * Time.deltaTime, MyForceMode);
            }
            else
            {
                MyRigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, 1.0f * CurrentSpeed * Time.deltaTime), MyForceMode);
            }
        }

        MyRigidbody.velocity = new Vector3(0, MyRigidbody.velocity.y, 0);
        
    }

    private float GetSpeedOfPlayer()
    {
        switch (ControlerPlayer.WhatSpeedPlayerLegs)
        {
            case SpeedLegsPlayer.CrouchWalk:
                return SpeedStelth;                
            case SpeedLegsPlayer.Walk:
                return SpeedWalk;                
            case SpeedLegsPlayer.Run:
                return SpeedRun;                
            default:
                return 0.0f;                
        }
    }

    public float GetSpeedPlayerForAnimations()
    {
        switch (ControlerPlayer.WhatSpeedPlayerLegs)
        {
            case SpeedLegsPlayer.CrouchWalk:
                return 0.5f;
            case SpeedLegsPlayer.Walk:
                return 0.5f;
            case SpeedLegsPlayer.Run:
                return 1.0f;
            default:
                return 0.0f;
        }
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