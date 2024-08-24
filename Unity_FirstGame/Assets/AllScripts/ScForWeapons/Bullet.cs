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
        GetDamageScript GetDamageScr = collision.gameObject.GetComponent<GetDamageScript>();
        if (GetDamageScr) GetDamageScr.GetDamage(BulletDamage, CaliberIs);
        Destroy(gameObject);
        
    }

}
