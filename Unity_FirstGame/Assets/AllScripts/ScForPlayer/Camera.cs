using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] public GameObject TargetCamera;
    [SerializeField] Vector3 Offset;
    [SerializeField] float MouseSens = 1.0f;
    [SerializeField] float MoveBack = 5.0f;    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        //Vector3 RotateX = new Vector3(0.0f, 0.0f + MouseX * MouseSens, 0.0f);
        //Vector3 RotateY = new Vector3(0.0f + MouseY * MouseSens, 0.0f, 0.0f);

        TargetCamera.transform.Rotate(MouseSens * new Vector3(0.0f + -MouseY, 0.0f + MouseX, 0.0f));
        

    }



    void RotateCamera()
    {
        
        transform.localEulerAngles = new Vector3(
        transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * MouseSens,
        transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens,
        0.0f);
        
        TargetCamera.transform.localEulerAngles = new Vector3(0.0f, TargetCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens, 0.0f);
    }
    
    void CammeraOffset()
    {
        Vector3 TargetPosition = TargetCamera.transform.TransformPoint(Offset);
        //transform.position = TargetPosition - transform.forward * MoveBack;
    }

    
}