using UnityEngine;

public class M4ControlerForFire : MonoBehaviour
{
    [SerializeField] private Transform MyWeapon;
    [SerializeField] private Transform Muzzle;

    //[SerializeField] private float Sens = 0.5f;

    [SerializeField] public float ShotDeley = 0.3f;
    [SerializeField] public float ShotTime = 0.0f;

    [SerializeField] private Rigidbody WeaonRigidbody;
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private SlotControler MySlotControler;


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

    void FireForTarget()
    {        
        bool audit01 = Input.GetKey(KeyCode.Mouse0);

        //if (MySlotControler)
        {            
            if (audit01 && Time.time >= ShotTime)
            {
                ShotTime = ShotDeley + Time.time;

                

                
                
                
            }
        }
        
    }
}
