using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControler : MonoBehaviour
{
    [SerializeField] private Transform SlotForGun;
    [SerializeField] private Transform MyGun;
    [SerializeField] private Gun MyWeaponScript;

    [SerializeField] public bool CanFire;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
        if (CanFire && Input.GetKey("1"))
        {
            CanFire = false;
            MyGun.transform.position = SlotForGun.transform.position;
            MyGun.transform.rotation = SlotForGun.transform.rotation;               
        }
        else if (!CanFire && Input.GetKey("2"))
        {
            CanFire = true;
            //MyGun.transform.position =                      
        }
        
    }
    

    

}
