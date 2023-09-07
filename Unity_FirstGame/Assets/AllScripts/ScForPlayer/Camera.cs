using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] UiControler ControlerUi;

    [SerializeField] public GameObject TargetCamera;
    
    [SerializeField] Transform HandTarget;
    [SerializeField] Transform Cube; 

    [SerializeField] private Rigidbody RigTarget;

    [SerializeField] private Transform MoveBackObject;


    [SerializeField] Vector3 OffsetCamera;

    [SerializeField] Vector3 OffsetCameraToAiming;

    [SerializeField] Vector3 OffsetHandForAimingTra;
    [SerializeField] Vector3 OffsetHandForAimingRot;

    [SerializeField] Vector3 OffsetHandPos;
    [SerializeField] Vector3 OffsetHandRot;

    [SerializeField] float MouseSens = 1.0f;
    [SerializeField] float MoveBack = 5.0f;

    [SerializeField] float t;
    [SerializeField] float CurrentLenghtOfOneStep;
    [SerializeField] float LenghtToOneStep = 100.0f;
    [SerializeField] float LenghtOfOneStepToChangeState = 3.0f;

    [SerializeField] float SpeedMove;
    [SerializeField] float SpeedInAiming; 
    [SerializeField] float SpeedRun;

    [SerializeField] MoodCamera CameraMood;
    [SerializeField] CameraIs CurrentState;

    [SerializeField] public bool Aiming = false;
    [SerializeField] private bool CameraIsUsig = false;

    Vector3 TargetPosition01;
    Vector3 TargetPosition02;

    bool CameraState01 = false;
    bool CameraState02 = false;

    [SerializeField] bool FraimSimple = false;
    [SerializeField] bool FraimAiming = false;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(HandTarget) RotateHandToSimple();

        CurrentLenghtOfOneStep = LenghtOfOneStepToChangeState;


    }

    private void Update()
    {
        
        
        if (ControlerUi) ControlerUi.Scope.gameObject.SetActive(Aiming);
        
        if (ControlerUi && !ControlerUi.InventoryIsOpen || !ControlerUi)
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

            Vector3 a = TargetPosition01;
            a.y += 0.01f;
            a.z -= 0.01f;

            Vector3 a1 = TargetPosition02;
            a1.y -= 0.01f;
            a1.z += 0.01f;


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CameraIsUsig = false;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
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
                if ((transform.position - a).magnitude <= 0.2f)
                {
                    //Debug.Log("transform.position.y <= a.y || transform.position.z >= a.z");
                    
                    CameraState01 = true;
                    CurrentState = CameraIs.Aiming;
                    CurrentLenghtOfOneStep = LenghtToOneStep;
                    
                    Debug.Log("1");
                    
                }
                else CameraState01 = false;
            }

            if (!Aiming && !FraimSimple)
            {
                if ((transform.position - a1).magnitude <= 0.2f)
                {
                    //Debug.Log("transform.position.y >= a.y || transform.position.z <= a.z");
                    
                    CameraState02 = true;
                    CurrentState = CameraIs.Simple;
                    CurrentLenghtOfOneStep = LenghtToOneStep;
                    
                    Debug.Log("2");
                }
                else CameraState02= false;

            }

            if (!CameraState01 && !CameraState02)
            {
                CurrentLenghtOfOneStep = LenghtOfOneStepToChangeState;
                CurrentState = CameraIs.Null;
                Debug.Log("!!!!!!!!!!!");
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

    }
    
    
    
    
    void LerpCamera(Vector3 TargetVector)
    {
        t = ((CurrentLenghtOfOneStep / (TargetVector - transform.position).magnitude) * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, TargetVector, t);
    }

    void RoateHandToAiming()
    {
        Vector3 TargetPoint;

        if (FraimAiming)
        {
            
            HandTarget.localPosition = OffsetHandForAimingTra;
            //HandTarget.localEulerAngles = OffsetHandForAimingRot;

        }
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit HitResult))
        {
            TargetPoint = transform.TransformPoint(transform.forward) + transform.forward * (HitResult.distance);

            Debug.Log(TargetPoint);
            //Cube.position = TargetPoint; 
            //TargetPoint.z = 0.0f;

            //HandTarget.LookAt(TargetPoint);
            //HandTarget.localPosition = OffsetHandForAimingTra;

        }




    }

    void RotateHandToSimple()
    {
        if (!FraimSimple)
        {
            HandTarget.localPosition = OffsetHandPos;
            HandTarget.localEulerAngles = OffsetHandRot;
        }
    }


    void RotateCameraSimple()
    {
        transform.localEulerAngles = new Vector3(
        transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * MouseSens,
        transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens,
        0.0f);

        MoveBackObject.transform.localEulerAngles = new Vector3(
        MoveBackObject.transform.localEulerAngles.x - Input.GetAxis("Mouse Y"),
        MoveBackObject.transform.localEulerAngles.y + Input.GetAxis("Mouse X"),
        0.0f);
    }

    void RotateCameraAiming()
    {
        MoveBackObject.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y * MouseSens, 0.0f);


        transform.eulerAngles = new Vector3(transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * MouseSens,
        TargetCamera.transform.eulerAngles.y * MouseSens, 0.0f);


        TargetCamera.transform.localEulerAngles = new Vector3(
        0.0f,
        TargetCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens,
        0.0f);

    }

    void StateCameraAiming()
    {
        TargetPosition01 = TargetCamera.transform.TransformPoint(OffsetCameraToAiming);
        
        Debug.DrawRay(MoveBackObject.position, -MoveBackObject.forward * (MoveBack), Color.blue);
        if (Physics.Raycast(MoveBackObject.position, -MoveBackObject.forward, out RaycastHit ResultHit01, (MoveBack)))
        {
            TargetPosition01 = (MoveBackObject.transform.TransformPoint(MoveBackObject.forward) - MoveBackObject.transform.forward * ResultHit01.distance);
            //Debug.Log(TargetPosition);
        }




        LerpCamera(TargetPosition01);
        //Debug.Log(a);

        //Debug.Log("Camera is Aiming");
        
    }


    void MoveBackCammera()
    {
        TargetPosition02 = (TargetCamera.transform.TransformPoint(OffsetCamera)) - transform.forward * MoveBack;

        //transform.position = TargetPosition - transform.forward * MoveBack;
        //Vector3 a = TargetPosition - transform.forward * MoveBack;
        Debug.DrawRay(MoveBackObject.position, -MoveBackObject.forward * (MoveBack - 1.0f),Color.blue);
        if (Physics.Raycast(MoveBackObject.position, -MoveBackObject.forward, out RaycastHit ResultHit02, (MoveBack - 1.5f)))
        {
            TargetPosition02 = MoveBackObject.transform.TransformPoint(MoveBackObject.forward) - MoveBackObject.forward * ResultHit02.distance;
            //Debug.Log(TargetPosition);
        }

        //Debug.Log(a);

        LerpCamera(TargetPosition02);
        
    }

    void MoveBackObjectToRay()
    {
        
        Vector3 TargetPosition = TargetCamera.transform.TransformPoint(OffsetCamera);
        if (!Aiming)
        {
            MoveBackObject.position = TargetPosition - MoveBackObject.forward;
        }
        else
        {
            TargetPosition = (TargetCamera.transform.TransformPoint(OffsetCameraToAiming)) + MoveBackObject.forward * (MoveBack + 1.0f);
            MoveBackObject.position = TargetPosition - MoveBackObject.forward;
        }


    }

}