using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] UiControler ControlerUi;
    [SerializeField] PlayerControler ControlerPlayer;

    [SerializeField] private GameObject CurrentTargetCamera;
    [SerializeField] public GameObject TargetCamera;
    [SerializeField] private GameObject TargetCameraForAnimations; 
    
    [SerializeField] private RaycastHit HitResult;

    [SerializeField] Transform HandTarget;
    [SerializeField] Transform Cube;
    [SerializeField] Transform Cube1;

    //New references 
    [SerializeField] public Vector3 CurrentOffSetCamera;
    [SerializeField] public Vector3 DesirableVector;

    [SerializeField] public Vector3 OffsetCameraSimple;
    [SerializeField] Vector3 OffsetCameraToAiming;
    //[SerializeField] Vector3 OffsetCameraToAimingInStelth;
    //[SerializeField] Vector3 OffsetCameraInStelth;

    [SerializeField] public float CurrentMoveBackDistance;
    [SerializeField] float MoveBackDistanceSimple = 5.0f;
    [SerializeField] float MoveBackDistanceStelth = 3.0f;
    [SerializeField] float MoveBackDistanceAiming = 2.02f;

    [SerializeField] float MouseSens = 1.0f;
    [SerializeField] float MaxMagnitude = 2.4f;
    [SerializeField] float MinMagnitude = 0.1f;
    [SerializeField] public float HeightStartPoint = 1.0f;

    [SerializeField] float t;
    [SerializeField] float CurrentLenghtOfOneStep;
    [SerializeField] float LenghtToOneStepSimple = 1000.0f;
    [SerializeField] float LenghtOfOneStepIsAiming = 30.0f;

    [SerializeField] MoodCamera CameraMood;
    [SerializeField] public bool CameraIsUsig = true;

    float MouseY;
    float MouseX;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CameraIsUsig = true;

        DesirableVector = OffsetCameraSimple;
        CurrentMoveBackDistance = MoveBackDistanceSimple;

        CurrentLenghtOfOneStep = LenghtOfOneStepIsAiming;

    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CameraIsUsig = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CameraIsUsig = true;
        }

        if ((ControlerUi && ControlerUi.InventoryIsOpen) || !CameraIsUsig) return;
        
        //Prameters
        MouseY = Input.GetAxis("Mouse Y");
        MouseX = Input.GetAxis("Mouse X");
        HeightStartPoint = (0.9935f + OffsetCameraSimple.y);

        CurrentOffSetCamera = transform.position;
        
        float EulerX = transform.eulerAngles.x + (-MouseY * MouseSens);
        
        if (EulerX >= 180.0f)
        {
            EulerX -= 360.0f;
        }

        EulerX = Mathf.Clamp(EulerX, -80.0f, 70.0f);

        transform.eulerAngles = new Vector3(
           EulerX,
           transform.eulerAngles.y + (MouseX * MouseSens),
            0.0f);

        //Rotate Player When Camera Aiming
        if (ControlerPlayer.WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething)
        {
            TargetCamera.transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
            CurrentLenghtOfOneStep = LenghtOfOneStepIsAiming;

        }
        else CurrentLenghtOfOneStep = LenghtToOneStepSimple;


        if (ControlerPlayer.StealthKilling)
        {
            TargetCameraForAnimations.transform.position = new Vector3(TargetCameraForAnimations.transform.position.x, TargetCamera.transform.position.y, TargetCameraForAnimations.transform.position.z);
            transform.position = TargetCameraForAnimations.transform.TransformPoint(DesirableVector) + -(transform.forward * CurrentMoveBackDistance);
            
        }
        else transform.position = TargetCamera.transform.TransformPoint(DesirableVector) + -(transform.forward * CurrentMoveBackDistance);


        //Audit To Walls
        if (Physics.Raycast(TargetCamera.transform.TransformPoint(DesirableVector) - (transform.forward * (DesirableVector.z)) /*- (transform.forward * 1.0f)*/, -transform.forward, out RaycastHit LocalHitResult, CurrentMoveBackDistance))
        {
            //DesirableVector = HitInfo.point;
            //DesirableVector -= transform.position;
            //HitResult = LocalHitResult;
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
                CurrentMoveBackDistance = MoveBackDistanceSimple;
            }

        }


        //Draw Ray Backward
        Debug.DrawRay(TargetCamera.transform.TransformPoint(DesirableVector) , -(transform.forward * CurrentMoveBackDistance), Color.blue);


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


        //LerpCamera(DesirableVector);


    }


    void LerpCamera(Vector3 OffSet)
    {
        Vector3 TargetVector = TargetCamera.transform.TransformPoint(OffSet) + -(transform.forward * CurrentMoveBackDistance);

        t = ((CurrentLenghtOfOneStep / (TargetVector - transform.position).magnitude) * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, TargetVector, t);
    }


}