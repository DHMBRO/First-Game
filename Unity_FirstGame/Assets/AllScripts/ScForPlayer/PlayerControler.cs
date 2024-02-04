using UnityEngine;

public class PlayerControler : MonoBehaviour
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
    [SerializeField] private PickUp PickUpPlayer;
    [SerializeField] private DropControler ControlerDrop;
    [SerializeField] private SlotControler SlotControler;

    //Camera Components
    [SerializeField] private Transform PlayerCameraF1;
    [SerializeField] private ThirdPersonCamera CameraPlayerF3;
    
    //Inventory Components
    [SerializeField] private UiControler ControlerUi;
    [SerializeField] private UseAndDropTheLoot SelectObj;

    //Game Objects
    [SerializeField] Transform gameobject;
    [SerializeField] Transform Anchor;
    
    //Bools
    [SerializeField] public bool IsAiming = false;
    [SerializeField] public bool InStealth = false;
    [SerializeField] public bool IsJuming = false;
    [SerializeField] public bool HaveWeaponInHand = false;
    [SerializeField] public bool HavePistolInHand = false;
    [SerializeField] public bool IsUsingLoot = false;
    [SerializeField] public bool IsRun = false;
    [SerializeField] public bool StealthKilling = false;

    //Enums
    [SerializeField] public WhatIsInHand Using;
    [SerializeField] public MainPlayer MainStatePlayer;
    [SerializeField] public WhatDoPlayer PlayerDo;
    [SerializeField] public ModeMovement MovementMode;
    [SerializeField] public CameraPlayer StateCamera;

    public enum MainPlayer
    {
        Null,

        InventoryIsOpen,
        ShootWithWeapon,
        InStealth,
        AimingToDropRock,
        
    }

    public enum WhatDoPlayer
    {
        Null,

        UseLoot,
    }

    public enum CameraPlayer
    {
        Null,

        RotateSimple,
        Aiming,
    }

    
    void Start()
    {
        //Movement
        Move = GetComponent<Move1F>();
        MovePlayer = GetComponent<MovePlayer>();
        
        //Other Scripts
        DivertAttention = GetComponent<DivertAttention>();
        StelthScript = GetComponent<StelthScript>();
        EEScript = GetComponent<ExecutoreScriptToPlayer>();
        
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
            if (ControlerUi.InventoryIsOpen) MainStatePlayer = MainPlayer.InventoryIsOpen;
            ControlerUi.InterfaceControler();
        }

        if (SelectObj)
        {
            if (SelectObj.ObjectToUse)
            {
                PlayerDo = WhatDoPlayer.UseLoot;
                IsUsingLoot = true;
            }
            else 
            {
                PlayerDo = WhatDoPlayer.Null;
                IsUsingLoot = false;
            }

        }
        
        if((!ControlerUi || !ControlerUi.InventoryIsOpen) && PlayerDo == WhatDoPlayer.Null && !StealthKilling)
        {
            
            // Movement && Executore Noice
            bool Inputs = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
            
            if (MovePlayer)
            {
                //Change Mood Movement
                if (Inputs) MovementMode = ModeMovement.Go;
                else MovementMode = ModeMovement.Null;

                //Run
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    InStealth = false;
                    IsRun = true;
                    MovementMode = ModeMovement.Run;
                }
                else IsRun = false;

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

            //Stels 
            if (Input.GetKeyUp(KeyCode.LeftControl)) InStealth = !InStealth;

            //Camera
            if (CameraPlayerF3.CameraIsUsig && (HaveWeaponInHand || HavePistolInHand))
            {
                if (Input.GetKey(KeyCode.Mouse1)) IsAiming = true;
                else IsAiming = false;
            }

            //IsAiming
            if (IsAiming && Inputs && !InStealth) MovementMode = ModeMovement.Aiming;
            if (ControlerUi) ControlerUi.Scope.gameObject.SetActive(IsAiming);

            //Stels
            if (InStealth && !IsAiming)
            {
                MovementMode = ModeMovement.Stelth;
                MainStatePlayer = MainPlayer.InStealth;
            }

            if (IsAiming && InStealth) MovementMode = ModeMovement.StelsAndAiming;

            //if (Input.GetKeyUp(KeyCode.Mouse1)) IsAiming = false;

            // PickUp
            if (PickUpPlayer && !IsRun && !IsAiming) 
            {
                PickUpPlayer.RayForLoot();
                PickUpPlayer.ComplertingTheLink();
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

            //Add Noice
            if (EEScript) EEScript.ExecutoreNoice(MovementMode);
            else Debug.Log("Not set EEScript");

            //Movement
            MovePlayer.Move(MovementMode);
            //MovePlayer.Jump();

            //Other
            if (Input.GetKeyDown(KeyCode.T) && gameobject && Anchor)
            {
                gameobject.transform.position = Anchor.transform.position;

            }

            

        }

        ScrAnimationsPlayer.UpdateAnimations();






    }

}
