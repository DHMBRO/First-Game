using UnityEngine;
using System.Collections;

public class ScrForUseHeal : MethodsFromDevelopers, IUsebleInterFace
{ 
    [SerializeField] float HealHp;

    [SerializeField] float TimeToUse;
    [SerializeField] float TimeoToDestroy;
    
    public void Use(GameObject Target, InfoForLoot InfoLoot, SelectAnObject SelectObj)
    {
        
        Debug.Log("1");
        
        //HpScript HealPointToTarget = Target.GetComponent<HpScript>();
        //if (HealPointToTarget) HealPointToTarget.HealHp(HealHp);

        SelectObj.PutLoot(transform);
        SelectObj.SelectObject();

        //Invoke("RealUse", TimeToUse);
        StartCoroutine("RealUse", Target);
        

        //Destroy(gameObject, TimeToUse);



    }



    IEnumerator RealUse(GameObject Target)
    {
        Debug.Log("RealUse is work, Time to use: " + TimeToUse);

        yield return new WaitForSeconds(5.0f);
        
        Debug.Log("Time is over");

        HpScript HealPointToTarget = Target.GetComponent<HpScript>();
        if (HealPointToTarget) HealPointToTarget.HealHp(HealHp);
        
        Destroy(gameObject);
        //yield return null;
    }

}
