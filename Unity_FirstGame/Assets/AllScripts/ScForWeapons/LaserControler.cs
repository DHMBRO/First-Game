using UnityEngine;

public class LaserControler : MonoBehaviour
{
    [SerializeField] ShootControler ShootControlerWeapon;
    [SerializeField] LineRenderer LaserRenderer; 
    
    [SerializeField] bool HaveToWork = false;
    [SerializeField] float LaserMaxDistance = 100.0f;

    private void Start()
    {
        LaserRenderer = GetComponent<LineRenderer>();
        if (LaserRenderer) LaserRenderer.enabled = false;
        else Debug.Log("Not set LaserRenderer");

    }

    private void Update()
    {
        if (!LaserRenderer || !ShootControlerWeapon) return;

        if (HaveToWork) EnableLaser();

        switch (ShootControlerWeapon.Weapon)
        {
            case StateWeapon.IsUsing:
                LaserRenderer.enabled = true;
                EnableLaser();
                break;
            default:
                LaserRenderer.enabled = false;
                break;
        }

    }
    
    private void EnableLaser()
    {
        Vector3 EndPoint = transform.position + -transform.forward * LaserMaxDistance;

        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit HitResult))
        {
            EndPoint = HitResult.point;    
        }

        LaserRenderer.SetPosition(0, transform.position);
        LaserRenderer.SetPosition(1, EndPoint);

    }
    
}