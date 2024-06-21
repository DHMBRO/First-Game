using System.Collections.Generic;
using UnityEngine;


public class ShootControler : MonoBehaviour
{
    public WeaponIsShoting SetShootDelegat;
    public bool UseShoulderOffSet = true;
    private bool CanWork;

    [SerializeField] public Transform SlotForUseShop;
    [SerializeField] public ShopControler WeaponShoop;
    [SerializeField] private GameObject GameObjectForRay;
    [SerializeField] public GameObject Muzzle;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject Collet;
    [SerializeField] private GameObject ColletPoint;
    [SerializeField] public Transform LeftArmPositionIK;

    public TypeWeapon TheGun;
    public TypeCaliber CaliberToWeapon;
    public StateWeapon Weapon;

    [SerializeField] public Vector3 HandOffSet;
    [SerializeField] public Vector3 ShoulderOffSet;
    [SerializeField] public float ShotDeley = 1.0f;
    [SerializeField] public float ShotTime = 0.0f;
    [SerializeField] float BulletPrefabDmage = 1.0f;
    [SerializeField] public float Mass = 0.0f;
    [SerializeField] private float BulletPrefabSpeed = 1.0f;
    [SerializeField] private float ColletSpeed = 1.0f;
    [SerializeField] private float ChangedBulletPrefabAngle = 0.0f;

    public bool UnLimitedAmmo;
    //[SerializeField] List<Rigidbody> ColletsRig = new List<Rigidbody>();

    //[SerializeField] private float ColletSpeed = 0.0f;


    void Start()
    {
        if (!Muzzle) Debug.Log("Not set Muzzle");
        if (!BulletPrefab) Debug.Log("Not set BulletPrefab");

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
        if ((!GameObjectForRay || !Muzzle || !BulletPrefab || !Collet || !ColletPoint))
        {
            Debug.Log(GameObjectForRay);
            Debug.Log(Muzzle);
            Debug.Log(BulletPrefab);
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
        GameObject NewBulletPrefab = Instantiate(BulletPrefab);
        GameObject NewCollet = Instantiate(Collet);

        Rigidbody NewBulletPrefabRIG = NewBulletPrefab.GetComponent<Rigidbody>();
        Rigidbody NewColletRIG = NewCollet.GetComponent<Rigidbody>();

        Bullet NewBulletScr = NewBulletPrefab.GetComponent<Bullet>();

        //ColletsRig.Add(NewColletRIG);

        if (!NewBulletPrefabRIG) NewBulletPrefabRIG = NewBulletPrefab.AddComponent<Rigidbody>();
        if (!NewColletRIG) NewColletRIG = NewCollet.AddComponent<Rigidbody>();

        // Setup reference
        NewBulletPrefab.transform.position = Muzzle.transform.position;
        NewBulletPrefab.transform.eulerAngles = ChangedDirection(Muzzle.transform.eulerAngles); 

        NewCollet.transform.position = ColletPoint.transform.position;
        NewCollet.transform.eulerAngles = ColletPoint.transform.eulerAngles;

        NewBulletScr.BulletDamage = BulletPrefabDmage;

        if (gameObject.GetComponentInParent<PlayerControler>())
        {
            NewBulletScr.LauncherBullet = gameObject.GetComponentInParent<PlayerControler>().gameObject;

        }

        // Realization

        NewBulletPrefabRIG.AddForce(NewBulletPrefab.transform.forward * BulletPrefabSpeed, ForceMode.Impulse);
        NewColletRIG.AddForce((NewCollet.transform.right + (NewCollet.transform.up / 2.0f)) * ColletSpeed, ForceMode.Impulse);

        NewColletRIG.AddTorque(NewCollet.transform.right * 1000.0f);
        
        Destroy(NewBulletPrefab, 10.0f);
        Destroy(NewCollet, 3.0f);
         
    }



    private Vector3 ChangedDirection(Vector3 CurrentDirection)
    {
        CurrentDirection += new Vector3(Random.Range(-ChangedBulletPrefabAngle, ChangedBulletPrefabAngle), 
        Random.Range(-ChangedBulletPrefabAngle, ChangedBulletPrefabAngle), 
        Random.Range(-ChangedBulletPrefabAngle, ChangedBulletPrefabAngle));

        return CurrentDirection;
    }

}