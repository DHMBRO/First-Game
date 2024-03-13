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
    [SerializeField] public GameObject TerroristGun;
    public ShootControler TerroristWeaponScript;
    protected bool CanAttack;
    [SerializeField] Animator MyAnimator;
    protected HpScript TargetHpScript;
    protected LocateScript ZombieLocateScript;
    protected PatrolScriptNavMesh ZombiePatrolScript;
    

    void Start()
    {
        MyAnimator = gameObject.GetComponentInChildren<Animator>();
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
        ZombieLocateScript = gameObject.GetComponent<LocateScript>();
        if (ZombieLocateScript.Target) TargetHpScript = ZombieLocateScript.Target.GetComponent<HpScript>();
        if (TerroristGun)
        {
            GameObject Gun = Instantiate(TerroristGun, GunPos.transform.position, Quaternion.identity, GunPos.transform);
            Gun.transform.localEulerAngles = Vector3.zero;
            (Gun?.GetComponent<Rigidbody>()).isKinematic = true;
            TerroristWeaponScript = Gun.GetComponentInParent<ShootControler>();
            TerroristWeaponScript.UnLimitedAmmo = true;
        }
    }

    void Update()
    {
       
    }

    public void StartAttack(GameObject Target)
    {  
        if (TerroristWeaponScript)
        {
            //GunPos.transform.LookAt(Target.transform);
            TerroristWeaponScript.Shoot();
            //gameObject.transform.LookAt(Target.transform);
            if (MyAnimator)
            {
                MyAnimator.SetBool("Aiming", true);
            }
           
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
   public  void StopAttack()
   {
        if (TerroristWeaponScript)
        {
            MyAnimator.SetBool("Aiming", false);
        }

    }
}


    

