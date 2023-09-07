using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    [SerializeField] protected float ZombieHp = 100;
    [SerializeField] public MeshRenderer ZombieMeshRenderer;
    [SerializeField] public Material ZombieDeadMaterial;
    ZombieController ZombieControllerScript;
    HpScript ZomblieHpScript;
    LocateScript ZombleLocateScript;
    void Start()
    {
        ZombleLocateScript = GetComponent<LocateScript>();
        ZomblieHpScript = gameObject.GetComponent<HpScript>();
        ZombieControllerScript = GetComponentInParent<ZombieController>();
        
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
        if (ZombieHp < 100)
        {
            ZombieControllerScript.IsLive = false;
        }
        if (!ZombieControllerScript.IsLive)
        {
            ZombieMeshRenderer.material = ZombieDeadMaterial;
        }

       
    }
    public void InstansteKillMe()
    {
       
      ZomblieHpScript.InflictingDamage(ZombieHp);                                                                     

    }
    public bool IsObjectFromBehinde(GameObject Object) 
    {


       return ZombleLocateScript.IsObjectFromBehinde(Object);
    }


}
