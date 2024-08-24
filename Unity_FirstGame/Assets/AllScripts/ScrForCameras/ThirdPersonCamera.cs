using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] UiControler ControlerUi;
    [SerializeField] PlayerControler ControlerPlayer;

    [SerializeField] private Transform CurrentTargetCamera;
    [SerializeField] public Transform TargetCamera;
    [SerializeField] private Transform TargetCameraForAnimations; 
    
    [SerializeField] private RaycastHit HitResult;

    [SerializeField] Transform Cube;
    [SerializeField] Transform Cube1;

    //New references 
    [SerializeField] public Vector3 CameraChangeAngle;
    [SerializeField] public Vector3 CurrentOffSetCamera;
    [SerializeField] public Vector3 DesirableVector;

    [SerializeField] public Vector3 OffsetCameraSimple;
    [SerializeField] Vector3 OffsetCameraToAiming;
    
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
    
    float MouseY;
    float MouseX;

    private void Start()
    {
        DesirableVector = OffsetCameraSimple;
        CurrentMoveBackDistance = MoveBackDistanceDefault;
        CurrentMouseSens = DefaultMouseSens;
        //CurrentLenghtOfOneStep = LenghtOfOneStepIsAiming;

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

        transform.position += transform.right * CurrentMoveRightDistance;
        transform.position -= transform.forward * CurrentMoveBackDistance;

        //Check To Walls
        if (Physics.Raycast(transform.position + (transform.forward * CurrentMoveBackDistance), -transform.forward, out RaycastHit LocalHitResult, CurrentMoveBackDistance))
        {
            if (!LocalHitResult.collider.isTrigger) transform.position = LocalHitResult.point;
        }
        else
        {
            if (ControlerPlayer.WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething)
            {
                DesirableVector = OffsetCameraToAiming;
                CurrentMoveBackDistance = MoveBackDistanceAiming;
            }
            else
            {
                DesirableVector = OffsetCameraSimple;
                CurrentMoveBackDistance = MoveBackDistanceDefault;
            }

        }


        //Draw Ray Backward
        Debug.DrawRay(transform.position + (transform.forward * CurrentMoveBackDistance), -(transform.forward * CurrentMoveBackDistance), Color.blue);


        if (ControlerPlayer.WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething)
        {
            Vector3 TransfromOffSetAiming = TargetCamera.transform.TransformPoint(OffsetCameraToAiming) + -(transform.forward * DesirableVector.z);
            if ((TransfromOffSetAiming - CurrentOffSetCamera).magnitude > MaxMagnitude)
            {
                //Debug.Log("CurrentMagnitude > MaxMagnitude");
                //Debug.Log((TransfromOffSetAiming - CurrentOffSetCamera).magnitude);
                //transform.position = TargetCamera.transform.TransformPoint(OffsetCameraSimple) + -(transform.forward * DesirableVector.z);
            }
            if ((TransfromOffSetAiming - CurrentOffSetCamera).magnitude <= MinMagnitude)
            {
                Debug.Log("True");
            }

        }


        

    }

}