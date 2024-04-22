using UnityEngine;

public class ControlerAnimationsPlayer : MonoBehaviour
{
    //Other components
    Animator Parameters;

    [SerializeField] PlayerControler ControlerPlayer;
    [SerializeField] MovePlayer MovePlayer;
    [SerializeField] Rigidbody RigPlayer;

    //Refrence to animations
    //[SerializeField] float TimeStelthKill = 7.0f;
    //[SerializeField] float WhenFinishedStelthKill;

    //References to additional objects
    [SerializeField] Transform BaseBodyPlayerForAnimations;
    
    //Parameters
    [SerializeField] float CurrentSpeed;
    [SerializeField] float DesirableSpeed;
    
    //Additional parameters to work 
    //[SerializeField] public float CurrentAddedRotateBodyPlayer;

    [SerializeField] float RotateBodyWhenAiming;
    //[SerializeField] float RotateBodyPlayerWhenAimingInStelth;


    void Start()
    {
        Parameters = GetComponent<Animator>();
        RigPlayer = ControlerPlayer.GetComponent<Rigidbody>();
        
        if (Parameters) Parameters.SetFloat("CurrentSpeed", 0.0f);
        else Debug.Log("Not set Parameters");

    }

    public void UpdateAnimations()
    {
        //Type date for work
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        //Vector3 HowRotateBasePlayer;
        //

        //Speed Movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) DesirableSpeed = 0.5f;
        else DesirableSpeed = 0.0f;
        if (Input.GetKey(KeyCode.LeftShift)) DesirableSpeed = 1.0f;

        //Change the float parameters
        if (CurrentSpeed > DesirableSpeed) CurrentSpeed -= (Time.deltaTime + 0.001f);
        if (CurrentSpeed < DesirableSpeed) CurrentSpeed += (Time.deltaTime + 0.001f);
        Mathf.Clamp(CurrentSpeed, 0.0f, 1.0f);

        //Change the float parameters
        Parameters.SetFloat("CurrentSpeed", CurrentSpeed); //Current speed player
        Parameters.SetFloat("SpeedHorizontal", Horizontal);
        Parameters.SetFloat("SpeedVertical", Vertical);

        //Change the bool parameters
        Parameters.SetBool("IsAiming", ControlerPlayer.WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething);

        Parameters.SetBool("HaveWeaponInHand", ControlerPlayer.WhatPlayerHandsHave == HandsPlayerHave.Weapon);
        Parameters.SetBool("HavePistolInHand", ControlerPlayer.WhatPlayerHandsHave == HandsPlayerHave.Pistol);
        Parameters.SetBool("IsCrouchWalk", ControlerPlayer.WhatPlayerLegsDo == LegsPlayer.SatDown);
        Parameters.SetBool("UsingLoot", ControlerPlayer.WhatPlayerHandsDo == HandsPlayer.UseSomething);

        //Triggers
        if (ControlerPlayer.IsJuming)
        {
            Parameters.SetTrigger("Jump");
            Debug.Log("True");
        }


        /*
        if(Time.time >= WhenFinishedStelthKill)
        {
            ControlerPlayer.StealthKilling = false;
            WhenFinishedStelthKill = Time.time + TimeStelthKill;
        }
        */

    }    
    
    public void ShootTrigger()
    {
        Parameters.SetTrigger("Shoot");
    }

}
