using UnityEngine;

public class StelthScript : MonoBehaviour
{
    [SerializeField] private PlayerControler ControlerPlayer;
    public bool Stelth;

    void Start()
    {
        ControlerPlayer = GetComponent<PlayerControler>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bush"))
        {
            if(ControlerPlayer)
            {
                if(ControlerPlayer.WhatPlayerLegsDo == LegsPlayer.SatDown
                && ControlerPlayer.WhatPlayerHandsDo != HandsPlayer.AimingForDoSomething)
                {
                    Stelth = true;
                }
                else
                {
                    Stelth = false;
                }
            }
            else
            {
                Stelth = true;
            }
        }
        else Stelth = false;



    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bush"))
        {
            Stelth = false;
        }    
    }
}
