using UnityEngine;

public class AimControler : MonoBehaviour
{
    [SerializeField] Transform PlayerCamera;
    [SerializeField] float MaxDistanceEyes = 100.0f;

    [SerializeField] Transform ButtSlot;
    [SerializeField] Transform PlayerShoulder;

    [SerializeField] public Transform WeaponMuzzle;
    [SerializeField] Color RayMuzzleColor;

    PlayerToolsToInteraction PlayerTools;
    SlotControler ControlerSlot;

    [SerializeField] bool CanAim = true;

    [SerializeField] Vector3 SelectedPoint = new Vector3();
    RaycastHit[] HitPoints = new RaycastHit[10];


    void Start()
    {
        PlayerTools = GetComponent<PlayerToolsToInteraction>();
        ControlerSlot = GetComponent<SlotControler>();
    }

    public void UpdateWeapoMuzzle()
    {
        Debug.Log("UpdateWeapoMuzzle is work");

        if (!ControlerSlot.ObjectInHand)
        {
            if (WeaponMuzzle)
            {
                WeaponMuzzle.eulerAngles = Vector3.zero;
            }
            WeaponMuzzle = null;
        }
        else if (ControlerSlot.ObjectInHand.GetComponent<ShootControler>())
        {
            WeaponMuzzle = ControlerSlot.ObjectInHand.GetComponent<ShootControler>().Muzzle.transform;
        }
        else
        {
            WeaponMuzzle = null;
        }
    }

    public void EliminateReferenceWeapoinMuzzle()
    {
        WeaponMuzzle = null;
    }

    private void Update()
    {
        if (PlayerShoulder && ButtSlot)
        {
            ButtSlot.position = PlayerShoulder.position;
        }
        else Debug.Log("Not set ButtSlot or PlayerShoulder");

        if (!WeaponMuzzle || !CanAim)
        {
            return;
        }

        
        Vector3 DirectionWeapon = ButtSlot.eulerAngles;
        SelectedPoint = Vector3.zero;

        HitPoints = Physics.RaycastAll(PlayerCamera.position, PlayerCamera.forward, MaxDistanceEyes);

        for (int i = HitPoints.Length - 1; i > -1; i--)
        {
            if (HitPoints[i].collider != null && HitPoints[i].collider.isTrigger == false)
            {
                SelectedPoint = HitPoints[i].point;
                break;
            }
        }

        if (SelectedPoint != Vector3.zero)
        {
            WeaponMuzzle.LookAt(SelectedPoint);
        }
        else
        {
            WeaponMuzzle.LookAt(PlayerCamera.position + PlayerCamera.forward * MaxDistanceEyes);
        }

        ButtSlot.LookAt(PlayerCamera.position + PlayerCamera.forward * MaxDistanceEyes);


        //Additioanll
        ButtSlot.eulerAngles = new Vector3(ButtSlot.eulerAngles.x, DirectionWeapon.y, DirectionWeapon.z);
        Debug.DrawRay(WeaponMuzzle.position, WeaponMuzzle.forward * MaxDistanceEyes, Color.green);
        
    }


}
