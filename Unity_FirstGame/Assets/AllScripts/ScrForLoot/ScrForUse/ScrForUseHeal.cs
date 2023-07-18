using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrForUseHeal : MethodsFromDevelopers, IUsebleInterFace
{ 
    [SerializeField] float HealHp;

    public void GetReferences(GameObject ObjectToCopy, GameObject ObjectForCopy)
    {
        ObjectToCopy = ObjectForCopy;
        
    }

    public void Use(GameObject Target, SelectAnObject SelectObj)
    {
        Debug.Log("1");
        if (Target)
        {
            HpScript HealPointToTarget = Target.GetComponent<HpScript>();
            if (HealPointToTarget) HealPointToTarget.HealHp(HealHp);

            SelectObj.SelectObject();
            Destroy(gameObject, 3.0f);
        
        }
    }
    
    
}
