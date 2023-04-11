using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Move1F Move;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private StelsScript StelsScript;

    [SerializeField] private PickUp PickUp;
    [SerializeField] private SlotControler SlotControler;    
    [SerializeField] private ShootControler ShootControler;

    void Start()
    {
        Move = gameObject.GetComponent<Move1F>();
        StelsScript = gameObject.GetComponent<StelsScript>();

        PickUp = gameObject.GetComponent<PickUp>();
        SlotControler = gameObject.GetComponent<SlotControler>();
        ShootControler = gameObject.GetComponent<ShootControler>();
        
    }
    
    void Update()
    {
        Moving();
        PickUpAll();
        SlotControlerForAll();
    }
    
    void Moving()
    {
        Move.Move();
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
