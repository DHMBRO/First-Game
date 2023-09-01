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

    [SerializeField] bool IsAiming = false;
    [SerializeField] bool IsNotAiming = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(HandTarget) RotateHandToSimple();


        
    }

    private void Update()
    {
        
       
        
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
            if (Input.GetKey(KeyCode.Mouse1) && CameraIsUsig)
            {
                Aiming = true;
                //Vector3 TargetVector = transform.TransformPoint(OffsetCameraToAiming);
                //LerpCamera(TargetVector);
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
                        

                        RotateHandToSimple();
                        RotateCameraSimple();
                        MoveBackCammera();
                        
                    }
                    else
                    {

                        RoateHandToAiming();
                        StateCameraAiming();
                        RotateCameraAiming();

                        
                    }

                }
                if (CameraMood == MoodCamera.FirstFace)
                {
                    //TargetCamera.transform.localEulerAngles = new Vector3(0.0f, TargetCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens, 0.0f);

                }
            }

            
        }
        




    }
    
    
    
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
    
    void LerpCamera(Vector3 TargetVector)
    {
        t = ((LenghtToOneStep / (TargetVector - transform.position).magnitude) * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, TargetVector, t);
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

        transform.eulerAngles = new Vector3(0.0f, TargetCamera.transform.eulerAngles.y * MouseSens, 0.0f);
        
        TargetCamera.transform.localEulerAngles = new Vector3(
        0.0f,
        TargetCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens,
        0.0f);
        

        /*
        transform.localEulerAngles = new Vector3(
        transform.localEulerAngles.x - Input.GetAxis("Mouse X") * MouseSens, 0.0f, 0.0f);
        */

    }

    void StateCameraAiming()
    {
        Vector3 TargetPosition01 = TargetCamera.transform.TransformPoint(OffsetCameraToAiming);
        transform.position = TargetPosition01 - transform.forward;
        //LerpCamera(TargetPosition01);
        
        Debug.Log("Camera is Aiming");
        
    }


    void MoveBackCammera()
    {
        Vector3 TargetPosition = TargetCamera.transform.TransformPoint(OffsetCamera);
        transform.position = TargetPosition - transform.forward * MoveBack;
        //LerpCamera(TargetPosition);
    }

    
}