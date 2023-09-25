using UnityEngine;

public class ScrForUseHeal : MethodsFromDevelopers, IUsebleInterFace
{ 
    [SerializeField] float HealHp;

    [SerializeField] float TimeToUse;
    //[SerializeField] float TimeoToDestroy;

    public void Use(GameObject Target, InfoForLoot InfoLoot, SelectAnObject SelectObj)
    {
        Debug.Log("1");
        
        HpScript HealPointToTarget = Target.GetComponent<HpScript>();
        if (HealPointToTarget) HealPointToTarget.HealHp(HealHp);

        SelectObj.PutLoot(transform);
        SelectObj.SelectObject();

        Destroy(gameObject, TimeToUse);
        

    }

    
}
