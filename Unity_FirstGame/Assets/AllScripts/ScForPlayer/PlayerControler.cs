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
    
    //Main Components To Work Player
    [SerializeField] private PlayerToolsToInteraction PlayerTools; 
    [SerializeField] private PickUp PickUpPlayer;
    [SerializeField] private InteractionScr PlayerInteractionScr;
    [SerializeField] private PullBodyScript PlayerPullBodyScript;
    [SerializeField] private DropControler ControlerDrop;
    [SerializeField] private SlotControler SlotControler;

    //Camera Components
    [SerializeField] private Transform PlayerCameraF1;
    [SerializeField] public ThirdPersonCamera CameraPlayerF3;
    
    //Inventory Components
    [SerializeField] private UiControler ControlerUi;
    [SerializeField] private UseAndDropTheLoot UseAndDropTheLootScr;

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
        
        //Main Scripts To Work Player
        PlayerTools = GetComponent<PlayerToolsToInteraction>();
        PickUpPlayer = GetComponent<PickUp>();
        PlayerInteractionScr = GetComponent<InteractionScr>();
        PlayerPullBodyScript = GetComponent<PullBodyScript>();
        
        ControlerDrop = GetComponent<DropControler>();
        SlotControler = GetComponent<SlotControler>();
        
        //All Debug
        if (!ControlerUi) Debug.Log("Not set ControlerUi");
        
        //Setup References
        
    }

    void Update()
    {
        PlayerSpeed = (PlayerLastPosition - gameObject.transform.position) / Time.deltaTime;
        PlayerLastPosition = gameObject.transform.position;

        if (ControlerUi)
        {
            if (Input.GetKeyDown(KeyCode.I)) ControlerUi.OpenOrCloseInventory();
            if (ControlerUi.InventoryIsOpen) WhatPlayerDo = Player.OpenInventory;
            else if(WhatPlayerDo == Player.OpenInventory) WhatPlayerDo = Player.Null;
            
            ControlerUi.InterfaceControler();
            ControlerUi.DeleteNameOnTable();

        }

        if (UseAndDropTheLootScr)
        {
            if (UseAndDropTheLootScr.ObjectToUse)
            {
                WhatPlayerHandsDo = HandsPlayer.UseSomething;
            }
            
        }
        else if(WhatPlayerHandsDo == HandsPlayer.UseSomething)
        {
            WhatPlayerHandsDo = HandsPlayer.Null;
        }

        if (/*(!ControlerUi || !ControlerUi.InventoryIsOpen) && WhatPlayerHandsDo == HandsPlayer.Null &&*/ !StealthKilling)
        {
            
            // Movement && Executore Noice
            bool Inputs = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
            
            if (MovePlayer)
            {
                //Change Mood Movement
                if (Inputs) WhatSpeedPlayerLegs = SpeedLegsPlayer.Walk;
                else WhatSpeedPlayerLegs = SpeedLegsPlayer.Null;

                //Run
                if (Input.GetKey(KeyCode.LeftShift) && WhatPlayerHandsDo == HandsPlayer.Null )
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
            if (WhatPlayerHandsDo != HandsPlayer.CarryBody)
            {
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
            }

            //Camera
            if (CameraPlayerF3.CameraIsUsig)
            {
                if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Z))
                {
                    if (WhatPlayerHandsDo == HandsPlayer.Null) WhatPlayerHandsDo = HandsPlayer.AimingForDoSomething;
                    if (StateCamera == CameraPlayer.RotateSimple) StateCamera = CameraPlayer.Aiming; 
                }
                else if (WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething)
                {
                    WhatPlayerHandsDo = HandsPlayer.Null;
                    StateCamera = CameraPlayer.RotateSimple;
                }

            }

            //IsAiming
            if (WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething && Inputs && WhatSpeedPlayerLegs != SpeedLegsPlayer.CrouchWalk) 
            {
                WhatSpeedPlayerLegs = SpeedLegsPlayer.Walk;
                
            }

            if (ControlerUi)
            {
                ControlerUi.Scope.gameObject.SetActive(WhatPlayerHandsDo == HandsPlayer.AimingForDoSomething);

            }

            //Stelth
            if (WhatPlayerLegsDo == LegsPlayer.SatDown)
            {
                if (Inputs) WhatSpeedPlayerLegs = SpeedLegsPlayer.CrouchWalk;
                else WhatSpeedPlayerLegs = SpeedLegsPlayer.Null;
            }
            
            //Player Tools
            if (PlayerTools /* && WhatSpeedPlayerLegs != SpeedLegsPlayer.Run && WhatPlayerHandsDo == HandsPlayer.Null*/)
            {
                PlayerTools.InteractionWithRayCast();
                
            }
            
            if (!PlayerTools) Debug.Log("Not set PlayerTools");

            // PickUp
            if (PickUpPlayer && WhatSpeedPlayerLegs != SpeedLegsPlayer.Run && WhatPlayerHandsDo == HandsPlayer.Null) 
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    PickUpPlayer.Work();
                }
            }
            if (!PickUpPlayer) Debug.Log("Cannot  PickUpPlayer");

            //PlayerPullBodyScript
            if (PlayerPullBodyScript && PlayerPullBodyScript.CanEnable() && Input.GetKeyDown(KeyCode.X))
            {
                SlotControler.PutWeapon();
                PlayerPullBodyScript.Work();
                
                if (PlayerPullBodyScript.PlayerHingeJoint)
                {
                    WhatPlayerHandsDo = HandsPlayer.CarryBody;
                }
                else
                {
                    WhatPlayerHandsDo = HandsPlayer.Null;
                    SlotControler.UpWeapon();
                }
                if (WhatPlayerLegsDo != LegsPlayer.SatDown)
                {
                    WhatPlayerLegsDo = LegsPlayer.SatDown;
                    MovePlayer.ControlCapsuleColider(true);
                }

            }

            Debug.Log(PlayerPullBodyScript && PlayerPullBodyScript.CanEnable() && Input.GetKeyDown(KeyCode.X));

            //InteractionScr
            if (PlayerInteractionScr)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    PlayerInteractionScr.Work();
                }
            }

            // Slot Controler
            if (SlotControler && WhatPlayerHandsDo == HandsPlayer.Null)
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
                }
                SlotControler.UpdateTypeWeaponInHand();

            }
            
            // Shooting || Weapon
            if (ControlerShoot && CameraPlayerF3)
            {
                if (Input.GetKey(KeyCode.Mouse0) && ControlerShoot.NowIsEnable())
                {
                    Ray ForwardCamera = new Ray(CameraPlayerF3.transform.position, CameraPlayerF3.transform.forward);
                    ControlerShoot.SetShootDelegat();
                }

                
            }
            
            //Divert Attention 
            if (DivertAttention && WhatPlayerHandsDo == HandsPlayer.Null)
            {
                if (Input.GetKeyDown(KeyCode.Z)) DivertAttention.SpawnRock();
                if (Input.GetKey(KeyCode.Z)) DivertAttention.AimingToDrop();
                if (Input.GetKeyUp(KeyCode.Z))
                {
                    DivertAttention.AimingToDrop();
                    DivertAttention.DropRock();
                }
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
        
        return PlayerSpeed;
    }
}
