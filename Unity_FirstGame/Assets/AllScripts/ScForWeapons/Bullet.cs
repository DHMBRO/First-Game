using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float BulletDamage;
    
    private void OnCollisionEnter(Collision collision)
    {
        HpScript HPScr = collision.gameObject.GetComponent<HpScript>();
        if (HPScr) GiveDamage(HPScr);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        HpScript HPScr = other.gameObject.GetComponent<HpScript>();
        if (HPScr) GiveDamage(HPScr);
    }

    void GiveDamage(HpScript HPScr)
    {
        if (BulletDamage > 0.0f)
        {
            HPScr.InflictingDamage(BulletDamage);
            Destroy(gameObject);
        }
    }

}
