using UnityEngine;

public class ShootControler : MonoBehaviour
{
    //[SerializeField] private BoxCollider ColiderForWeapon;

    [SerializeField] public Transform SlotForUseShop;
    [SerializeField] public GameObject WeaponShoop;
    [SerializeField] private GameObject GameObjectForRay;
    [SerializeField] private GameObject Muzzle;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Collet;
    [SerializeField] private GameObject ColletPoint;

    public TypeWeapon TheGun;
    public TypeCaliber CaliberToWeapon;

    [SerializeField] public float ShotDeley = 1.0f;
    [SerializeField] public float ShotTime = 0.0f;
    [SerializeField] public float Mass = 0.0f;
    [SerializeField] private float BulletSpeed = 0.0f;
    public bool UnLimitedAmmo;
    //[SerializeField] private float ColletSpeed = 0.0f;


    void Start()
    {
        if (!Muzzle) Debug.Log("Not set Muzzle");
        if (!Bullet) Debug.Log("Not set Bullet");
    }

    public void ShootWithPoint(Ray ForwardCamera)
    {
        //if ()
        {

        }

    }


    public void Shoot()//ColectPoint
    {
        if (!GameObjectForRay || !Muzzle || !Bullet || !Collet || !ColletPoint )
        {
           
            return;
        }

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
            newBullet.transform.rotation = Muzzle.transform.rotation;

            Rigidbody newBulletRB = newBullet.GetComponent<Rigidbody>();
            if (!newBulletRB) newBulletRB.gameObject.AddComponent<Rigidbody>();
            if (newBulletRB) newBulletRB.AddForce(Muzzle.transform.forward * BulletSpeed, ForceMode.Impulse); // Dont touch this !!!

            GameObject newCollet = Instantiate(Collet, ColletPoint.transform.position, Quaternion.LookRotation(TargetPoint - Muzzle.transform.position));
            newCollet.transform.rotation = ColletPoint.transform.rotation;

            //Rigidbody newColletRB = newCollet.GetComponent<Rigidbody>();                    

            //newColletRB.AddRelativeForce(ColletPoint.transform.forward * ColletSpeed, ForceMode.Impulse);
            Destroy(newCollet, 2.5f);
            if (Shop)
            {
                Shop.CurrentAmmo--;
            }
            

        }





    }
}