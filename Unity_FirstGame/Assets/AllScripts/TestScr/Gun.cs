using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    
    [SerializeField] private Transform Target;

    [SerializeField] private Transform Weapon;
    [SerializeField] private Transform Muzzle;
    [SerializeField] private Transform Bullet;

    [SerializeField] private float SpeedForBullet;


    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RotateWeapon(Target, Weapon, Muzzle);
        }
    }

    void RotateWeapon(Transform TTarget,Transform TWeapon, Transform Tmuzzle)
    {
        //while (true)
        {
            TWeapon.LookAt(TTarget);
            Shot(Tmuzzle);
        }
        
        
    }

    void Shot(Transform TMuzzle)
    {
        GameObject Bullet01 = Instantiate(Bullet.gameObject);
        
        Bullet01.transform.position = TMuzzle.transform.position;
        Bullet01.transform.rotation = TMuzzle.transform.rotation;

        Vector3 MoveToTarget = new Vector3(0.0f, 0.0f, 0.0f + 1.0f * SpeedForBullet);
        
        Rigidbody RigBulet = Bullet01.GetComponent<Rigidbody>();

        RigBulet.AddRelativeForce(MoveToTarget, ForceMode.Impulse);

    }
    
}
