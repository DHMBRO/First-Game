using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrForUseHeal : MonoBehaviour, IUsebleInterFace
{
    [SerializeField] private GameObject ObjectToHeal;

    [SerializeField] float HealHp;
    public void Use()
    {
        HpScript HealPointToTarget = ObjectToHeal.GetComponent<HpScript>();
        if (HealPointToTarget) HealPointToTarget.HealHp(HealHp);
    }
    
}
