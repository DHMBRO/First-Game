using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    //Movement Components
    [SerializeField] private Move1F Move;
    [SerializeField] private MovePlayer MovePlayer;

    //Other Components
    [SerializeField] public ShootControler ControlerShoot;
    [SerializeField] private StelsScript StelsScript;
    [SerializeField] private DivertAttention DivertAttention;
    [SerializeField] private ExecutoreScriptToPlayer EEScript;

    //Main Components To Work Player
    [SerializeField] private PickUp PickUpPlayer;
    [SerializeField] private DropControler ControlerDrop;
    [SerializeField] private SlotControler SlotControler;

    //Camera Components
    [SerializeField] private Transform PlayerCameraF1;
    [SerializeField] private Camera CameraPlayerF3;
    
    //Inventory Components
    [SerializeField] private UiControler ControlerUi;
    
    //Game Objects
    [SerializeField] Transform gameobject;
    [SerializeField] Transform Anchor;
    
    //Bools
    [SerializeField] public bool Aiming = false;
    [SerializeField] public bool InStels = false;

    //Enums
    [SerializeField] public WhatIsInHand Using;
    [SerializeField] public MainPlayer MainStatePlayer;
    [SerializeField] public ModeMovement MovementMode;
    [SerializeField] public CameraPlayer StateCamera;


    public enum MainPlayer
    {
        Null,

        InventoryIsOpen,
        Stels,
        UseLoot,
        AimingToDropRock,
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
        StelsScript = GetComponent<StelsScript>();
        EEScript = GetComponent<ExecutoreScriptToPlayer>();
        
        //Main Scripts To Work Player                  
        PickUpPlayer = GetComponent<PickUp>();
        ControlerDrop = GetComponent<DropControler>();
        SlotControler = GetComponent<SlotControler>();
        
        if (!ControlerUi) Debug.Log("Not set ControlerUi");
    }

    void Update()
    {
        if (ControlerUi && Input.GetKeyDown(KeyCode.I))
        {
            ControlerUi.OpenOrCloseInventory();
        }
        
        if(!ControlerUi || !ControlerUi.InventoryIsOpen)
        {
            // Movement && Executore Noice
            if (MovePlayer)
            {
                bool Inputs = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
                //Change Mood Movement
                if (Inputs) MovementMode = ModeMovement.Go;
                else MovementMode = ModeMovement.Null;

                if (Input.GetKey(KeyCode.LeftShift)) 
                {
                    InStels = false;
                    MovementMode = ModeMovement.Run;
                } 
                if (Aiming) MovementMode = ModeMovement.Aiming;

                if (Inputs)
                {
                    //Stels
                    if (InStels) MovementMode = ModeMovement.Stels;
                    
                    //Add Noice
                    if (EEScript) EEScript.ExecutoreNoice(MovementMode);
                    else Debug.Log("Not set EEScript");

                }


                
                //Movement
                MovePlayer.Move(MovementMode);
                //MovePlayer.Jump();
                
            }
            
            //Camera
            if (Input.GetKey(KeyCode.Mouse1)) Aiming = true;
            if (Input.GetKeyUp(KeyCode.Mouse1)) Aiming = false;
            
            // PickUp
            if (PickUpPlayer) 
            {
                PickUpPlayer.RayForLoot();
                PickUpPlayer.ComplertingTheLink();
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
            
            // Slot Controler
            if (SlotControler)
            {
                SlotControler.MovingGunForSlots();

                if (SlotControler.SlotHand && SlotControler.ObjectInHand)
                {
                    ControlerShoot = SlotControler.ObjectInHand.GetComponent<ShootControler>();
                    
                }
                // Change Object In Hand
                if (Input.GetKeyDown("1") && SlotControler.Counter == 0)
                {
                    SlotControler.ChangingSlots();
                    SlotControler.Counter = 1;
                }
                if (Input.GetKeyUp("1"))
                {
                    SlotControler.Counter = 0;
                }
                
            }
            
            // Shooting
            if (ControlerShoot)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
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
            
            //Stels 
            if (Input.GetKeyUp(KeyCode.X)) InStels = true;
            if (Input.GetKeyDown(KeyCode.X)) InStels = false;

            //Other
            if (Input.GetKeyDown(KeyCode.T) && gameobject && Anchor)
            {
                gameobject.transform.position = Anchor.transform.position;

            }



        }







    }

}
