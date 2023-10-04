using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{


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

   
    void Update()
    {
    }
    public void InstansteKillMe()
    {
        ZomblieHpScript.InstanceKill();                                                                    
    }
    public bool IsObjectFromBehinde(GameObject Object) 
    {
       return ZombleLocateScript.IsObjectFromBehinde(Object);
    }
    

}
