using UnityEngine;

public class ScopeScr : MonoBehaviour
{
    [SerializeField] float AimFieldFoView = 60.0f;

    public float Return_AimFieldFoView()
    {
        return AimFieldFoView;
    }

}
