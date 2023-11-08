using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public bool CanInstantKill;
    [SerializeField] private float MaxKillDistance = 3.0f;
    [SerializeField] private float MinKillDistance = 0.5f;
    [SerializeField] GameObject BodyPlayer;
    Collider[] Colliders;
    void Start()
    {
        
    }
    void Update()
    {

         
        if (Input.GetKeyDown(KeyCode.Z))
        {
            float HalfExtents = (MaxKillDistance - MinKillDistance) / 2;

            Colliders = Physics.OverlapBox(BodyPlayer.transform.position + 1.0f * BodyPlayer.transform.forward, new Vector3(HalfExtents, HalfExtents, HalfExtents));
            
            foreach (Collider Collider in Colliders)
            {
                ZombieScript ZombieScript = Collider.gameObject.GetComponentInParent<ZombieScript>();
                LocateScript ZombieLocateScript = Collider.gameObject.GetComponentInParent<LocateScript>();
                if (ZombieScript && ZombieLocateScript)
                {
                    if (ZombieScript.IsObjectFromBehinde(gameObject)) 
                    {
                        if (ZombieLocateScript.WhatForvardToMe(BodyPlayer) == Collider.gameObject)
                        {
                            ZombieScript.InstansteKillMe();
                            break;
                        } 
                    }
                }
            }
        }
    }
    
    }
    

