using UnityEngine;

public class ShootControler : MonoBehaviour
{
    [SerializeField] private GameObject MyWeapon;
    [SerializeField] private GameObject Muzzle;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Collet;
    [SerializeField] private GameObject ColletPoint;
    
    [SerializeField] public float ShotDeley = 1.0f;
    [SerializeField] public float ShotTime = 0.0f;

    [SerializeField] private Rigidbody WeaonRigidbody;
    [SerializeField] public Transform CameraTransform;
    [SerializeField] private SlotControler MySlotControler;
    

    private float ColletSpeed = 3.0f;
    private Transform ShootPoint;
    private float BulletSpeed = 100;
        private string NameForWeapon;
    private bool CanFire = false;
    

    void Start()
    {
        if (gameObject.CompareTag("M4"))
        {
            NameForWeapon = "M4";
        }
        else if (gameObject.CompareTag("AK47"))
        {
            NameForWeapon = "AK47";
        }
        else if (gameObject.CompareTag("M249"))
        {
            NameForWeapon = "M249";
        }
        else if (gameObject.CompareTag("Glok"))
        {
            NameForWeapon = "Glok";
        }        
        else if (gameObject.CompareTag("M1911"))
        {
            NameForWeapon = "M1911";
        }
        //
        if (!MyWeapon)
        {
            Debug.Log("You dont have Weapon");
        }
        else if (!Muzzle)
        {
            Debug.Log("You dont have Muzzle");
        }
        else if (!Bullet)
        {
            Debug.Log("You dont have Bullet");
        }

    }
    
    public void ShootForM4()
    {
        if (MyWeapon && Muzzle && Bullet)
        {
            CanFire = true;
        }

    }

    public void ShootForAK47()
    {

    }    

    public void ShootM249()
    {

    }

    public void ShootM1911()
    {

    }

    public void ShootGlok()
    {

    }


    GameObject Shoot(GameObject Weapon, GameObject Muzzle, GameObject TransformCamera, GameObject ColletPoint, GameObject Collet, GameObject Bullet)
    {
        if (CanFire)
        {
            Debug.Log("Is work ");
            Vector3 TargetPoint = TransformCamera.transform.position + TransformCamera.transform.forward * 100.0f;
            RaycastHit Hitresult;
            if (Physics.Raycast(TransformCamera.transform.position, TransformCamera.transform.forward, out Hitresult))
            {
                Debug.DrawRay(TransformCamera.transform.position, TransformCamera.transform.forward * 100.0f, Color.black);
                TargetPoint = Hitresult.point;
            }
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= ShotTime)
            {
                Debug.Log("Shoot");
                ShotTime = ShotDeley + Time.time;
                GameObject newBullet = Instantiate(Bullet, Muzzle.transform.position, Quaternion.LookRotation(TargetPoint - Muzzle.transform.position));
                newBullet.transform.rotation = Muzzle.transform.rotation;

                Rigidbody newBulletRB = newBullet.GetComponent<Rigidbody>();

                newBulletRB.AddForce(newBullet.transform.forward * BulletSpeed, ForceMode.Impulse);

                GameObject newCollet = Instantiate(Bullet, Muzzle.transform.position, Quaternion.LookRotation(TargetPoint - Muzzle.transform.position));
                newCollet.transform.rotation = Muzzle.transform.rotation;
                Rigidbody newColletRB = newBullet.GetComponent<Rigidbody>();
                newColletRB.AddRelativeForce(ColletPoint.transform.forward * ColletSpeed, ForceMode.Impulse);
            }
        }
        
        return Bullet;
    }
}