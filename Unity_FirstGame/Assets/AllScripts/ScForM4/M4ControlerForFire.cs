using UnityEngine;

public class M4ControlerForFire : MonoBehaviour
{
    [SerializeField] private Transform MyWeapon;
    [SerializeField] private Transform Muzzle;
    [SerializeField] private GameObject Bullet;
    //[SerializeField] private float Sens = 0.5f;

    [SerializeField] public float ShotDeley = 0.3f;
    [SerializeField] public float ShotTime = 0.0f;

    [SerializeField] private Rigidbody WeaonRigidbody;
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private SlotControler MySlotControler;
    private Transform ShootPoint;
    private float BulletSpeed = 10;
    void Start()
    {
        MySlotControler = gameObject.GetComponent<SlotControler>();
        WeaonRigidbody = gameObject.GetComponent<Rigidbody>();
        MyWeapon = gameObject.GetComponent<Transform>();
    }

    void Update()
    {


        if (gameObject.transform.parent && CameraTransform && gameObject.transform.parent.CompareTag("SlotForUse"))
        {
            float MouseX = Input.GetAxis("Mouse X");
            float MouseY = Input.GetAxis("Mouse Y");

            //gameObject.transform.Rotate(-MouseY * new Vector3(Sens, 0.0f, 0.0f));
            //transform.Rotate(MouseX * new Vector3(0.0f, Sens, 0.0f));

            transform.rotation = CameraTransform.rotation;
            Debug.Log("1");
        }

    }

    void GuidanceWeapon()
    {



    }
    void Shoot()
    {
        Vector3 TargetPoint = CameraTransform.position + CameraTransform.forward * 100.0f;
        RaycastHit Hitresult;
        if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out Hitresult))
        {

            TargetPoint = Hitresult.point;
        }
        if ()
        {
            GameObject newBullet = Instantiate(Bullet, Muzzle.position, Quaternion.LookRotation(TargetPoint - Muzzle.position));


            Destroy(newBullet, 5.0f);




            Rigidbody newBulletRB = newBullet.GetComponent<Rigidbody>();
            newBulletRB.AddForce(newBullet.transform.forward * BulletSpeed, ForceMode.Impulse);
        }

    }
    void FireForTarget()
    {
        bool audit01 = Input.GetKey(KeyCode.Mouse0);

        if (MySlotControler.CanFire)
        {
            if (audit01 && Time.time >= ShotTime)
            {
                ShotTime = ShotDeley + Time.time;






            }
        }

    }
}