using UnityEngine;

public class M4ControlerForFire : MonoBehaviour
{
    [SerializeField] private Transform MyWeapon;
    [SerializeField] private Transform Muzzle;

    [SerializeField] private float SensY = 0.3f;

    [SerializeField] public float ShotDeley = 0.3f;
    [SerializeField] public float ShotTime = 0.0f;

    [SerializeField] private Rigidbody WeaonRigidbody;

    [SerializeField] private SlotControler MySlotControler;

    void Start()
    {
        MySlotControler = gameObject.GetComponent<SlotControler>();
        WeaonRigidbody = gameObject.GetComponent<Rigidbody>();
        MyWeapon = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        float MouseY = Input.GetAxis("Mouse Y");

        if (gameObject.transform.parent && gameObject.transform.parent.CompareTag("SlotForUse"))
        {            
            if (MouseY > 0.0f )
            {
                Vector3 Cursor = new Vector3(0.0f, 0.0f, 0.0f + MouseY * SensY);
                transform.Rotate(Cursor);

            }
            if (MouseY < 0.0f)
            {
                Vector3 Cursor = new Vector3(0.0f, 0.0f, 0.0f - MouseY * SensY);
                transform.Rotate(Cursor);

            }
        }
            
    }
    
    void GuidanceWeapon()
    {
        
        
        
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
