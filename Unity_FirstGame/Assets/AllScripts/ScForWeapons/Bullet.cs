using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject audioToMetalic;
    [SerializeField] float Damage;
    //GameObject audio;
    
    private void OnCollisionEnter(Collision collision)
    {
        //audio = GameObject.Instantiate(audioToMetalic);
        Debug.Log("Bullet Collide with " + collision.gameObject.name);
        HpScript HPScr = collision.gameObject.GetComponent<HpScript>();
        if (HPScr && Damage > 0.0f)
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
