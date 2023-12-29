using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMethod : MonoBehaviour
{
    protected float ZombieDamage = 5;
    [SerializeField] public float AttackDistance;
    [SerializeField] public float GoingDistance;
    [SerializeField] protected float AttackDelay = 3.0f;
    [SerializeField] protected float AttackTime = 3.5f;
    [SerializeField] public GameObject GunPos;
    [SerializeField] protected GameObject TerroristGun;
    protected ShootControler TerroristWeaponScript;
    protected bool CanAttack;

    protected HpScript TargetHpScript;
    protected LocateScript ZombieLocateScript;
    protected PatrolScriptNavMesh ZombiePatrolScript;
    

    void Start()
    {
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
        ZombieLocateScript = gameObject.GetComponent<LocateScript>();
        if (ZombieLocateScript.Target) TargetHpScript = ZombieLocateScript.Target.GetComponent<HpScript>();
        if (TerroristGun)
        {
            GameObject Gun = Instantiate(TerroristGun, GunPos.transform);
            
            (Gun?.GetComponent<Rigidbody>()).isKinematic = true;
            TerroristWeaponScript = Gun.GetComponentInParent<ShootControler>();
            TerroristWeaponScript.UnLimitedAmmo = true;
        }
    }

    void Update()
    {
        if (ZombieLocateScript.Target) TargetHpScript = ZombieLocateScript.Target.GetComponent<HpScript>();
    }

    public void Attack(GameObject Target)
    {  
        if (TerroristWeaponScript)
        {
            GunPos.transform.LookAt(Target.transform);
            TerroristWeaponScript.Shoot();

        }
        else 
        {
            
            
            if (Time.time >= AttackTime)
            {
                HpScript TargetHpScript = Target.GetComponentInParent<HpScript>();
                if (TargetHpScript)
                {
                    TargetHpScript.InflictingDamage(ZombieDamage);
                    ZombieLocateScript.DefineMyTarget();
                }

                AttackTime = AttackDelay + Time.time;
            }
        }  
    }
}


    

