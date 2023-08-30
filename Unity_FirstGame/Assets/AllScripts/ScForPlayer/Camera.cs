using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] UiControler ControlerUi;

    [SerializeField] public GameObject TargetCamera;
    [SerializeField] Transform HandTarget;
    [SerializeField] private Rigidbody RigTarget; 

    [SerializeField] Vector3 OffsetCamera;

    [SerializeField] Vector3 OffsetCameraToAiming;

    [SerializeField] Vector3 OffsetHandForAimingTra;
    [SerializeField] Vector3 OffsetHandForAimingRot;

    [SerializeField] Vector3 OffsetHandPos;
    [SerializeField] Vector3 OffsetHandRot;


    [SerializeField] float MouseSens = 1.0f;
    [SerializeField] float MoveBack = 5.0f;

    [SerializeField] float t;
    [SerializeField] float LenghtToOneStep = 3.0f;

    [SerializeField] float SpeedMove;
    [SerializeField] float SpeedInAiming; 
    [SerializeField] float SpeedRun;

    [SerializeField] MoodCamera CameraMood;
    [SerializeField] public bool Aiming = false;
    [SerializeField] bool CameraIsUsig = false;

    [SerializeField] SatetsCamera CurrentState;

    Vector3 TargetPosition01;
    Vector3 TargetPosition02;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(HandTarget) RotateHandToSimple();


        
    }

    private void Update()
    {
        
        /*
        if (CurrentState == SatetsCamera.Simple)
        {
            //Task01();
            Debug.Log("1");
        }
        else if(CurrentState == SatetsCamera.Aiming)
        {
            //Task02();
            Debug.Log("2");

        }
        */
        
        if(ControlerUi) ControlerUi.Scope.gameObject.SetActive(Aiming);
        
        if (ControlerUi && !ControlerUi.InventoryIsOpen || !ControlerUi)
        {
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
                   
            }

            if (Input.GetKeyUp(KeyCode.Mouse1) && CameraIsUsig)
            {
                Aiming = false;
                
            }
            
            
            if (CameraIsUsig)
            {
                if (CameraMood == MoodCamera.ThirdFace)
                {
                    if (!Aiming)
                    {
                        TargetPosition01 = TargetCamera.transform.TransformPoint(OffsetCamera);
                        RotateHandToSimple();

                        if (transform.position != TargetPosition01) RotateCameraToStateSimple();

                        RotateCameraSimple();
                        MoveBackCammera();
                        
                    }
                    else
                    {
                        
                        TargetPosition02 = TargetCamera.transform.TransformPoint(OffsetCameraToAiming);
                        RoateHandToAiming();

                        if (transform.position != TargetPosition02) RotateCameraToStateAiming();
                        StateCameraAiming();
                        RotateCameraAiming();

                        //MoveInAiming();
                    }

                }
                if (CameraMood == MoodCamera.FirstFace)
                {
                    //TargetCamera.transform.localEulerAngles = new Vector3(0.0f, TargetCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens, 0.0f);

                }
            }

            
        }
        




    }
    
    /*
    void Task01()
    {
        Vector3 CameraSimple = TargetCamera.transform.TransformPoint(OffsetCamera);
        t = (LenghtToOneStep/(CameraSimple - transform.position).magnitude*Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position,CameraSimple,t);
    }

    void Task02()
    {
        Vector3 CameraAiming = TargetCamera.transform.TransformPoint(OffsetCameraToAiming);
        t = (LenghtToOneStep/(CameraAiming - transform.position).magnitude*Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position,CameraAiming,t);
    }
    */
    
    void RotateCameraToStateSimple()
    {
        t = ((LenghtToOneStep / (TargetPosition01 - transform.position).magnitude * Time.deltaTime));
        transform.position = Vector3.Lerp(transform.position, TargetPosition01, t);
    }

    void RotateCameraToStateAiming()
    {
        t = ((LenghtToOneStep / (TargetPosition02 - transform.position).magnitude) * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, TargetPosition02, t);
        Debug.Log("Camera is lerping");
    }
    

    void RoateHandToAiming()
    {
        HandTarget.localPosition = OffsetHandForAimingTra;
        HandTarget.localEulerAngles = OffsetHandForAimingRot;  
    }

    void RotateHandToSimple()
    {
        HandTarget.localPosition = OffsetHandPos;
        HandTarget.localEulerAngles = OffsetHandRot;
    }


    void RotateCameraSimple()
    {
        transform.localEulerAngles = new Vector3(
        transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * MouseSens,
        transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens,
        0.0f);

    }

    void RotateCameraAiming()
    {
        
        TargetCamera.transform.localEulerAngles = new Vector3(
        0.0f,
        TargetCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens,
        0.0f);
        
        /*
        transform.localEulerAngles = new Vector3(
        transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * MouseSens,
        transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens,
        0.0f);
        */
        
        
        transform.eulerAngles = new Vector3(0.0f, TargetCamera.transform.eulerAngles.y, 0.0f);
        

    }

    void RotateTargetCamera()
    {
        
        return;
        //Quaternion CurentRotation = TargetCamera.transform.rotation;
        RigTarget = TargetCamera.GetComponent<Rigidbody>();



        if (Input.GetKey(KeyCode.W))
        {
            
            //TargetCamera.transform.rotation = Quaternion.AngleAxis(1.0f, new Vector3(0.0f, transform.eulerAngles.y, 0.0f));
            
            if (Input.GetKey(KeyCode.A))
            {
                TargetCamera.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y - 45.0f, 0.0f);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                TargetCamera.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + 45.0f, 0.0f);
            }
            else TargetCamera.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
            MoveForward();// !!!
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
            {
                TargetCamera.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y - 135.0f, 0.0f);    
            }
            else if (Input.GetKey(KeyCode.D))
            {
                TargetCamera.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + 135.0f, 0.0f);
                
            }
            else TargetCamera.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + 180.0f, 0.0f);
            MoveForward();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            TargetCamera.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y - 90.0f, 0.0f);
            MoveForward();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            TargetCamera.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + 90.0f, 0.0f);
            MoveForward();
        }

        RigTarget.velocity = new Vector3(0, RigTarget.velocity.y, 0);


        
        
    }

    void MoveForward()
    {
        Vector3 MoveForward = new Vector3(0.0f, 0.0f, SpeedMove);
        RigTarget.AddRelativeForce(MoveForward, ForceMode.Force);

        if (Input.GetKey(KeyCode.LeftShift)) RigTarget.AddRelativeForce(MoveForward * SpeedRun, ForceMode.Force);
    }

    void MoveInAiming()
    {
        float MoveVertical = Input.GetAxisRaw("Vertical") * 100000;
        float MoveHorizontal = Input.GetAxisRaw("Horizontal") * 100000;

        

        Vector3 ForceBack = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;
        Vector3 ForrceForward = new Vector3(MoveHorizontal, 0.0f, MoveVertical).normalized;

        RigTarget.velocity = new Vector3(0, RigTarget.velocity.y, 0);


        RigTarget.AddRelativeForce(ForceBack * SpeedInAiming, ForceMode.Force);
        

    }


    void StateCameraAiming()
    {
        Vector3 TargetPosition01 = TargetCamera.transform.TransformPoint(OffsetCameraToAiming);
        //transform.position = TargetPosition01 - transform.forward;
            
        Debug.Log("Camera is Aiming");
        
    }


    void MoveBackCammera()
    {
        Vector3 TargetPosition = TargetCamera.transform.TransformPoint(OffsetCamera);
        transform.position = TargetPosition - transform.forward * MoveBack;
         
    }

    
}