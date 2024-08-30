using UnityEngine;
using System.Collections;

public class ScrForUseHeal : MethodsFromDevelopers, IUsebleInterFace
{ 
    [SerializeField] float HealHp;

    [SerializeField] float TimeToUse;
    [SerializeField] float TimeoToDestroy;

    public bool Audit(GameObject Target, ScrSaveAndGiveInfo InfoLoot, UseAndDropTheLoot SelectObj)
    {
        bool Result = false;
        HpScript HpTatget = Target.GetComponent<HpScript>();

        if (HpTatget.HealthPoint == HpTatget.MaxHp || HpTatget.HealthPoint == 0.0f)
        {
            Result = false;
        }
        else Result = true; 

        return Result;
    }

    public void Use(GameObject Target, ScrSaveAndGiveInfo InfoLoot, UseAndDropTheLoot SelectObj)
    {
        //Methods
        SelectObj.PutLoot(transform);
        SelectObj.DeleteReferenceToLoot();
        
        //Do Use
        //Invoke("RealUse", TimeToUse);
        StartCoroutine("RealUse", Target);
        
    }


    IEnumerator RealUse(GameObject Target)
    {
        yield return new WaitForSeconds(TimeToUse);
        
        //References To Components
        HpScript HealPointToTarget = Target.GetComponent<HpScript>();
        
        //Do Use
        if (HealPointToTarget) HealPointToTarget.HealHp(HealHp);

        Destroy(gameObject);
        
    }



}
