using UnityEngine;

public class LaserControler : MonoBehaviour
{
    [SerializeField] ShootControler ShootControlerWeapon;
    [SerializeField] LineRenderer LaserRenderer; 
    
    [SerializeField] bool HaveToWork = false;
    [SerializeField] float LaserMaxDistance = 100.0f;
    public Vector3 LaserEndPoint;

    private void Start()
    {
        LaserRenderer = GetComponent<LineRenderer>();
        LaserRenderer.enabled = false;

        if (LaserRenderer) LaserRenderer.enabled = false;
        else Debug.Log("Not set LaserRenderer");


    }

    private void Update()
    {
        if (!LaserRenderer || !ShootControlerWeapon) return;

        if (HaveToWork) SetLaser();

        switch (ShootControlerWeapon.Weapon)
        {
            case StateWeapon.IsUsing:
                LaserRenderer.enabled = true;
                SetLaser();
                break;
            default:
                LaserRenderer.enabled = false;
                LaserEndPoint = Vector3.zero;
                break;
        }

    }
    
    private void SetLaser()
    {
        Vector3 EndPoint = transform.position + -transform.forward * LaserMaxDistance;

        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit HitResult))
        {
            EndPoint = HitResult.point;    
        }

        
        LaserRenderer.SetPosition(0, transform.position);
        
        if (LaserEndPoint == Vector3.zero)
        {
            LaserRenderer.SetPosition(1, EndPoint);
        }
        else LaserRenderer.SetPosition(1, LaserEndPoint);

        LaserRenderer.enabled = true;

    }

}
