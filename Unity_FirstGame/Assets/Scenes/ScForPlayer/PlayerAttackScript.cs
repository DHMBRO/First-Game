using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackScript : MonoBehaviour
{
  //  public bool CanInstantKill;
    
    [SerializeField] private float MaxKillDistance = 3.0f;
    [SerializeField] private float MinKillDistance = 0.5f;
    Collider[] Colliders;
    protected Animator PlayerAnimator;
    void Start()
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
    }
    void Update()
    {

         
        if (Input.GetKeyDown(KeyCode.V))
        {
            float HalfExtents = (MaxKillDistance - MinKillDistance) / 2;

            Colliders = Physics.OverlapBox(gameObject.transform.position + 1.0f * gameObject.transform.forward, new Vector3(HalfExtents, HalfExtents, HalfExtents));
            foreach (Collider Collider in Colliders)
            {
                ZombieScript ZombieScript = Collider.gameObject.GetComponentInParent<ZombieScript>();
                LocateScript ZombieLocateScript = Collider.gameObject.GetComponentInParent<LocateScript>();
                InfScript InfoScript = Collider.gameObject.GetComponentInParent<InfScript>();
                if (ZombieScript && ZombieLocateScript && InfoScript)
                {
                    if (ZombieScript.IsObjectFromBehinde(gameObject)) 
                    {
                       // if (ZombieLocateScript.WhatForvardToMe(gameObject) == Collider.gameObject)
                        {
                            InfoScript.StealthKillCast(gameObject);
                            ZombieScript.InstansteKillMe();
                            break;
                        }
                    }
                }
            }
        }
    }
    
    public void SetPlayerAnimation(string TriggerName)
    {
        PlayerAnimator.SetTrigger(TriggerName);
    }
    public void StealthKill()
    {
        SetPlayerAnimation("StealthKill");
       //gameObject.transform.position = PointToKillMe.transform.position;

    }
}

/*
 void StealthKill (Gameobj Target)
{
   InfoScript = target.getComponent<InfScript>();
    InfoScript.SetStopped();
    gameobj.traansform.position = InfoScript.PosToKillMe;
    gameobj.LookAt(Target.transform.rotation);
    SetPlayerAnimation("StealthKill");
    InfoScript.SetAnimation("StealthKill")
    
}

 
 
 */
    

