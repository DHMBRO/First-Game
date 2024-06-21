using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletDamage = 0.0f;
    public GameObject LauncherBullet;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collider: " + collision.gameObject.name);

        GetDamageScript GetDamageScr = collision.gameObject.GetComponent<GetDamageScript>();
        if (GetDamageScr)
        {
            GetDamageScr.GetDamage(BulletDamage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger: " + other.name);

        GetDamageScript GetDamageScr = other.gameObject.GetComponent<GetDamageScript>();
        
        if (GetDamageScr) 
        {
            GetDamageScr.GetDamage(BulletDamage);
            Destroy(gameObject);
        }
    }

}
