using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class AimControler : MonoBehaviour
{
    [SerializeField] Transform PlayerCamera;
    
    [SerializeField] Transform CurrentSlotHand;
    [SerializeField] public Transform WeaponMuzzle;

    [SerializeField] float MaxDistanceEyes = 100.0f;
    [SerializeField] Color RayMuzzleColor;
    [SerializeField] bool CanAim = true;

    [SerializeField] Transform RightArmAnimation;
    [SerializeField] Transform RightArm;
    
    [SerializeField] Vector3 RightHandPoint;
    [SerializeField] Vector3 LeftHandPoint;
    [SerializeField] Transform RightHandAnimation;

    RaycastHit[] HitPoints = new RaycastHit[10];
    RaycastHit SelectedPoint;

    SlotControler ControlerSlot;
    PlayerControler ControlerPlayer;

    

    void Start()
    {
        ControlerSlot = GetComponent<SlotControler>();
        ControlerPlayer = GetComponent<PlayerControler>();

        CurrentSlotHand = ControlerSlot.CurrentSlotHand;
    }

    private void Update()
    {
        if (ControlerPlayer.ControlerShoot && RightArm)
        {
            ShootControler CurrentWeapon = ControlerPlayer.ControlerShoot.GetComponent<ShootControler>();

            RightArm.position = RightArmAnimation.position;
            RightArm.eulerAngles = new Vector3(PlayerCamera.eulerAngles.x, RightArm.eulerAngles.y, RightArm.eulerAngles.z);

            RightHandPoint = RightArmAnimation.position;
            LeftHandPoint = CurrentWeapon.transform.position;

            // Right hand
            RightHandPoint += RightArm.forward * CurrentWeapon.ShoulderOffSet.z;
            RightHandPoint += RightArm.right * CurrentWeapon.ShoulderOffSet.x;
            RightHandPoint += RightArm.up * CurrentWeapon.ShoulderOffSet.y;

            // Left hand
            LeftHandPoint += CurrentWeapon.transform.forward * CurrentWeapon.LeftHandOffSet.z;
            LeftHandPoint += CurrentWeapon.transform.right * CurrentWeapon.LeftHandOffSet.x;
            LeftHandPoint += CurrentWeapon.transform.up * CurrentWeapon.LeftHandOffSet.y;

        }

    }

    private Vector3 GetEndPositionForMuzzleOfWeapon()
    {
        return SelectedPoint.point;
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

        if(CurrentSlotHand) CurrentSlotHand.localEulerAngles = new Vector3(-90.0f, 0.0f, 90.0f);

        CurrentSlotHand = ControlerSlot.CurrentSlotHand;
    }

    public void EliminateReferenceWeapoinMuzzle()
    {
        WeaponMuzzle = null;
    }

    public Vector3 GetRightHandPosition()
    {
        return RightHandPoint;
    }

    public Vector3 GetLeftHandPosition()
    {
        return LeftHandPoint;
    }

    public Vector3 GetRightHandRotation()
    {
        return new Vector3(PlayerCamera.eulerAngles.x, PlayerCamera.eulerAngles.y, -90.0f);
    }

    public Vector3 GetLeftHandRotation()
    {
        if (ControlerPlayer.ControlerShoot)
        {
            return ControlerPlayer.ControlerShoot.LeftHandOffSetEulerAngels + ControlerPlayer.ControlerShoot.transform.eulerAngles;
        }
        else
        {
            return Vector3.zero;
        }

    }    
   

    public void Aim()
    {
        if (!WeaponMuzzle || !CanAim)
        {
            return;
        }

        Vector3 DirectionWeapon = CurrentSlotHand.eulerAngles;
        SelectedPoint = new RaycastHit();

        HitPoints = Physics.RaycastAll(PlayerCamera.position, PlayerCamera.forward, MaxDistanceEyes);
        List<RaycastHit> ValidValues = new List<RaycastHit>();

        for (int i = 0; i < HitPoints.Length; i++)
        {
            if (HitPoints[i].collider != null && HitPoints[i].collider.isTrigger == false)
            {
                ValidValues.Add(HitPoints[i]);
            }
        }

        for (int i = 0; i < ValidValues.Count; i++)
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
            SelectedPoint.point = PlayerCamera.position + PlayerCamera.forward * MaxDistanceEyes;
            WeaponMuzzle.LookAt(SelectedPoint.point);
        }

        if (ControlerPlayer.ControlerShoot.ControelrLaser != null)
        {
            ControlerPlayer.ControlerShoot.ControelrLaser.LaserEndPoint = SelectedPoint.point;
        }

        //Additioanll
        CurrentSlotHand.eulerAngles = new Vector3(CurrentSlotHand.eulerAngles.x, DirectionWeapon.y, DirectionWeapon.z);
        
        Vector3 Origin = PlayerCamera.position + (PlayerCamera.forward * PlayerCamera.GetComponent<ThirdPersonCamera>().CurrentMoveBackDistance);
        Vector3 Direction = PlayerCamera.forward * 100.0f;
        
        Debug.DrawRay(Origin, Direction, Color.red);
        Debug.DrawRay(WeaponMuzzle.transform.position, WeaponMuzzle.transform.forward * MaxDistanceEyes, Color.yellow);
        Debug.DrawRay(WeaponMuzzle.transform.position, CurrentSlotHand.transform.forward * MaxDistanceEyes, Color.green);
        
    }

    public void StopAim()
    {
        if (ControlerPlayer.ControlerShoot && ControlerPlayer.ControlerShoot.ControelrLaser != null)
        {
            ControlerPlayer.ControlerShoot.ControelrLaser.LaserEndPoint = Vector3.zero;
        }
        
    }

}
