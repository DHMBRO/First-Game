using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] UiControler ControlerUi;
    [SerializeField] PlayerControler ControlerPlayer;

    [SerializeField] public GameObject TargetCamera;
    [SerializeField] private RaycastHit HitResult;

    [SerializeField] Transform HandTarget;
    [SerializeField] Transform Cube;
    [SerializeField] Transform Cube1;

    [SerializeField] private Transform MoveBackObject;

    //New references 
    [SerializeField] public Vector3 CurrentOffSetCamera;
    [SerializeField] public Vector3 DesirableVector;

    [SerializeField] public Vector3 OffsetCameraSimple;
    [SerializeField] Vector3 OffsetCameraInStelth;

    [SerializeField] Vector3 OffsetCameraToAiming;
    [SerializeField] Vector3 OffsetCameraToAimingInStelth;

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
    [SerializeField] public bool CameraIsUsig = false;

    float MouseY;
    float MouseX;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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

        if (!CameraIsUsig) return;
        
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

        EulerX = Mathf.Clamp(EulerX, -60.0f, 70.0f);

        if (ControlerPlayer.IsAiming)
        {
            TargetCamera.transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
        }

        transform.eulerAngles = new Vector3(
           EulerX,
           transform.eulerAngles.y + (MouseX * MouseSens),
            0.0f);

        //Rotate Player When Camera Aiming
        if (ControlerPlayer.IsAiming)
        {
            TargetCamera.transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
        }

        if (ControlerPlayer.IsAiming)
        {
            CurrentLenghtOfOneStep = LenghtOfOneStepIsAiming;
        }
        else CurrentLenghtOfOneStep = LenghtToOneStepSimple;

        transform.position = TargetCamera.transform.TransformPoint(DesirableVector) + -(transform.forward * CurrentMoveBackDistance);

        

        //Audit To Walls
        if (Physics.Raycast(TargetCamera.transform.TransformPoint(DesirableVector) - (transform.forward * (DesirableVector.z)) /*- (transform.forward * 1.0f)*/, -transform.forward, out RaycastHit LocalHitResult, CurrentMoveBackDistance))
        {
            //DesirableVector = HitInfo.point;
            //DesirableVector -= transform.position;
            //HitResult = LocalHitResult;
            
            transform.position = LocalHitResult.point;
            
        }
        else
        {
            switch (ControlerPlayer.MovementMode)
            {
                case ModeMovement.Aiming:
                    DesirableVector = OffsetCameraToAiming;
                    CurrentMoveBackDistance = MoveBackDistanceAiming;
                    break;
                case ModeMovement.Stelth:
                    DesirableVector = OffsetCameraInStelth;
                    CurrentMoveBackDistance = MoveBackDistanceStelth;
                    break;
                case ModeMovement.StelsAndAiming:
                    DesirableVector = OffsetCameraToAimingInStelth;
                    CurrentMoveBackDistance = MoveBackDistanceAiming;
                    break;
                default:
                    DesirableVector = OffsetCameraSimple;
                    CurrentMoveBackDistance = MoveBackDistanceSimple;
                    break;
            }
        }
        

        //Draw Ray Backward
        Debug.DrawRay(TargetCamera.transform.TransformPoint(DesirableVector) , -(transform.forward * CurrentMoveBackDistance), Color.blue);


        if (ControlerPlayer.IsAiming)
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