using UnityEngine;

public class PPOscr : MonoBehaviour
{
    
    [SerializeField] GameObject Target;
    [SerializeField] GameObject BulletOrRocket;
    [SerializeField] GameObject FierFrom;
    
    [SerializeField] HorisontalOrVertical RotateTo;
    
    [SerializeField] float RSpeed;
    [SerializeField] float TimeOfSet;

    float timer = 0.00f;
    float bulletSpeed = 0;

    private PPOSettings SettingsPPOScr;
    private Quaternion ToTargetRotation;

    enum HorisontalOrVertical
    {
        Horisontal,
        Vertical
    }
    
    private void Start()
    {
        SettingsPPOScr = GetComponentInParent<PPOSettings>();  
    }

    void Update()
    {
        if (SettingsPPOScr && SettingsPPOScr.PPORotationSpeed != 0.0f)
        {
            RSpeed = GetComponentInParent<PPOSettings>().PPORotationSpeed;
        }
        if (SettingsPPOScr && SettingsPPOScr.BulletSpeed != 0.0f)
        {
            bulletSpeed = GetComponentInParent<PPOSettings>().BulletSpeed;
        }
        if (Target && SettingsPPOScr.ReturnPPOIsEnable())
        {
            ToTargetRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, ToTargetRotation, RSpeed);
            if (RotateTo == HorisontalOrVertical.Horisontal)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                if (timer >= TimeOfSet)
                {
                    GameObject bullet = Instantiate(BulletOrRocket, FierFrom.transform.position, gameObject.transform.rotation);
                    Destroy(bullet, 10f);
                    if (bulletSpeed != 0f)
                    {
                        bullet.GetComponent<Fier>().Speed = bulletSpeed;
                    }
                    timer = 0.00f;
                }
                timer += Time.deltaTime;
            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Helicopter - " + other.gameObject.name);
        if (other.gameObject.CompareTag("Helicopter"))
        {
            Target = other.gameObject;
            Debug.Log("Helicopter In");
        }
        else
        {
            if (other.gameObject.name == "Helicopter 01")
            {
                Debug.LogError("No Teg Helicopter");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Helicopter"))
        {
            Target = null;
        }
    }
}
