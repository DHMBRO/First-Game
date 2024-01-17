using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject audioToMetalic;
    [SerializeField] float Damage;
    //GameObject audio;
    
    private void OnCollisionEnter(Collision collision)
    {
        //audio = GameObject.Instantiate(audioToMetalic);
        HpScript HPScr = collision.gameObject.GetComponent<HpScript>();


        //Debug.Log("Bullet Collide with " + collision.gameObject.name);

        if (HPScr)
        {
            GiveDamage(HPScr);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        HpScript HPScr = other.gameObject.GetComponent<HpScript>();


        //Debug.Log("Bullet Collide with " + other.gameObject.name);

        if (HPScr)
        {
            GiveDamage(HPScr);
        }
    }

    void GiveDamage(HpScript HPScr)
    {
        if (Damage > 0.0f)
        {
            
            HPScr.InflictingDamage(Damage);
            Debug.Log(HPScr.gameObject.name + " Bullet Hit " + HPScr.HealthPoint);
        }

        Destroy(gameObject);

    }

    void Start()
    {
        Destroy(gameObject,10.0f);
    }
}
