using UnityEngine;

public class M4ControlerForFire : MonoBehaviour
{
    [SerializeField] private GameObject MyWeapon;
    [SerializeField] private GameObject Muzzle;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Collet;
    [SerializeField] private GameObject ColletPoint;
    //[SerializeField] private float Sens = 0.5f;

    [SerializeField] public float ShotDeley = 1.0f;
    [SerializeField] public float ShotTime = 0.0f;

    [SerializeField] private Rigidbody WeaonRigidbody;
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private SlotControler MySlotControler;
    private float ColletSpeed = 3.0f;
    private Transform ShootPoint;
    private float BulletSpeed = 100;
    void Start()
    {

        MySlotControler = gameObject.GetComponent<SlotControler>();
        WeaonRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {

        Debug.Log(gameObject.transform.parent && CameraTransform && gameObject.transform.parent.CompareTag("SlotForUse"));
        if (gameObject.transform.parent && CameraTransform && gameObject.transform.parent.CompareTag("SlotForUse"))
        {
            Debug.Log("work");
            float MouseX = Input.GetAxis("Mouse X");
            float MouseY = Input.GetAxis("Mouse Y");

            //gameObject.transform.Rotate(-MouseY * new Vector3(Sens, 0.0f, 0.0f));
            //transform.Rotate(MouseX * new Vector3(0.0f, Sens, 0.0f));

            /* transform.rotation = CameraTransform.rotation;*/
            if (MyWeapon && Muzzle && Bullet)
            {
                GameObject(gameObject, Muzzle, ColletPoint, Collet, Bullet);
                Debug.Log("I can fire");
            }
            else if (!MyWeapon)
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
        
    }

    void GuidanceWeapon()
    {



    }
    GameObject GameObject(GameObject Weapon, GameObject Muzzle, GameObject ColletPoint, GameObject Collet, GameObject Bullet)
    {
        Debug.Log("Is work ");
        Vector3 TargetPoint = CameraTransform.position + CameraTransform.forward * 100.0f;
        RaycastHit Hitresult;
        if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out Hitresult))
        {
            Debug.DrawRay(CameraTransform.transform.position, CameraTransform.transform.forward * 100.0f, Color.black);
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
        return Bullet;
    }
    void FireForTarget()
    {
        bool audit01 = Input.GetKey(KeyCode.Mouse0);

        if (MySlotControler)
        {
            if (audit01 && Time.time >= ShotTime)
            {
                ShotTime = ShotDeley + Time.time;






            }
        }

    }
}