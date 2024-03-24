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

        SetShootDelegat += Shoot;
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
    


    private void Shoot()//ColectPoint
    {
        //Chek referece
        if (!GameObjectForRay || !Muzzle || !Bullet || !Collet || !ColletPoint || !WeaponShoop)
        {
            Debug.Log(GameObjectForRay);
            Debug.Log(Muzzle);
            Debug.Log(Bullet);
            Debug.Log(Collet);
            Debug.Log(ColletPoint);
            Debug.Log(WeaponShoop);

            return;
        }

        //Chek that can work
        if (Time.time < ShotTime)
        {
            if(ShotTime == 0.0f) ShotTime = ShotDeley + Time.time;
            return;
        }
        else ShotTime = ShotDeley + Time.time;

        if (WeaponShoop.CurrentAmmo > 0 || UnLimitedAmmo)
        {
            if (WeaponShoop.CurrentAmmo > 0) WeaponShoop.CurrentAmmo--;
        }
        else return;

        // Instance reference
        GameObject NewBullet = Instantiate(Bullet);
        GameObject NewCollet = Instantiate(Collet);

        Rigidbody NewBulletRIG = NewBullet.GetComponent<Rigidbody>();
        Rigidbody NewColletRIG = NewCollet.GetComponent<Rigidbody>();

        if (!NewBulletRIG) NewBulletRIG = NewBullet.AddComponent<Rigidbody>();
        if (!NewColletRIG) NewColletRIG = NewCollet.AddComponent<Rigidbody>();

        // Setup reference
        NewBullet.transform.position = Muzzle.transform.position;
        NewBullet.transform.eulerAngles = ChangedDirection(Muzzle.transform.eulerAngles); 

        NewCollet.transform.position = ColletPoint.transform.position;
        NewCollet.transform.eulerAngles = ColletPoint.transform.eulerAngles;

        // Implemetation

        NewBulletRIG.useGravity = false;
        NewBulletRIG.AddForce(NewBullet.transform.forward * BulletSpeed, ForceMode.Force);
        
        Debug.DrawRay(Muzzle.transform.position, Muzzle.transform.forward, Color.blue);


        Destroy(NewBullet, 10.0f);
        Destroy(NewCollet, 10.0f);

        /*
        if (!WeaponShoop && !UnLimitedAmmo)
        {
            //Debug.Log("Shoot2");
            return;
        }
        ShopControler Shop = null;
        if (!UnLimitedAmmo)
        {
            Shop = WeaponShoop.gameObject.GetComponent<ShopControler>();
            if (Shop?.CurrentAmmo <= 0)
            {; return; }
        }
        Vector3 TargetPoint = GameObjectForRay.transform.position + GameObjectForRay.transform.forward * 100.0f;
        RaycastHit Hitresult;

        if (Physics.Raycast(GameObjectForRay.transform.position, GameObjectForRay.transform.forward, out Hitresult))
        {
            Debug.DrawRay(Muzzle.transform.position, Muzzle.transform.forward * 100.0f, Color.blue);
            TargetPoint = Hitresult.point;
        }
        if (Time.time >= ShotTime)
        {
            ShotTime = ShotDeley + Time.time;
            GameObject newBullet = Instantiate(Bullet, Muzzle.transform.position, Quaternion.LookRotation(TargetPoint - GameObjectForRay.transform.position));

            Vector3 ShootDirection = Muzzle.transform.forward;
            newBullet.transform.forward = ChangeDirection(ShootDirection);

            Rigidbody newBulletRB = newBullet.GetComponent<Rigidbody>();
            if (!newBulletRB) newBulletRB.gameObject.AddComponent<Rigidbody>();
            if (newBulletRB) newBulletRB.AddForce(newBulletRB.transform.forward * BulletSpeed, ForceMode.Impulse); // Dont touch this !!!

            GameObject newCollet = Instantiate(Collet, ColletPoint.transform.position, Quaternion.LookRotation(TargetPoint - Muzzle.transform.position));
            newCollet.transform.rotation = ColletPoint.transform.rotation;

            Rigidbody newColletRB = newCollet.GetComponent<Rigidbody>();                    

            //newColletRB.AddRelativeForce(ColletPoint.transform.forward * ColletSpeed, ForceMode.Impulse);
            Destroy(newCollet, 2.5f);

            if (Shop)
            {
                Shop.CurrentAmmo--;
            }
        }
        */

    }

    private Vector3 ChangedDirection(Vector3 CurrentDirection)
    {
        CurrentDirection += new Vector3(Random.Range(-ChangedAngle, ChangedAngle), 
        Random.Range(-ChangedAngle, ChangedAngle), 
        Random.Range(-ChangedAngle, ChangedAngle));

        return CurrentDirection;
    }

}