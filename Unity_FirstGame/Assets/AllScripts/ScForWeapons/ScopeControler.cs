using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScopeControler : MonoBehaviour
{
    [SerializeField] float DefaultFiledOfView = 60.0f; 

    Camera LocalCamera;
    ThirdPersonCamera CameraScr;

    private void Start()
    {
        LocalCamera = GetComponent<Camera>();
        CameraScr = GetComponent<ThirdPersonCamera>();
    }

    public void UseScope(ScopeScr ScrScope, bool Aim)
    {
        if (Aim) 
        {
            LocalCamera.fieldOfView = ScrScope.Return_AimFieldFoView();
            CameraScr.CurrentMouseSens = (CameraScr.DefaultMouseSens / (DefaultFiledOfView / ScrScope.Return_AimFieldFoView()));
        }
        else
        {
            LocalCamera.fieldOfView = DefaultFiledOfView;
            CameraScr.CurrentMouseSens = CameraScr.DefaultMouseSens;
        }

    }

}
