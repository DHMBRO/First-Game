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
        if (HPScr && Damage > 0.0f)
        {
            Debug.Log(HPScr.gameObject.name + " Bullet Hit");
            HPScr.InflictingDamage(Damage); 
            
        }

        Destroy(gameObject);
        
    }

    void Start()
    {
        Destroy(gameObject,10.0f);
    }
}
