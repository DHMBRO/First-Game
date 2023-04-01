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

    private void FixedUpdate()
    {
        Raycast(gameObject.transform, 6.0f);


    }

    private void Update()
    {


        RotateCamera();
        CammeraOffset();
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
        transform.position = TargetPosition - transform.forward * MoveBack;
    }

    public void Raycast(Transform SatrtPosForRay, float DistanceForRay)
    {        
        Ray CursosRay = new Ray(SatrtPosForRay.position, SatrtPosForRay.forward * DistanceForRay);
        if (Physics.Raycast(CursosRay, out RaycastHit HitResult))
        {
            Debug.Log(HitResult.collider.tag);            
        }
        
        Debug.DrawRay(transform.position, transform.forward * DistanceForRay, Color.red);
           
    }
    
}