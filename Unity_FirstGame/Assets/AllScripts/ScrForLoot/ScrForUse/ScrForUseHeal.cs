using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrForUseHeal : MethodsFromDevelopers, IUsebleInterFace
{
    

    [SerializeField] public GameObject ObjectToHeal;

    [SerializeField] float HealHp;

    public void GetReferences(GameObject ObjectToCopy, GameObject ObjectForCopy)
    {
        ObjectToCopy = ObjectForCopy;
        
    }

    public void Use()
    {
        Debug.Log("1");
        if (ObjectToHeal)
        {
            HpScript HealPointToTarget = ObjectToHeal.GetComponent<HpScript>();
            if (HealPointToTarget) HealPointToTarget.HealHp(HealHp);
            
            Destroy(gameObject, 2.5f);
        }
    }
    
    
}
