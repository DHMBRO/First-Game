using UnityEngine;

public class PPOscr : MonoBehaviour
{
    private Quaternion ToTargetRotation;
    GameObject Turget;
    bool PPOIsOn = true;
    [SerializeField] float RSpeed;
    [SerializeField] GameObject BulletOrRocket;
    [SerializeField] GameObject FierFrom;
    [SerializeField] float TimeOfSet;
    float timer = 0.00f;
    enum HorisontalOrVertical
    {
        Horisontal,
        Vertical
    }
    [SerializeField] HorisontalOrVertical RotateTo;
    float bulletSpeed = 0;
    // Start is called before the first frame update
    
    
    public void UpdateState(bool NewState)
    {
        PPOIsOn = NewState;        
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (GetComponentInParent<PPOSettings>().PPORotationSpeed != 0f)
        {
            RSpeed = GetComponentInParent<PPOSettings>().PPORotationSpeed;
        }
        if (GetComponentInParent<PPOSettings>().BulletSpeed != 0f)
        {
            bulletSpeed = GetComponentInParent<PPOSettings>().BulletSpeed;
        }
        if (Turget && PPOIsOn)
        {
            ToTargetRotation = Quaternion.LookRotation(Turget.transform.position - transform.position);
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
            Turget = other.gameObject;
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
            Turget = null;
        }
    }
}
