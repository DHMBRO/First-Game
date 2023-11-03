using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Move1F Move;
    [SerializeField] private MovePlayer MovePlayer;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private StelsScript StelsScript;

    [SerializeField] private PickUp PickUpPlayer;
    [SerializeField] private DropControler ControlerDrop;
    [SerializeField] private SlotControler SlotControler;
    [SerializeField] public ShootControler ControlerShoot;
    [SerializeField] private DivertAttention DivertAttention;

    [SerializeField] private UiControler ControlerUi;

    [SerializeField] Transform gameobject;
    [SerializeField] Transform Anchor;

    [SerializeField] public bool Aiming = false;

    [SerializeField] WhatIsInHand Using;


    enum MainPlayer
    {
        Null,

        InventoryIsOpen,
        Stels,
        UseLoot,
        AimingToDropRock,
    }

    enum Movement
    {
        Null,
        
        Go,
        Run,
        Jump,
    }
    
    enum Hand
    {
        Null,
        
        Knife,
        Weapon,
        Loot,
    }
    
    enum Camera
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
        MovePlayer.ControlerPlayer = GetComponent<PlayerControler>();
        
        StelsScript = GetComponent<StelsScript>();
                    
        PickUpPlayer = GetComponent<PickUp>();
        ControlerDrop = GetComponent<DropControler>();
        SlotControler = GetComponent<SlotControler>();
        DivertAttention = GetComponent<DivertAttention>();

    }
    
    void Update()
    {   
        if(ControlerUi && ControlerUi.InventoryIsOpen == false || !ControlerUi)
        {
            if (SlotControler.SlotHand)
            {
                if (SlotControler.ObjectInHand)
                {
                    if(SlotControler.ObjectInHand.GetComponent<ShootControler>()) ControlerShoot = SlotControler.ObjectInHand.GetComponent<ShootControler>();
                }
                else ControlerShoot = null;
                
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
            else Debug.Log("Not set SlotHand");

            if (ControlerShoot && Input.GetKey(KeyCode.Mouse0))
            {
                if (!ControlerUi.InventoryIsOpen) ControlerShoot.Shoot();
            }

            if (Input.GetKey(KeyCode.Mouse1)) Aiming = true;
            if (Input.GetKeyUp(KeyCode.Mouse1)) Aiming = false;
            if (MovePlayer) MovePlayer.Move();

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


            if (PickUpPlayer) PickUpAll();
            if (ControlerDrop && Input.GetKeyDown(KeyCode.Q))
            {
                ControlerDrop.Drop();
                SlotControler.ObjectInHand = null;
                ControlerShoot = null;
                
            }
            
            if (SlotControler) SlotControlerForAll();

            /*
            if (MovePlayer)
            {
                MovePlayer.Move();
                MovePlayer.Jump();
            }
            */

            if (gameobject && Anchor)
            {
                gameobject.transform.position = Anchor.transform.position;

            }
        }

        
    }

    void ShootControlerForAllWeapon()
    {
                
    }

    void PickUpAll()
    {
        PickUpPlayer.RayForLoot();
        PickUpPlayer.ComplertingTheLink();

    }

    void SlotControlerForAll()
    {
        SlotControler.MovingGunForSlots();
        

    }

}
