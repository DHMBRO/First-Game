using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform Weapon_Transform;
    [SerializeField] private Transform Muzzle_Transform;
    [SerializeField] private Transform Camera_Transform;
    [SerializeField] private float Bullet_Speed = 20.0f;
    [SerializeField] private SlotControler MySlotControler;

    [SerializeField] public float ShotDeley = 0.3f;
    [SerializeField] public float ShotTime = 0.0f;

    

    private Vector3 TargetPoint;
    private RaycastHit HitResult;
    private Quaternion RotationToTarget;
    

    void Start()
    {
        MySlotControler = gameObject.GetComponent<SlotControler>();
        
    }

    void Update()
    {
        GuidanceWeapon();
        FireForTarget();

       
    }
    
    void GuidanceWeapon()
    {
        if (MySlotControler.CanFire)
        {
            TargetPoint = Camera_Transform.position + Camera_Transform.forward * 100.0f;
            
            if (Physics.Raycast(Camera_Transform.position, Camera_Transform.forward, out HitResult, 100.0f))
            {
                TargetPoint = HitResult.point;
            }
            
            RotationToTarget = Quaternion.LookRotation(TargetPoint - Weapon_Transform.position);
            Weapon_Transform.rotation = Quaternion.Lerp(Weapon_Transform.rotation, RotationToTarget, 0.05f);
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

                Debug.DrawLine(Camera_Transform.position, Camera_Transform.position + Camera_Transform.forward * 10.0f, Color.blue, 5.0f);

                GameObject new_Bullet = Instantiate(Bullet, Muzzle_Transform.position, Quaternion.LookRotation(TargetPoint - Muzzle_Transform.position));
                
                
                Destroy(new_Bullet, 5.0f);

                Rigidbody new_BulletRB = new_Bullet.GetComponent<Rigidbody>();
                new_BulletRB.AddForce(new_Bullet.transform.forward * Bullet_Speed, ForceMode.Impulse);
            }
        }
        
    }
}
