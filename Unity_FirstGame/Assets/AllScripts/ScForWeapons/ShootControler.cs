using UnityEngine;

public class ShootControler : MonoBehaviour
{
    [SerializeField] private GameObject MyWeapon;
    //[SerializeField] private BoxCollider ColiderForWeapon;

    [SerializeField] public Transform SlotForUseShop;
    [SerializeField] public GameObject WeaponShoop;
    [SerializeField] private GameObject GameObjectForRay;
    [SerializeField] private GameObject Muzzle;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Collet;
    [SerializeField] private GameObject ColletPoint;

    [SerializeField] public float ShotDeley = 1.0f;
    [SerializeField] public float ShotTime = 0.0f;
    [SerializeField] public float Mass = 0.0f;
    [SerializeField] public float SpeedForBullet = 0.0f;

    //[SerializeField] private Rigidbody WeaponRigidbody;
    [SerializeField] public Transform CameraTransform;
    [SerializeField] private SlotControler MySlotControler;


    [SerializeField] private float ColletSpeed = 0.0f;
    private Transform ShootPoint;
    [SerializeField] private float BulletSpeed = 90;
    private string NameForWeapon;

    [SerializeField] private bool InInvetory;
    [SerializeField] private bool IsUsing;

    void Start()
    {
        MyWeapon = gameObject;
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
        
    private void Update()
    {
        
        if (gameObject.transform.parent && gameObject.transform.parent.tag == "SlotForUse")
        {
            IsUsing = true;
            InInvetory = true;

            if (Input.GetKey(KeyCode.Mouse0)) 
            {
                ShootForM4();                
            }
        }
        
        if (transform.parent && transform.parent.tag == "UnloadingSlot")
        {
            InInvetory = true;
            IsUsing = false;
        }

    }
    
    public void ShootForM4()
    {
        bool Audit01 = MyWeapon && GameObjectForRay && Muzzle && Bullet && Collet && ColletPoint;  
        if(gameObject.tag == "M4" && Audit01)
        {
            Shoot(Muzzle, GameObjectForRay, ColletPoint, Collet, Bullet);
        }
        else if(gameObject.tag == "AK47" && Audit01)
        {
            Shoot(Muzzle, GameObjectForRay, ColletPoint, Collet, Bullet);
        }
        else if(gameObject.tag == "Glok" && Audit01)
        {
            Shoot(Muzzle, GameObjectForRay, ColletPoint, Collet, Bullet);
        }
        else if(gameObject.tag == "M1911" && Audit01)
        {
            Shoot(Muzzle, GameObjectForRay, ColletPoint, Collet, Bullet);
        }        
    }

    

    void Shoot(GameObject Muzzle, GameObject GameObjectForRay, GameObject ColletPoint, GameObject Collet, GameObject Bullet)//ColectPoint
    {
        if (WeaponShoop)
        {
            ShopControler Shop = WeaponShoop.gameObject.GetComponent<ShopControler>();
            if (Shop.CurrentAmmo > 0)
            {
                Vector3 TargetPoint = GameObjectForRay.transform.position + GameObjectForRay.transform.forward * 100.0f;
                RaycastHit Hitresult;

                if (Physics.Raycast(GameObjectForRay.transform.position, GameObjectForRay.transform.forward, out Hitresult))
                {
                    Debug.DrawRay(GameObjectForRay.transform.position, GameObjectForRay.transform.forward * 100.0f, Color.blue);
                    TargetPoint = Hitresult.point;
                }
                if (Input.GetKey(KeyCode.Mouse0) && Time.time >= ShotTime)
                {
                    ShotTime = ShotDeley + Time.time;
                    GameObject newBullet = Instantiate(Bullet, Muzzle.transform.position, Quaternion.LookRotation(TargetPoint - GameObjectForRay.transform.position));
                    newBullet.transform.rotation = Muzzle.transform.rotation;

                    Rigidbody newBulletRB = newBullet.GetComponent<Rigidbody>();
                    newBulletRB.AddForce(GameObjectForRay.transform.forward * BulletSpeed, ForceMode.Impulse); // Dont touch this !!!

                    GameObject newCollet = Instantiate(Collet, ColletPoint.transform.position, Quaternion.LookRotation(TargetPoint - Muzzle.transform.position));
                    newCollet.transform.rotation = ColletPoint.transform.rotation;
                    
                    Rigidbody newColletRB = newCollet.GetComponent<Rigidbody>();                    
                    
                    newColletRB.AddRelativeForce(ColletPoint.transform.forward * ColletSpeed, ForceMode.Impulse);
                    Destroy(newCollet, 2.5f);
                    
                    Shop.CurrentAmmo--;
                    
                }
            }
        }        
        
        
        
    }
}