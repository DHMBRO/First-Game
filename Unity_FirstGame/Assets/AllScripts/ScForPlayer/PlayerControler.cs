using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Move1F Move;
    [SerializeField] private MovePlayer MovePlayer;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private StelsScript StelsScript;

    [SerializeField] private PickUp PickUp;
    [SerializeField] private SlotControler SlotControler;    
    [SerializeField] private ShootControler ShootControler;

    [SerializeField] private UiControler ControlerUi;

    [SerializeField] Transform gameobject;
    [SerializeField] Transform Anchor;

    void Start()
    {
        Move = gameObject.GetComponent<Move1F>();
        MovePlayer = gameobject.GetComponent<MovePlayer>();

        StelsScript = gameObject.GetComponent<StelsScript>();

        PickUp = gameObject.GetComponent<PickUp>();
        SlotControler = gameObject.GetComponent<SlotControler>();
        ShootControler = gameObject.GetComponent<ShootControler>();
    }
    
    void Update()
    {   
        if(ControlerUi && ControlerUi.InventoryIsOpen == false)
        {
            if (PickUp) PickUpAll();
            if (Move) Move.Move();
            if (SlotControler) SlotControlerForAll();

            if (MovePlayer)
            {
                MovePlayer.Move();
                MovePlayer.Jump();
            }
            
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
        PickUp.RayForLoot();
        PickUp.ComplertingTheLink();

    }

    void SlotControlerForAll()
    {
        SlotControler.MovingGunForSlots();
        

    }

}
