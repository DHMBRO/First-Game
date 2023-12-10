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
    [SerializeField] float LenghtToOneStep = 100.0f;
    [SerializeField] float LenghtOfOneStepToChangeState = 3.0f;

    [SerializeField] MoodCamera CameraMood;
    //[SerializeField] CameraIs CurrentState;

    //[SerializeField] public bool Aiming = false;
    [SerializeField] public bool CameraIsUsig = false;

    Vector3 TargetPosition01;
    Vector3 TargetPosition02;

    bool CameraState01 = false;
    bool CameraState02 = false;

    bool FraimSimple = false;
    bool FraimAiming = false;

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

        EulerX = Mathf.Clamp(EulerX, -60.0f, 50.0f);
        

        transform.eulerAngles = new Vector3(
           EulerX,
           transform.eulerAngles.y + (MouseX * MouseSens),
            0.0f);



        transform.position = TargetCamera.transform.TransformPoint(DesirableVector) + -(transform.forward * CurrentMoveBackDistance);

        
        


        //Debug.Log(transform.localEulerAngles.x + "\t" + transform.eulerAngles.x);

        //float a = 0.0f;
        //transform.eulerAngles = new Vector3(a, 0.0f, 0.0f);

        //Rotate Player When Camera Aiming
        if (ControlerPlayer.IsAiming) 
        {
            TargetCamera.transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
        }

        //Change Current OffSet
        if (ControlerPlayer.IsAiming && ControlerPlayer.InStealth)
        {
            DesirableVector = OffsetCameraToAimingInStelth;
            CurrentMoveBackDistance = MoveBackDistanceAiming;
        }
        else if (ControlerPlayer.IsAiming && !ControlerPlayer.InStealth)
        {
            DesirableVector = OffsetCameraToAiming;
            CurrentMoveBackDistance = MoveBackDistanceAiming;
        }
        else if (!ControlerPlayer.IsAiming && ControlerPlayer.InStealth)
        {
            DesirableVector = OffsetCameraInStelth;
            CurrentMoveBackDistance = MoveBackDistanceStelth;
        }
        else if(!ControlerPlayer.IsAiming && !ControlerPlayer.InStealth)
        {
            DesirableVector = OffsetCameraSimple;
            CurrentMoveBackDistance = MoveBackDistanceSimple;
        }


        //Debug.Log((CurrentOffSetCamera - TargetCamera.transform.position).magnitude);
        
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
        

        //Debug.Log(transform.position);


        //Cube1.position = TargetCamera.transform.TransformPoint(OffsetCameraSimple) - (transform.forward * 1.0f);


        /*
        if (ControlerUi) ControlerUi.Scope.gameObject.SetActive(Aiming);
        
        if (ControlerUi && !ControlerUi.InventoryIsOpen || !ControlerUi)
        {

            if (MoveBackObject)
            {
                if (Physics.Raycast(MoveBackObject.position, -MoveBackObject.forward, out RaycastHit ResultHit01, (MoveBack)))
                {
                    TargetPosition01 = (MoveBackObject.transform.TransformPoint(MoveBackObject.forward) - MoveBackObject.transform.forward * ResultHit01.distance);
                    //Debug.Log(TargetPosition);
                }

                if (Physics.Raycast(MoveBackObject.position, -MoveBackObject.forward, out RaycastHit ResultHit02, (MoveBack - 1.5f)))
                {
                    TargetPosition02 = MoveBackObject.transform.TransformPoint(MoveBackObject.forward) - MoveBackObject.forward * ResultHit02.distance;
                    //Debug.Log(TargetPosition);
                }

            }



            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CameraIsUsig = false;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CameraIsUsig = true;

            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && CameraIsUsig)
            {
                Aiming = true;
                FraimAiming = true;

                TargetCamera.transform.localEulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
                CurrentLenghtOfOneStep = LenghtOfOneStepToChangeState;

                transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z - 0.03f);

            }
            if (Input.GetKeyUp(KeyCode.Mouse1) && CameraIsUsig)
            {
                Aiming = false;
                FraimSimple = true;

                CurrentLenghtOfOneStep = LenghtOfOneStepToChangeState;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.03f, transform.position.z + 0.03f);
                
            }


            
            if (Aiming && !FraimAiming)
            {
                if ((transform.position - TargetPosition01).magnitude <= 0.5f)
                {
                    //Debug.Log("transform.position.y <= a.y || transform.position.z >= a.z");
                    
                    CameraState01 = true;
                    //CurrentState = CameraIs.Aiming;
                    CurrentLenghtOfOneStep = LenghtToOneStep;
                    
                    //Debug.Log("1");
                    //Debug.Log((transform.position - TargetPosition01).magnitude);

                }
                else CameraState01 = false;
            }

            
            if (!Aiming && !FraimSimple)
            {
                if ((transform.position - TargetPosition02).magnitude <= 0.5f)
                {
                    //Debug.Log("transform.position.y >= a.y || transform.position.z <= a.z");
                    
                    CameraState02 = true;
                    //CurrentState = CameraIs.Simple;
                    CurrentLenghtOfOneStep = LenghtToOneStep;

                    //Debug.Log("2");
                    //Debug.Log((transform.position - TargetPosition02).magnitude);

                }
                else CameraState02= false;

            }

            if (!CameraState01 && !CameraState02)
            {
                CurrentLenghtOfOneStep = LenghtOfOneStepToChangeState;
                //CurrentState = CameraIs.Null;
                //Debug.Log("!!!!!!!!!!!");
            }





            if (CameraIsUsig)
            {
                if (CameraMood == MoodCamera.ThirdFace)
                {
                    if (!Aiming)
                    {
                        RotateHandToSimple();
                        RotateCameraSimple();
                        
                        MoveBackCammera();
                        MoveBackObjectToRay();

                    }
                    else
                    {

                        RoateHandToAiming();
                        StateCameraAiming();
                        
                        RotateCameraAiming();
                        MoveBackObjectToRay();



                    }

                }
                if (CameraMood == MoodCamera.FirstFace)
                {
                    //TargetCamera.transform.localEulerAngles = new Vector3(0.0f, TargetCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens, 0.0f);

                }
            }

            
        }


        FraimSimple = false;
        FraimAiming = false;
        
        */

    }


    void LerpCamera(Vector3 TargetVector)
    {
        t = ((CurrentLenghtOfOneStep / (TargetVector - transform.position).magnitude) * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, TargetVector, t);
    }


}