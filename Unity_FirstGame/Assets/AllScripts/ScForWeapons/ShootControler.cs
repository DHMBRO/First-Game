using UnityEngine;


public class ShootControler : MonoBehaviour
{
    public WeaponIsShoting SetShootDelegat;
    private bool CanWork;

    [SerializeField] public Transform SlotForUseShop;
    [SerializeField] public ShopControler WeaponShoop;
    [SerializeField] private GameObject GameObjectForRay;
    [SerializeField] private GameObject Muzzle;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Collet;
    [SerializeField] private GameObject ColletPoint;

    public TypeWeapon TheGun;
    public TypeCaliber CaliberToWeapon;
    public StateWeapon Weapon;

    [SerializeField] public float ShotDeley = 1.0f;
    [SerializeField] public float ShotTime = 0.0f;
    [SerializeField] public float Mass = 0.0f;
    [SerializeField] private float BulletSpeed = 0.0f;

    [SerializeField] private float ChangedAngle = 0.0f;

    public bool UnLimitedAmmo;
    //[SerializeField] private float ColletSpeed = 0.0f;


    void Start()
    {
        if (!Muzzle) Debug.Log("Not set Muzzle");
        if (!Bullet) Debug.Log("Not set Bullet");

        SetShootDelegat += ShootWeapon;
    }

    private void Update()
    {
        Transform ParentWeapon = GetComponentInParent<Transform>();
        
        if (ParentWeapon.parent)
        {
            if (ParentWeapon.parent.tag == "SlotOwner")
            {
                Weapon = StateWeapon.HaveOwner;
            }
            else if (ParentWeapon.parent.tag == "SlotForUse")
            {
                Weapon = StateWeapon.IsUsing;
            }
            else Weapon = StateWeapon.HaventOwher;
        }
        else Weapon = StateWeapon.HaventOwher;
        
    }

    public bool NowIsEnable()
    {
        if (SetShootDelegat != null)
        {
            CanWork = true;
        }
        else CanWork = false;

        return CanWork;
    }

    private void ShootWeapon()
    {
        if(!GameObjectForRay || !Muzzle || !Bullet || !Collet || !ColletPoint || !WeaponShoop)
        {
            Debug.Log("GameObjectForRay is: " + GameObjectForRay);
            Debug.Log("Muzzle is: " + Muzzle);
            Debug.Log("Collet is: " + Collet);
            Debug.Log("ColletPoint is: " + ColletPoint);
            Debug.Log("WeaponShoop is: " + WeaponShoop);

            return;
        }

        if (WeaponShoop.CurrentAmmo > 0 && Time.time > ShotTime)
        {
            ShotTime = Time.time + ShotDeley;

            //All References
            GameObject NewBullet = Instantiate(Bullet);
            GameObject NewCollet = Instantiate(Collet);
            
            Rigidbody NewBulletRIG = NewBullet.GetComponent<Rigidbody>();
            Rigidbody NewColletRIG = NewCollet.GetComponent<Rigidbody>();

            //Rigidbody
            NewBulletRIG.useGravity = false;
            NewBulletRIG.isKinematic = false;

            NewColletRIG.useGravity = true;
            NewColletRIG.isKinematic = true;// !!!
            
            //Transform
            NewBullet.transform.position = Muzzle.transform.position;
            NewBullet.transform.forward = Muzzle.transform.forward;
            NewBullet.transform.eulerAngles = ChangeBulletDirection(NewBullet.transform.eulerAngles);

            NewCollet.transform.position = ColletPoint.transform.position;
            NewCollet.transform.forward = ColletPoint.transform.forward;

            //Shoot
            NewBulletRIG.AddForce(NewBullet.transform.forward * BulletSpeed, ForceMode.Force);

            Destroy(NewBullet, 10.0f);
            //Destroy(NewCollet, 10.0f);
        }
        else Debug.Log("Cannot shoot beacuse low ammo");


    }

    private Vector3 ChangeBulletDirection(Vector3 CurrentDirection)
    {
        CurrentDirection += new Vector3(Random.Range(-ChangedAngle, ChangedAngle), 
        Random.Range(-ChangedAngle, ChangedAngle), 
        Random.Range(-ChangedAngle, ChangedAngle));

        return CurrentDirection;
    }

}