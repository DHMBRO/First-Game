using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScopeControler : MonoBehaviour
{
    [SerializeField] float DefaultFiledOfView = 60.0f; 

    Camera LocalCamera;

    private void Start()
    {
        LocalCamera = GetComponent<Camera>();
    }

    public void UseScope(ScopeScr ScrScope, bool Aim)
    {
        if (Aim) 
        {
            LocalCamera.fieldOfView = ScrScope.Return_AimFieldFoView();
        }
        else
        {
            LocalCamera.fieldOfView = DefaultFiledOfView;
        }

    }

}
