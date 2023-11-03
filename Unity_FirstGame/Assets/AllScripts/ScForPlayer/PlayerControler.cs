using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Move1F Move;
    [SerializeField] private MovePlayer MovePlayer;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private StelsScript StelsScript;
    [SerializeField] private Camera CameraPlayer1;
    
    [SerializeField] private PickUp PickUpPlayer;
    [SerializeField] private DropControler ControlerDrop;
    [SerializeField] private SlotControler SlotControler;
    [SerializeField] public ShootControler ControlerShoot;
    [SerializeField] private DivertAttention DivertAttention;

    [SerializeField] private UiControler ControlerUi;

    [SerializeField] Transform gameobject;
    [SerializeField] Transform Anchor;

    [SerializeField] public bool Aiming = false;
    [SerializeField] private bool WorkWithOutInventory = false;
    

    [SerializeField] WhatIsInHand Using;


    enum MainPlayer
    {
        Null,

        InventoryIsOpen,
        Stels,
        UseLoot,
        AimingToDropRock,
    }

    enum CameraPlayer
    {
        Null,

        RotateSimple,
        Aiming,
    }

    [SerializeField] MainPlayer MainStatePlayer;
    [SerializeField] CameraPlayer StateCamera;
    

    void Start()
    {
        //Movement
        Move = GetComponent<Move1F>();
        MovePlayer = GetComponent<MovePlayer>();
        MovePlayer.ControlerPlayer = GetComponent<PlayerControler>();
        
        StelsScript = GetComponent<StelsScript>();
                    
        PickUpPlayer = GetComponent<PickUp>();
        ControlerDrop = GetComponent<DropControler>();
        SlotControler = GetComponent<SlotControler>();
        DivertAttention = GetComponent<DivertAttention>();

    }
    
    void Update()
    {
        if (!ControlerUi) { Debug.Log("Not set ControlerUi"); WorkWithOutInventory = true;}

        if (!WorkWithOutInventory && Input.GetKeyDown(KeyCode.I))
        {
            ControlerUi.OpenOrCloseInventory();
        }
        


        if(!WorkWithOutInventory && !ControlerUi.InventoryIsOpen)
        {
            // Movement
            if (MovePlayer)
            {
                MovePlayer.Move();
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
            //Other
            if (Input.GetKeyDown(KeyCode.T) && gameobject && Anchor)
            {
                gameobject.transform.position = Anchor.transform.position;

            }



        }







    }

}
