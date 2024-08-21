using UnityEditor.Rendering.LookDev;
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
    [SerializeField] private IKControlerForPlayer ControlrPlayerIK;

    //Main Components To Work Player
    [SerializeField] private PlayerToolsToInteraction PlayerTools; 
    [SerializeField] private PickUp PickUpPlayer;
    [SerializeField] private InteractionScr PlayerInteractionScr;
    [SerializeField] private PullBodyScript PlayerPullBodyScript;
    [SerializeField] private DropControler ControlerDrop;
    [SerializeField] private SlotControler SlotControler;
    [SerializeField] private AimControler ControlerAim;
    [SerializeField] private OnDeadScript OnDeadPlayerScript;

    //Camera Components
    [SerializeField] private Transform PlayerCameraF1;
    [SerializeField] public ThirdPersonCamera CameraPlayerF3;
    [SerializeField] private ScopeControler ControlerScope;
    
    //Inventory Components
    [SerializeField] public UiControler ControlerUi;
    [SerializeField] private UseAndDropTheLoot UseAndDropTheLootScr;

    //Game Objects
    [SerializeField] Transform gameobject;
    [SerializeField] Transform Anchor;
    [SerializeField] public GameObject Head;

    //Bools
    [SerializeField] public bool IsJuming = false;
    [SerializeField] public bool StealthKilling = false;
    [SerializeField] public bool MenuIsOpen = false;

    //Player
    [SerializeField] public Player WhatPlayerDo;
    [SerializeField] public CameraPlayer StateCamera;
    
    //Hands
    [SerializeField] public HandsPlayer WhatPlayerHandsDo;
    [SerializeField] public HandsPlayerHave WhatPlayerHandsHave;
    
    //Legs
    [SerializeField] public LegsPlayer WhatPlayerLegsDo;
    [SerializeField] public SpeedLegsPlayer WhatSpeedPlayerLegs;


    // Positions
    public Vector3 PlayerSpeed;
    public Vector3 PlayerLastPosition;
    
    private float TimeToCallFunction_PlayerTooolsToInteraction;
    [SerializeField] private float TimeDelayToCall_PlayerTooolsToInteraction = 1.0f;
    
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
        OnDeadPlayerScript = GetComponent<OnDeadScript>();

        ControlerDrop = GetComponent<DropControler>();
        SlotControler = GetComponent<SlotControler>();
        ControlerAim = GetComponent<AimControler>();
        ControlrPlayerIK = GetComponentInChildren<IKControlerForPlayer>();

        if (CameraPlayerF3)
        {
            ControlerScope = CameraPlayerF3.GetComponent<ScopeControler>();
        }

        Cursor.lockState = CursorLockMode.Locked;

        //All Debug
        if (!ControlerUi) Debug.Log("Not set ControlerUi");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ControlerUi)
            {
                ControlerUi.SetPanelSettings();
            }
        }

        if (MenuIsOpen || OnDeadPlayerScript.IsPlayerDead())
        {
            return;
        }

        PlayerSpeed = (PlayerLastPosition - gameObject.transform.position) / Time.deltaTime;
        PlayerLastPosition = gameObject.transform.position;

        if (ControlerUi)
        {
            if (Input.GetKeyDown(KeyCode.I)) ControlerUi.OpenOrCloseInventory();
            
            if (ControlerUi.InventoryIsOpen)
            {
                WhatPlayerDo = Player.OpenInventory;
                return;
            }
            else if(WhatPlayerDo == Player.OpenInventory) WhatPlayerDo = Player.Null;
            ControlerUi.InterfaceControler();
            
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

        if (!StealthKilling)
        {
            
            // Movement && Executore Noice
            if (MovePlayer)
            {
                CheckToMove();
            }

            //Drop
            if (ControlerDrop)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ControlerDrop.Drop();
                    SlotControler.ObjectInHand = null;
                    ControlerShoot = null;

                    ControlerAim.UpdateWeapoMuzzle();
                }
            }

            //Stelth 
            if (WhatPlayerHandsDo != HandsPlayer.CarryBody)
            {
                if (WhatPlayerLegsDo != LegsPlayer.SatDown && Input.GetKeyDown(KeyCode.LeftControl))
                {
                    WhatPlayerLegsDo = LegsPlayer.SatDown;
                    MovePlayer.ControlCapsuleColider(true);
                }
                else if (WhatPlayerLegsDo == LegsPlayer.SatDown && Input.GetKeyDown(KeyCode.LeftControl) && MovePlayer.AuditToStandUp())
                {
                    WhatPlayerLegsDo = LegsPlayer.Null;
                    MovePlayer.ControlCapsuleColider(false);
                }
            }

            //Camera
            if (CameraPlayerF3)
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

                if (true)
                {
                    CameraPlayerF3.CameraUpdate();
                }

            }

            
            //Stelth
            if (WhatPlayerLegsDo == LegsPlayer.SatDown)
            {
                if (Input.GetAxisRaw("Horizontal") != 0.0f || Input.GetAxisRaw("Vertical") != 0.0f) WhatSpeedPlayerLegs = SpeedLegsPlayer.CrouchWalk;
                else WhatSpeedPlayerLegs = SpeedLegsPlayer.Null;
            }
            
            //Player Tools
            if (PlayerTools && !ControlerUi.InventoryIsOpen && Time.time >= TimeToCallFunction_PlayerTooolsToInteraction)
            {
                TimeToCallFunction_PlayerTooolsToInteraction = Time.time + TimeDelayToCall_PlayerTooolsToInteraction;
                PlayerTools.InteractionWithRayCast();
                PlayerTools.SearchObjectsByBoxOverlap();
            }
            
            if (!PlayerTools) Debug.Log("Not set PlayerTools");

            //Interaction
            if ((PlayerInteractionScr || PickUpPlayer || PlayerPullBodyScript) && WhatSpeedPlayerLegs != SpeedLegsPlayer.Run)
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    PlayerTools.TryToInteractDelegate(PlayerTools.LastSelectedObject);
                }
            }

            //SlotControler
            if (SlotControler && WhatPlayerHandsDo == HandsPlayer.Null)
            {    
                if (SlotControler.ObjectInHand)
                {
                    ControlerShoot = SlotControler.ObjectInHand.GetComponent<ShootControler>();
                }
                else
                {
                    ControlerShoot = null;
                }

                SlotControler.ChangingSlots();
                SlotControler.UpdateTypeWeaponInHand(); // > 
                if(ControlerAim && ControlerAim.enabled) ControlerAim.UpdateWeapoMuzzle(); // <

                if (Input.GetKeyDown(KeyCode.R))
                {
                    SlotControler.Recharge();
                }

                ControlrPlayerIK.SetupIKReferences();
            }

            // AimControler 
            if (ControlerAim && StateCamera == CameraPlayer.Aiming && WhatPlayerHandsDo != HandsPlayer.CarryBody)
            {
                ControlerAim.Aim();
            }

            // Shooting || Weapon
            if (ControlerShoot && CameraPlayerF3)
            {
                ScopeScr ScrScope = ControlerShoot.GetComponent<ScopeScr>(); 

                if (Input.GetKey(KeyCode.Mouse0) && ControlerShoot.NowIsEnable() && StateCamera == CameraPlayer.Aiming)
                {
                    Ray ForwardCamera = new Ray(CameraPlayerF3.transform.position, CameraPlayerF3.transform.forward);
                    
                    ControlerShoot.SetShootDelegat();
                    ScrAnimationsPlayer.ShootTrigger();
                }

                ControlerScope.UseScope(ScrScope, StateCamera == CameraPlayer.Aiming);
            }
            
            //Divert Attention 
            if (DivertAttention && !SlotControler.ObjectInHand)
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
            MovePlayer.RotateBodyPlayer();
            //MovePlayer.Jump();

        }

        ScrAnimationsPlayer.UpdateAnimations();

    }

    private void CheckToMove()
    {
        //Change Mood Movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
            {
                return;
            }
            else
            {
                WhatSpeedPlayerLegs = SpeedLegsPlayer.Walk;

                //Run
                if (Input.GetKey(KeyCode.LeftShift) && WhatPlayerHandsDo == HandsPlayer.Null)
                {
                    WhatSpeedPlayerLegs = SpeedLegsPlayer.Run;
                }
            }
        }
        else
        {
            WhatSpeedPlayerLegs = SpeedLegsPlayer.Null;
        }
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
