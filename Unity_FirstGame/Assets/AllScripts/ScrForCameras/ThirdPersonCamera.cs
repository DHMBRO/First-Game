using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] UiControler ControlerUi;
    [SerializeField] PlayerControler ControlerPlayer;

    [SerializeField] public GameObject TargetCamera;

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

    [SerializeField] Vector3 OffsetHandForAimingTra;
    [SerializeField] Vector3 OffsetHandForAimingRot;

    [SerializeField] Vector3 OffsetHandPos;
    [SerializeField] Vector3 OffsetHandRot;

    [SerializeField] public float CurrentMoveBackDistance;
    [SerializeField] float MoveBackDistanceSimple = 5.0f;
    [SerializeField] float MoveBackDistanceStelth = 3.0f;
    [SerializeField] float MoveBackDistanceAiming = 2.02f;

    [SerializeField] float MouseSens = 1.0f;
    [SerializeField] float MaxMagnitude = 2.4f;
    [SerializeField] public float HeightStartPoint = 1.0f;

    [SerializeField] float t;
    [SerializeField] float CurrentLenghtOfOneStep;
    //[SerializeField] float LenghtToOneStep = 100.0f;
    [SerializeField] float LenghtOfOneStepToChangeState = 3.0f;

    [SerializeField] MoodCamera CameraMood;
    //[SerializeField] CameraIs CurrentState;

    //[SerializeField] public bool Aiming = false;
    [SerializeField] public bool CameraIsUsig = false;

    //Vector3 TargetPosition01;
    //Vector3 TargetPosition02;

    //bool CameraState01 = false;
    //bool CameraState02 = false;

    //bool FraimSimple = false;
    //bool FraimAiming = false;

    float MouseY;
    float MouseX;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        

        CurrentLenghtOfOneStep = LenghtOfOneStepToChangeState;

        //Debug.Log("Distance: " + (transform.position - MoveBackObject.position).magnitude);
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
        //Debug.Log("CurrentOffSetCamera: " + CurrentOffSetCamera);

        //Camera Rotate

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

        transform.position = TargetCamera.transform.TransformPoint(DesirableVector) + -(transform.forward * CurrentMoveBackDistance);
        
        //Rotate Player When Camera Aiming
        if (ControlerPlayer.IsAiming) 
        {
            TargetCamera.transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
        }
        
        //Change Current OffSet
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

        if (Cube)
        {
            Cube.transform.position = TargetCamera.transform.TransformPoint(DesirableVector) /*+ (transform.forward * CurrentMoveBackDistance)*/;
            Cube.transform.localEulerAngles = transform.localEulerAngles;

        }

        //Audit To Walls
        if (Physics.Raycast(TargetCamera.transform.TransformPoint(DesirableVector) - (transform.forward * (DesirableVector.z)) /*- (transform.forward * 1.0f)*/, -transform.forward, out RaycastHit HitInfo, CurrentMoveBackDistance))
        {
            transform.position = HitInfo.point;
        }
        
        //Draw Ray Backward
        Debug.DrawRay(TargetCamera.transform.TransformPoint(DesirableVector) , -(transform.forward * CurrentMoveBackDistance), Color.blue);

        //Change Position The "Cube1"
        if (Cube1)
        {
            //Cube1.position = transform.position;
        }
        
        
        if (ControlerPlayer.IsAiming)
        {
            Vector3 TransfromOffSetAiming = TargetCamera.transform.TransformPoint(OffsetCameraToAiming);
            if ((TransfromOffSetAiming - CurrentOffSetCamera).magnitude > MaxMagnitude)
            {
                //Debug.Log("CurrentMagnitude > MaxMagnitude");
                transform.position = TargetCamera.transform.TransformPoint(OffsetCameraSimple);
            }
            
        }
        

    }


    void LerpCamera(Vector3 TargetVector)
    {
        t = ((CurrentLenghtOfOneStep / (TargetVector - transform.position).magnitude) * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, TargetVector, t);
    }


}