using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject ParentObject;
    [SerializeField] float Damage;
    //GameObject audio;
    
    private void OnCollisionEnter(Collision collision)
    {
        HpScript HPScr = collision.gameObject.GetComponent<HpScript>();

        if (HPScr)
        {
            GiveDamage(HPScr);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        HpScript HPScr = other.gameObject.GetComponent<HpScript>();

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

        Destroy(ParentObject);

    }

    
}
