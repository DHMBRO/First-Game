using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        
    }

    void Update()
    {
        Destroy(gameObject, 10.0f);
    }
}
