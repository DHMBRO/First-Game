using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrHeal : MonoBehaviour, IUsebleInterFace
{
    [SerializeField] private GameObject ObjectToHeal;

    [SerializeField] float HpToHeal;
    
    
    public void Use()
    {
        if (ObjectToHeal)
        {
            HpScript HpToObject = ObjectToHeal.GetComponent<HpScript>();
            if (HpToObject)
            {
                HpToObject.HealHp(HpToHeal);
            }
        }
    }
    
}
