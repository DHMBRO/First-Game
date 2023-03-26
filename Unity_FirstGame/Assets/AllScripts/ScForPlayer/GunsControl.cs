using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsControl : MonoBehaviour
{
    public bool GunIndackpack = false;
    public GameObject Gun1;
    public Transform Gun1Backpack;
    public Transform GunStandartPos;
    
    void Start()
    {
        
        GameObject gameObject = Gun1;
        
    }

    
    void Update()
    {
        
        if(GunIndackpack == false && Input.GetKeyDown("1"))
        {
            Debug.Log("work");
            Gun1.transform.position = Gun1Backpack.position;
            Gun1.transform.rotation = Gun1Backpack.rotation;

            GunIndackpack = true;
        }
        if (GunIndackpack && Input.GetKeyDown("2"))
        {
            Gun1.transform.position = GunStandartPos.position;
            Gun1.transform.rotation = GunStandartPos.rotation;

            GunIndackpack = false;
        } 
    }
}
