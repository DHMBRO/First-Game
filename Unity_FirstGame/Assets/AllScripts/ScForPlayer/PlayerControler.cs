using UnityEngine;

public class PlayerControler : MonoBehaviour, HeadInterface
{
    
    //Movement Components
    [SerializeField] private Move1F Move;
    [SerializeField] private MovePlayer MovePlayer;

    //Other Components
    [SerializeField] public ShootControler ControlerShoot;
    [SerializeField] private StelthScript StelthScript;
    [SerializeField] private DivertAttention DivertAttention;
    [SerializeField] private ExecutoreScriptToPlayer EEScript;
    [SerializeField] private ControlerAnimationsPlayer ScrAnimationsPlayer;
    [SerializeField] private PullBodyScript PlayerPullBodyScript;

    //Main Components To Work Player
    [SerializeField] private PickUp PickUpPlayer;
    [SerializeField] private DropControler ControlerDrop;
    [SerializeField] private SlotControler SlotControler;

    //Camera Components
    [SerializeField] private Transform PlayerCameraF1;
    [SerializeField] public ThirdPersonCamera CameraPlayerF3;
    
    //Inventory Components
    [SerializeField] private UiControler ControlerUi;
    [SerializeField] private UseAndDropTheLoot SelectObj;

    //Game Objects
    [SerializeField] Transform gameobject;
    [SerializeField] Transform Anchor;
    [SerializeField] public GameObject Head;

    //Bools
    [SerializeField] public bool IsJuming = false;
    [SerializeField] public bool StealthKilling = false;

    //Player
    [SerializeField] public Player WhatPlayerDo;
    [SerializeField] public CameraPlayer StateCamera;
    
    //Hands
    [SerializeField] public HandsPlayer WhatPlayerHandsDo;
    [SerializeField] public HandsPlayerHave WhatPlayerHandsHave;
    
    //Legs
    [SerializeField] public LegsPlayer WhatPlayerLegsDo;
    [SerializeField] public SpeedLegsPlayer WhatSpeedPlayerLegs;



    
    public Vector3 PlayerSpeed;

    // Positions
    public Vector3 PlayerLastPosition;
    //[SerializeField] public ModeMovement MovementMode;
    
    void Start()
    {
        PlayerLastPosition = gameObject.transform.position;


        //Movement
        Move = GetComponent<Move1F>();
        MovePlayer = GetComponent<MovePlayer>();
        
        //Other Scripts
        DivertAttention = GetComponent<DivertAttention>();
        StelthScript = GetComponent<StelthScript>();
        EEScript = GetComponent<ExecutoreScriptToPlayer>();
        PlayerPullBodyScript = GetComponent<PullBodyScript>();

        //Main Scripts To Work Player                  
        PickUpPlayer = GetComponent<PickUp>();
        ControlerDrop = GetComponent<DropControler>();
        SlotControler = GetComponent<SlotControler>();
        
        if (!ControlerUi) Debug.Log("Not set ControlerUi");
        
    }

