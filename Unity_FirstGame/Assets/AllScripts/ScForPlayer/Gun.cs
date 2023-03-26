using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform Weapon_Transform;
    [SerializeField] private Transform Muzzle_Transform;
    [SerializeField] private Transform Camera_Transform;
    [SerializeField] private float Bullet_Speed = 20.0f;

    [SerializeField] public float ShotDeley = 1.0f;
    [SerializeField] public float ShotTime = 0.0f;
    void Start()
    {
    }

    void Update()
    {
        Vector3 Target_Point = Camera_Transform.position + Camera_Transform.forward * 100.0f;
        
        RaycastHit Hit_Result;
        
        if (Physics.Raycast(Camera_Transform.position, Camera_Transform.forward, out Hit_Result, 100.0f))
        {
            Target_Point = Hit_Result.point;
        }
        
        Quaternion RotationToTarget = Quaternion.LookRotation(Target_Point - Weapon_Transform.position);
        
        Weapon_Transform.rotation = Quaternion.Lerp(Weapon_Transform.rotation, RotationToTarget, 0.05f);

        bool audit01 = Input.GetKey(KeyCode.Mouse0);

            if (audit01 && Time.time >= ShotTime)
            {

                ShotTime = ShotDeley + Time.time;

                Debug.DrawLine(Camera_Transform.position, Camera_Transform.position + Camera_Transform.forward * 10.0f, Color.blue, 5.0f);

                GameObject new_Bullet = Instantiate(Bullet, Muzzle_Transform.position, Quaternion.LookRotation(Target_Point - Muzzle_Transform.position));
                Destroy(new_Bullet, 5.0f);

                Rigidbody new_BulletRB = new_Bullet.GetComponent<Rigidbody>();
                new_BulletRB.AddForce(new_Bullet.transform.forward * Bullet_Speed, ForceMode.Impulse);
            }
        }
    
}
