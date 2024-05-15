using System.Collections.Generic;
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
    RaycastHit[] HitPoints = new RaycastHit[10];


    void Start()
    {
        PlayerTools = GetComponent<PlayerToolsToInteraction>();
        ControlerSlot = GetComponent<SlotControler>();
    }

    public void UpdateWeapoMuzzle()
    {
        
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
        RaycastHit SelectedPoint = new RaycastHit();

        HitPoints = Physics.RaycastAll(PlayerCamera.position, PlayerCamera.forward, MaxDistanceEyes);
        List<RaycastHit> ValidValues = new List<RaycastHit>();

        for (int i = 0; i < HitPoints.Length; i++)
        {
            if (HitPoints[i].collider != null && HitPoints[i].collider.isTrigger == false)
            {
                ValidValues.Add(HitPoints[i]);
                
            }
        }

        for (int i = 0;i < ValidValues.Count;i++)
        {
            if (SelectedPoint.collider == null)
            {
                SelectedPoint = ValidValues[i];
            }
            else
            {
                if ((ValidValues[i].point - WeaponMuzzle.position).magnitude <= (SelectedPoint.point - WeaponMuzzle.position).magnitude)
                {
                    SelectedPoint = ValidValues[i];
                }
            }
        }

        if (SelectedPoint.collider != null)
        {
            WeaponMuzzle.LookAt(SelectedPoint.point);
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
