using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    [SerializeField] protected float ZombieHp = 100;
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("BulletM4"))
        {
            ZombieHp -= 10;
            
        }
    }
    void Update()
    {
        Debug.Log("HP ===" + ZombieHp);
        if (ZombieHp <= 0) 
        {
            Destroy(gameObject);
            
        }
    }
}
