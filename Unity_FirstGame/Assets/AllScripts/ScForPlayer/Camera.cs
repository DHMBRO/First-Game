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
    

    [SerializeField] float SpeedMove;
    [SerializeField] float SpeedInAiming; 
    [SerializeField] float SpeedRun;

    [SerializeField] MoodCamera CameraMood;
    [SerializeField] public bool Aiming = false;
    [SerializeField] bool CameraIsUsig = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(HandTarget) RotateHandToSimple();
    
    }

    private void Update()
    {
        if(ControlerUi) ControlerUi.Scope.gameObject.SetActive(Aiming);
        
        if (ControlerUi && !ControlerUi.InventoryIsOpen || true)
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
                RoateHandToAiming();
            }
            if (Input.GetKeyUp(KeyCode.Mouse1) && CameraIsUsig)
            {
                Aiming = false;
                RotateHandToSimple();
            }


            if (CameraIsUsig)
            {
                if (CameraMood == MoodCamera.ThirdFace)
                {
                    if (!Aiming)
                    {
                        RotateCameraSimple();
                        MoveBackCammera();


                        //RotateTargetCamera();
                    }
                    else
                    {
                        StateCameraAiming();

                        RotateCameraAiming();

                        MoveInAiming();
                    }

                }
                if (CameraMood == MoodCamera.FirstFace)
                {
                    //TargetCamera.transform.localEulerAngles = new Vector3(0.0f, TargetCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens, 0.0f);

                }
            }


        }




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
        /*
        TargetCamera.transform.localEulerAngles = new Vector3(
        0.0f,
        TargetCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens,
        0.0f);
        */
        
        transform.localEulerAngles = new Vector3(
        0.0f,
        transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens,
        0.0f);
        
        
        
        TargetCamera.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);

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
        transform.position = TargetPosition01 - transform.forward;
    }


    void MoveBackCammera()
    {
        Vector3 TargetPosition = TargetCamera.transform.TransformPoint(OffsetCamera);
        transform.position = TargetPosition - transform.forward * MoveBack;
         
    }

    
}