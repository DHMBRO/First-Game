using System.Collections.Generic;
using UnityEngine;


public class ShootControler : MonoBehaviour
{
    public WeaponIsShoting SetShootDelegat;
    private bool CanWork;

    [SerializeField] public Transform SlotForUseShop;
    [SerializeField] public ShopControler WeaponShoop;
    [SerializeField] private GameObject GameObjectForRay;
    [SerializeField] public GameObject Muzzle;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Collet;
    [SerializeField] private GameObject ColletPoint;

    public TypeWeapon TheGun;
    public TypeCaliber CaliberToWeapon;
    public StateWeapon Weapon;

    [SerializeField] public Vector3 ShoulderOffSet;
    [SerializeField] public float ShotDeley = 1.0f;
    [SerializeField] public float ShotTime = 0.0f;
    [SerializeField] public float Mass = 0.0f;
    [SerializeField] private float BulletSpeed = 1.0f;
    [SerializeField] private float ColletSpeed = 1.0f;
    [SerializeField] private float ChangedBulletAngle = 0.0f;

    public bool UnLimitedAmmo;
    //[SerializeField] List<Rigidbody> ColletsRig = new List<Rigidbody>();

    //[SerializeField] private float ColletSpeed = 0.0f;


    void Start()
    {
        if (!Muzzle) Debug.Log("Not set Muzzle");
        if (!Bullet) Debug.Log("Not set Bullet");

        SetShootDelegat += Shoot;
    }

    private void Update()
    {
        Debug.DrawRay(GameObjectForRay.transform.position, GameObjectForRay.transform.forward * 100.0f, Color.yellow);

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
    
    public Transform WeaponMuzzle()
    {
        return Muzzle.transform;
    } 
    

    private void Shoot()//ColectPoint
    {
        //Chek referece
        if ((!GameObjectForRay || !Muzzle || !Bullet || !Collet || !ColletPoint))
        {
            Debug.Log(GameObjectForRay);
            Debug.Log(Muzzle);
            Debug.Log(Bullet);
            Debug.Log(Collet);
            Debug.Log(ColletPoint);

            return;
        }
        
        //Chek that can work
        if (Time.time < ShotTime)
        {
            if(ShotTime == 0.0f) ShotTime = ShotDeley + Time.time;
            return;
        }
        else ShotTime = ShotDeley + Time.time;

        if (WeaponShoop && WeaponShoop.CurrentAmmo > 0)
        {
            if (WeaponShoop.CurrentAmmo > 0) WeaponShoop.CurrentAmmo--;
        }
        else if(!UnLimitedAmmo) return;

        // Instance reference
        GameObject NewBullet = Instantiate(Bullet);
        GameObject NewCollet = Instantiate(Collet);

        Rigidbody NewBulletRIG = NewBullet.GetComponent<Rigidbody>();
        Rigidbody NewColletRIG = NewCollet.GetComponent<Rigidbody>();

        //ColletsRig.Add(NewColletRIG);

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
        NewColletRIG.AddForce((NewCollet.transform.right + (NewCollet.transform.up / 2.0f)) * ColletSpeed, ForceMode.Force);

        NewColletRIG.AddTorque(NewCollet.transform.right * 1000.0f);
        
        Destroy(NewBullet, 10.0f);
        Destroy(NewCollet, 3.0f);
         
    }



    private Vector3 ChangedDirection(Vector3 CurrentDirection)
    {
        CurrentDirection += new Vector3(Random.Range(-ChangedBulletAngle, ChangedBulletAngle), 
        Random.Range(-ChangedBulletAngle, ChangedBulletAngle), 
        Random.Range(-ChangedBulletAngle, ChangedBulletAngle));

        return CurrentDirection;
    }

}