    void Update()
    {
        

        if (ControlerUi)
        {
            if(Input.GetKeyDown(KeyCode.I)) ControlerUi.OpenOrCloseInventory();
            if (ControlerUi.InventoryIsOpen) WhatPlayerDo = Player.OpenInventory;
            else WhatPlayerDo = Player.Null;
            ControlerUi.InterfaceControler();
        }

        if (SelectObj)
        {
            if (SelectObj.ObjectToUse)
            {
                WhatPlayerHandsDo = HandsPlayer.UseSomething;
            }
            else 
            {
                WhatPlayerHandsDo = HandsPlayer.Null;
            }

        }
        else
        {
            WhatPlayerHandsDo = HandsPlayer.Null;
        }

        if((!ControlerUi || !ControlerUi.InventoryIsOpen) && WhatPlayerHandsDo == HandsPlayer.Null && !StealthKilling)
        {
            
            // Movement && Executore Noice
            bool Inputs = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
            
            if (MovePlayer)
            {
                //Change Mood Movement
                if (Inputs) WhatSpeedPlayerLegs = SpeedLegsPlayer.Walk;
                else WhatSpeedPlayerLegs = SpeedLegsPlayer.Null;

                //Run
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    WhatSpeedPlayerLegs = SpeedLegsPlayer.Run;
                }
                
            }

            // Drop
            if (ControlerDrop)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ControlerDrop.Drop();
                    SlotControler.ObjectInHand = null;
                    ControlerShoot = null;
                }
            }

            //Stelth 
            if (Input.GetKeyDown(KeyCode.LeftControl) && WhatPlayerLegsDo != LegsPlayer.SatDown)
            {
                WhatPlayerLegsDo = LegsPlayer.SatDown;
                MovePlayer.ControlCapsuleColider(true);
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && MovePlayer.AuditToStandUp())
            {
                WhatPlayerLegsDo = LegsPlayer.Null;
                MovePlayer.ControlCapsuleColider(false);
            }

            //Camera
            if (CameraPlayerF3.CameraIsUsig && (WhatPlayerHandsHave == HandsPlayerHave.Weapon || WhatPlayerHandsHave == HandsPlayerHave.Pistol))
            {
                if (Input.GetKey(KeyCode.Mouse1)) WhatPlayerHandsDo = HandsPlayer.AimingForDoSomething;
            }

            //IsAiming
            if (WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething && Inputs && WhatSpeedPlayerLegs != SpeedLegsPlayer.CrouchWalk) WhatSpeedPlayerLegs = SpeedLegsPlayer.Walk;
            if (ControlerUi) ControlerUi.Scope.gameObject.SetActive(WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething);

            //Stelth
            if (WhatPlayerLegsDo == LegsPlayer.SatDown)
            {
                if (Inputs) WhatSpeedPlayerLegs = SpeedLegsPlayer.CrouchWalk;
                else WhatSpeedPlayerLegs = SpeedLegsPlayer.Null;
            }
             

            //if (WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething && WhatSpeedPlayerLegs == SpeedLegsPlayer.CrouchWalk) WhatSpeedPlayerLegs = SpeedLegsPlayer.Walk;

            //if (Input.GetKeyUp(KeyCode.Mouse1)) IsAiming = false;

            // PickUp
            if (PickUpPlayer && WhatSpeedPlayerLegs != SpeedLegsPlayer.Run && WhatPlayerHandsDo != HandsPlayer.AimingForDoSomething) 
            {
                PickUpPlayer.RayForLoot();
            }
            
            
            
            // Slot Controler
            if (SlotControler)
            {
                SlotControler.MovingGunForSlots();
                
                if (SlotControler.CurrentSlotHand && SlotControler.ObjectInHand)
                {
                    ControlerShoot = SlotControler.ObjectInHand.GetComponent<ShootControler>();
                    
                }
                // Change Object In Hand
                if (Input.GetKeyDown("1"))
                {
                    //SlotControler.UpdateTypeWeaponInHand();
                    SlotControler.ChangingSlots();
                    SlotControler.Counter = 1;
                }
                if (Input.GetKeyUp("1"))
                {
                    SlotControler.Counter = 0;
                }
                SlotControler.UpdateTypeWeaponInHand();

            }
            
            // Shooting
            if (ControlerShoot && CameraPlayerF3)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Ray ForwardCamera = new Ray(CameraPlayerF3.transform.position, CameraPlayerF3.transform.forward);
                    ControlerShoot.Shoot();
                }
            }
            
            //Divert Attention 
            if (DivertAttention)
            {
                if (Input.GetKeyDown(KeyCode.Z)) DivertAttention.SpawnRock();
                if (Input.GetKey(KeyCode.Z)) DivertAttention.AimingToDrop();
                if (Input.GetKeyUp(KeyCode.Z))
                {
                    DivertAttention.AimingToDrop();
                    DivertAttention.DropRock();
                }
            }

            //PullBodyScript
            if (PlayerPullBodyScript)
            {
                PlayerPullBodyScript.SearchEnemyBody();
            }


            //Add Noice
            if (EEScript) EEScript.ExecutoreNoice();
            else Debug.Log("Not set EEScript");

            //Movement
            MovePlayer.RotateBodyPlayer(WhatSpeedPlayerLegs);
            //MovePlayer.Jump();

            //Other
            if (Input.GetKeyDown(KeyCode.T) && gameobject && Anchor)
            {
                gameobject.transform.position = Anchor.transform.position;

            }

            

        }

        ScrAnimationsPlayer.UpdateAnimations();






    }
    public Vector3 GetHeadPosition()
    {
        return Head.transform.position;
    }
    public Vector3 GetSpeed()
    {
        PlayerSpeed = PlayerLastPosition - gameObject.transform.position / Time.deltaTime;
        PlayerLastPosition = gameObject.transform.position;
        return PlayerSpeed;
    }
}
