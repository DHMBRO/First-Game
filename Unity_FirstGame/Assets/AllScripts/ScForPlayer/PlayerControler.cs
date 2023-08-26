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

    [SerializeField] private UiControler ControlerUi;

    [SerializeField] Transform gameobject;
    [SerializeField] Transform Anchor;

    [SerializeField] public bool Aiming = false;

    void Start()
    {
        Move = gameObject.GetComponent<Move1F>();
        
        MovePlayer = gameobject.GetComponent<MovePlayer>();
        MovePlayer.ControlerPlayer = GetComponent<PlayerControler>();

        StelsScript = gameObject.GetComponent<StelsScript>();

        PickUpPlayer = gameObject.GetComponent<PickUp>();
        ControlerDrop = gameobject.GetComponent<DropControler>();
        SlotControler = gameObject.GetComponent<SlotControler>();
        
    }
    
    void Update()
    {   
        if(ControlerUi && ControlerUi.InventoryIsOpen == false || !ControlerUi)
        {
            if (SlotControler.ObjectInHand) ControlerShoot = SlotControler.ObjectInHand.GetComponent<ShootControler>();
            else ControlerShoot = null;

            if (ControlerShoot && Input.GetKey(KeyCode.Mouse0))
            {
                if (!ControlerUi.InventoryIsOpen) ControlerShoot.Shoot();
            }

            
            if (Input.GetKey(KeyCode.Mouse1)) Aiming = true;
            if (Input.GetKeyUp(KeyCode.Mouse1)) Aiming = false;
            if (MovePlayer) MovePlayer.Move();



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
