using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    float mousex = 0;
    float mousey = 0;
    [SerializeField] GameObject Cam;
    [SerializeField] GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() 
    {
        mousex += Input.GetAxis("Mouse X")*10;
        mousey += Input.GetAxis("Mouse Y")*10;
        Vector3 rotation = new Vector3(mousey, 0, -mousex);
        Cam.transform.position = Player.transform.position;
        Cam.transform.rotation = Quaternion.Euler(rotation);
        Cam.transform.forward -= new Vector3(0, 0, 2);
        RaycastHit Hit_Result;
        if (Physics.Raycast(Cam.transform.position, -Player.transform.forward, out Hit_Result, 5.00f))
        {
            Cam.transform.position = Hit_Result.point;
            Cam.transform.forward -= new Vector3(0, 0, 2);
            Cam.transform.LookAt(Hit_Result.point - Player.transform.position);
        }
    }
}
