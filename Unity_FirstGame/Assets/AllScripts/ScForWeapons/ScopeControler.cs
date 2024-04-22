using UnityEngine;
using UnityEngine.UI;

public class ScopeControler : MonoBehaviour
{
    [SerializeField] Image UIScopeImage;
    [SerializeField] Vector3 OffSetCamera;

    [SerializeField] bool IsWorking = false;
    [SerializeField] bool UsingScopeImage = false;
    

    public Image ReturnUIIScopeMage()
    {
        Image SelectedImage = null;

        if (UsingScopeImage)
        {
            SelectedImage = UIScopeImage;
        }

        return SelectedImage;
    }

    public Vector3 ReturnOffSetCamera()
    {
        
        return transform.TransformPoint(OffSetCamera);
    }

    
}
