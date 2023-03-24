using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] GameObject TargetCamera;
    [SerializeField] Vector3 Offset;
    [SerializeField] float MouseSens = 1.0f;
    [SerializeField] float MoveBack = 5.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
            
    }

    private void Update()
    {
        

        transform.localEulerAngles = new Vector3(
        transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * MouseSens, 
        transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSens, 
        0.0f);

        Vector3 TargetPosition = TargetCamera.transform.TransformPoint(Offset);
        transform.position = TargetPosition - transform.forward * MoveBack;
    }


}