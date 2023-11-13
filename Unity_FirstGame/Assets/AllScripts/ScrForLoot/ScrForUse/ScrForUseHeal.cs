using UnityEngine;
using System.Collections;

public class ScrForUseHeal : MethodsFromDevelopers, IUsebleInterFace
{ 
    [SerializeField] float HealHp;

    [SerializeField] float TimeToUse;
    [SerializeField] float TimeoToDestroy;
    
    public void Use(GameObject Target, InfoForLoot InfoLoot, SelectAnObject SelectObj)
    {
        //Methods
        SelectObj.PutLoot(transform);
        SelectObj.SelectObject();
        
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
