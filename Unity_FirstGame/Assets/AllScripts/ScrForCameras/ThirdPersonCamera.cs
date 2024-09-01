using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] UiControler ControlerUi;
    [SerializeField] PlayerControler ControlerPlayer;

    [SerializeField] private Transform CurrentTargetCamera;
    [SerializeField] public Transform TargetCamera;
    [SerializeField] private Transform TargetCameraForAnimations; 
    
    [SerializeField] private RaycastHit HitResult;

    //New references 
    [SerializeField] public Vector3 CameraChangeAngle;
    [SerializeField] public Vector3 CurrentOffSetCamera;
    [SerializeField] public Vector3 DesirableVector;

    [SerializeField] public Vector3 OffsetCameraSimple;
    [SerializeField] Vector3 OffsetCameraToAiming;
    [SerializeField] Vector3 OffSetCameraToAimingAndCrouch;

    [SerializeField] public float CurrentMoveRightDistance;
    [SerializeField] public float CurrentMoveBackDistance;

    [SerializeField] float MoveBackDistanceDefault = 5.0f;
    [SerializeField] float MoveBackDistanceAiming = 2.02f;

    [SerializeField] float MoveRightDistanceDefault = 1.0f;
    [SerializeField] float MoveRightDistanceAiming = 0.5f;

    public float DefaultMouseSens = 1.0f;
    public float  CurrentMouseSens = 1.0f;
    
    [SerializeField] float MaxMagnitude = 2.4f;
    [SerializeField] float MinMagnitude = 0.1f;
    [SerializeField] public float HeightStartPoint = 1.0f;

    [SerializeField] MoodCamera CameraMood;

    Ray RayToRight;
    Ray RayToBack;

    RaycastHit[] MassOfHitPointsToRight;
    RaycastHit[] MassOfHitPointsToBack;

    float MouseY;
    float MouseX;



    private void Start()
    {
        DesirableVector = OffsetCameraSimple;
        CurrentMoveBackDistance = MoveBackDistanceDefault;
        CurrentMouseSens = DefaultMouseSens;
        //CurrentLenghtOfOneStep = LenghtOfOneStepIsAiming;

        if (transform.parent)
        {
            transform.SetParent(null);
        }

    }

    public void CameraUpdate()
    {
     
        //Prameters
        MouseY = Input.GetAxis("Mouse Y");
        MouseX = Input.GetAxis("Mouse X");
        HeightStartPoint = (0.9935f + OffsetCameraSimple.y);

        CurrentOffSetCamera = transform.position;

        CurrentMoveBackDistance = MoveBackDistanceDefault;
        CurrentMoveRightDistance = MoveRightDistanceDefault;

        float EulerX = transform.eulerAngles.x + (-MouseY * CurrentMouseSens);

        if (EulerX >= 180.0f)
        {
            EulerX -= 360.0f;
        }

        //Default Rotate
        EulerX = Mathf.Clamp(EulerX, -80.0f, 70.0f);

        transform.localEulerAngles = new Vector3(
           EulerX,
           transform.eulerAngles.y + (MouseX * CurrentMouseSens),
            0.0f);

        EulerX = transform.eulerAngles.x + (-MouseY * CurrentMouseSens);
        if (EulerX <= -80.0f || EulerX >= 70.0f)
        {
            EulerX = 0.0f;
        }
        else
        {
            EulerX = (-MouseY * CurrentMouseSens);
        }

        CameraChangeAngle = new Vector3(EulerX, (MouseX * CurrentMouseSens), 0.0f);

        //Change states
        if (ControlerPlayer.WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething)
        {
            if (ControlerPlayer.WhatPlayerLegsDo == LegsPlayer.SatDown)
            {
                DesirableVector = OffSetCameraToAimingAndCrouch; 
            }
            else
            {
                DesirableVector = OffsetCameraToAiming;
            }
            CurrentMoveBackDistance = MoveBackDistanceAiming;
        }
        else
        {
            DesirableVector = OffsetCameraSimple;
            CurrentMoveBackDistance = MoveBackDistanceDefault;
        }

        //Aiming Rotate
        if (ControlerPlayer.StateCamera == CameraPlayer.Aiming)
        {
            TargetCamera.transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);

            CurrentMoveBackDistance = MoveBackDistanceAiming;
            CurrentMoveRightDistance = MoveRightDistanceAiming;
        }
        else
        {
            CurrentMoveBackDistance = MoveBackDistanceDefault;
            CurrentMoveRightDistance = MoveRightDistanceDefault;
        }

        //Set Position 
        transform.position = TargetCamera.TransformPoint(DesirableVector);

        //Set right position

        RayToRight = new Ray(transform.position + transform.right * 0.5f, transform.right * CurrentMoveRightDistance);        
        MassOfHitPointsToRight = Physics.RaycastAll(RayToRight, CurrentMoveRightDistance);

        Debug.DrawRay(transform.position + transform.right * 0.5f, (transform.right * CurrentMoveRightDistance), Color.blue);

        for (int i = 0;i < MassOfHitPointsToRight.Length; i++)
        {
            if (MassOfHitPointsToRight[i].collider != null && !MassOfHitPointsToRight[i].collider.isTrigger)
            {
                transform.position = MassOfHitPointsToRight[i].point;
                transform.position += -transform.right * 0.3f;
                break;
            }
            else
            {
                transform.position += transform.right * CurrentMoveRightDistance;
            }
        }
        if(MassOfHitPointsToRight.Length == 0)
        {
            transform.position += transform.right * CurrentMoveRightDistance;
        }

        //Set back position         

        RayToBack = new Ray(transform.position, transform.forward + -(transform.forward * CurrentMoveBackDistance));
        MassOfHitPointsToBack = Physics.RaycastAll(RayToBack, CurrentMoveBackDistance);


        Debug.DrawRay(transform.position, transform.forward + -(transform.forward * CurrentMoveBackDistance * 1.25f), Color.blue);
        
        for (int i = 0; i < MassOfHitPointsToBack.Length ; i++)
        {
            if (MassOfHitPointsToBack[i].collider != null && !MassOfHitPointsToBack[i].collider.isTrigger)
            {
                transform.position = MassOfHitPointsToBack[i].point;
                transform.position += transform.forward * 0.3f; 
                break;
            }
            else
            {
                transform.position -= transform.forward * CurrentMoveBackDistance;
            }
        }
        if (MassOfHitPointsToBack.Length == 0)
        {
            transform.position -= transform.forward * CurrentMoveBackDistance;
        }


        if (ControlerPlayer.WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething)
        {
            Vector3 TransfromOffSetAiming = TargetCamera.transform.TransformPoint(OffsetCameraToAiming) + -(transform.forward * DesirableVector.z);
            if ((TransfromOffSetAiming - CurrentOffSetCamera).magnitude > MaxMagnitude)
            {
                //Debug.Log("CurrentMagnitude > MaxMagnitude");
                //Debug.Log((TransfromOffSetAiming - CurrentOffSetCamera).magnitude);
                //transform.position = TargetCamera.transform.TransformPoint(OffsetCameraSimple) + -(transform.forward * DesirableVector.z);
            }
            
        }


        

    }

}