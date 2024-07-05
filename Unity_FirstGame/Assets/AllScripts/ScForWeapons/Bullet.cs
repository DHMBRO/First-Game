using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float BulletDamage = 0.0f;
    [SerializeField] private TypeCaliber CaliberIs;
    [SerializeField] public GameObject LauncherBullet;

    public void GetNewBulletDamage(float NewDamage) 
    {
        BulletDamage = NewDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collider: " + collision.gameObject.name);

        GetDamageScript GetDamageScr = collision.gameObject.GetComponent<GetDamageScript>();
        if (GetDamageScr) GetDamageScr.GetDamage(BulletDamage, CaliberIs);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger: " + other.name);

        GetDamageScript GetDamageScr = other.gameObject.GetComponent<GetDamageScript>();
        
        if (GetDamageScr) 
        {
            GetDamageScr.GetDamage(BulletDamage, CaliberIs);
            Destroy(gameObject);
        }
    }

}
