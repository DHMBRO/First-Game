using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject Target;
    
    [SerializeField] float Heal;
    [SerializeField] float Damage;
    [SerializeField] bool HealPlayer = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player01")
        {
            Debug.Log("Player02 Enter in atak triger !");
            Target = other.gameObject;
            AtakAndHeal_Player(Target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player01")
        {
            Debug.Log("Player02 Exit from atak triger !");
            Target = null;
            
        }
    }

    void AtakAndHeal_Player(GameObject Target01)
    {
        HpScript HitPointPlayer = Target01.GetComponent<HpScript>();
        
        if (HitPointPlayer && !HealPlayer)
        {
            if(Damage > 0.0f) HitPointPlayer.MinusHp(Damage);
        }
        else if (HitPointPlayer && HealPlayer)
        {
            if(Heal > 0.0f) HitPointPlayer.PlusHp(Heal);
        }
    }
}
