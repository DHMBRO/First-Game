using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrForUseHeal : MethodsFromDevelopers, IUsebleInterFace, IDrop
{
    [SerializeField] public GameObject ObjectToHeal;
    [SerializeField] public Transform PointToDrop;

    [SerializeField] float HealHp;
    public void Use()
    {
        Debug.Log("1");
        if (ObjectToHeal)
        {
            HpScript HealPointToTarget = ObjectToHeal.GetComponent<HpScript>();
            if (HealPointToTarget) HealPointToTarget.HealHp(HealHp);

        }
    }
    
    public void Drop()
    {
        if (PointToDrop)
        {
            DropObjects(gameObject.transform, PointToDrop.transform);
        }
    }
}
