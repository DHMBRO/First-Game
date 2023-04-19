using UnityEngine;

public class ShootControler : MonoBehaviour
{
    [SerializeField] private GameObject MyWeapon;
    [SerializeField] public Transform SlotForUseShop;
    [SerializeField] public GameObject WeaponShoop;
    [SerializeField] private GameObject Muzzle;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Collet;
    [SerializeField] private GameObject ColletPoint;
    

    [SerializeField] public float ShotDeley = 1.0f;
    [SerializeField] public float ShotTime = 0.0f;
    [SerializeField] public float Mass = 0.0f;
    [SerializeField] public float SpeedForBullet = 0.0f;
    [SerializeField] public byte AtemptForFire; 

    [SerializeField] private Rigidbody WeaonRigidbody;
    [SerializeField] public Transform CameraTransform;
    [SerializeField] private SlotControler MySlotControler;


    [SerializeField] private float ColletSpeed = 0.0f;
    private Transform ShootPoint;
    private float BulletSpeed = 100;
    private string NameForWeapon;
    private bool CanFire = false;
    
    

    void Start()
    {
        NameForWeapon = gameObject.tag;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ShopGlok"))
        {
            UseNumbersBullets01(other.gameObject);
            Debug.Log("1");
        }
        else if(other.gameObject.CompareTag("ShopM4"))
        {
            UseNumbersBullets01(other.gameObject);
            Debug.Log("2");
        }
        else if(other.gameObject.CompareTag("ShopAK47"))
        {
            UseNumbersBullets01(other.gameObject);
            Debug.Log("3");
        }                
    }

    void UseNumbersBullets01(GameObject Shop)
    {
        ShopControler WeaponHaveBullets = Shop.gameObject.GetComponent<ShopControler>();
        if (WeaponHaveBullets)
        {
            AtemptForFire = WeaponHaveBullets.CurrentAmmo;
            WeaponShoop = WeaponHaveBullets.gameObject;
        }

    }


    private void Update()
    {
        if (gameObject.transform.parent && gameObject.transform.parent.tag == "SlotForUse")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) ;
            {
                ShootForM4();
            }
        }
    }

    public void ShootForM4()
    {
        if (MyWeapon && Muzzle && Bullet)
        {
            CanFire = true;
            Shoot(MyWeapon, Muzzle,ColletPoint,  Collet, Bullet);
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


    void Shoot(GameObject Weapon, GameObject Muzzle,  GameObject ColletPoint, GameObject Collet, GameObject Bullet)//ColectPoint
    {
        if (CanFire)
        {            
            Vector3 TargetPoint = Muzzle.transform.position + Muzzle.transform.forward * 100.0f;
            RaycastHit Hitresult;
            
            if (Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, out Hitresult))
            {
                Debug.DrawRay(Muzzle.transform.position, Muzzle.transform.forward * 100.0f, Color.blue);
                TargetPoint = Hitresult.point;
            }
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= ShotTime)
            {                
                ShotTime = ShotDeley + Time.time;
                GameObject newBullet = Instantiate(Bullet, Muzzle.transform.position, Quaternion.LookRotation(TargetPoint - Muzzle.transform.position));
                newBullet.transform.rotation = Muzzle.transform.rotation;

                Rigidbody newBulletRB = newBullet.GetComponent<Rigidbody>();
                newBulletRB.AddForce(newBullet.transform.forward * BulletSpeed, ForceMode.Impulse);

                GameObject newCollet = Instantiate(Collet, ColletPoint.transform.position, Quaternion.LookRotation(TargetPoint - Muzzle.transform.position));
                newCollet.transform.rotation = Muzzle.transform.rotation;
                
                Rigidbody newColletRB = newBullet.GetComponent<Rigidbody>();
                
                newColletRB.AddRelativeForce(ColletPoint.transform.forward * ColletSpeed, ForceMode.Impulse);//Colect Point 
                Destroy(newCollet,3.0f);
                Destroy(newColletRB, 0.5f);
                Destroy(newBullet, 3.0f);
            }
        }
        
        
    }
}