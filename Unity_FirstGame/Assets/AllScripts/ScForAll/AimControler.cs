using System.Collections.Generic;
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

    [SerializeField] Transform TestEndPoint;
    [SerializeField] Transform TestEndPointMuzle;
    [SerializeField] float TestBackDistance = 2.5f;

    RaycastHit[] HitPoints = new RaycastHit[10];

    SlotControler ControlerSlot;
    PlayerControler ControlerPlayer;

    void Start()
    {
        ControlerSlot = GetComponent<SlotControler>();
        ControlerPlayer = GetComponent<PlayerControler>();

        CurrentSlotHand = ControlerSlot.CurrentSlotHand;
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

        //if(CurrentSlotHand) CurrentSlotHand.localEulerAngles = new Vector3(-90.0f, 0.0f, 90.0f);

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
        //Debug.Log("Rotation: " + ControlerPlayer.ControlerShoot.transform.rotation);
        //Debug.Log("Vector.forward: " + ControlerPlayer.ControlerShoot.transform.rotation * Vector3.forward);
        //Debug.Log("EulerAngelse: " + ControlerPlayer.ControlerShoot.transform.eulerAngles);

        return ControlerPlayer.ControlerShoot.LeftHandOffSetEulerAngels + ControlerPlayer.ControlerShoot.transform.eulerAngles;

        /* 
        if (ControlerPlayer.StateCamera == CameraPlayer.Aiming)
        {
            return PlayerCamera.eulerAngles + ControlerPlayer.ControlerShoot.LeftHandOffSetEulerAngels;
        }
        else return ControlerPlayer.ControlerShoot.transform.eulerAngles + ControlerPlayer.ControlerShoot.LeftHandOffSetEulerAngels;


        //return new Vector3(
        //    ControlerPlayer.ControlerShoot.transform.eulerAngles.x /*+  ControlerPlayer.ControlerShoot.LeftHandOffSetEulerAngels.x*/
        //    ControlerPlayer.ControlerShoot.transform.eulerAngles.y /*+ ControlerPlayer.ControlerShoot.LeftHandOffSetEulerAngels.y*/,
        //    ControlerPlayer.ControlerShoot.transform.eulerAngles.z /*+ ControlerPlayer.ControlerShoot.LeftHandOffSetEulerAngels.z*/);
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

    public void Aim()
    {
        if (!WeaponMuzzle || !CanAim)
        {
            return;
        }

        Vector3 DirectionWeapon = CurrentSlotHand.eulerAngles;
        RaycastHit SelectedPoint = new RaycastHit();

        HitPoints = Physics.RaycastAll(PlayerCamera.position, PlayerCamera.forward, MaxDistanceEyes);
        List<RaycastHit> ValidValues = new List<RaycastHit>();

        for (int i = 0; i < HitPoints.Length; i++)
        {
            if (HitPoints[i].collider != null && HitPoints[i].collider.gameObject.layer == LayerMask.GetMask("Hit Box"))
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
            //CurrentSlotHand.LookAt(SelectedPoint.point);
        }
        else
        {
            SelectedPoint.point = PlayerCamera.position + PlayerCamera.forward * MaxDistanceEyes;

            WeaponMuzzle.LookAt(SelectedPoint.point);
            //CurrentSlotHand.LookAt(SelectedPoint.point);
        }

        //Additioanll
        CurrentSlotHand.eulerAngles = new Vector3(CurrentSlotHand.eulerAngles.x, DirectionWeapon.y, DirectionWeapon.z);
        Debug.DrawRay(WeaponMuzzle.transform.position, WeaponMuzzle.transform.forward * MaxDistanceEyes, Color.green);

        Vector3 Origin = PlayerCamera.position + (PlayerCamera.forward * PlayerCamera.GetComponent<ThirdPersonCamera>().CurrentMoveBackDistance);
        Vector3 Direction = PlayerCamera.forward * 100.0f;
        
        Debug.DrawRay(Origin, Direction, Color.blue);

        TestEndPointMuzle.position = WeaponMuzzle.transform.position + WeaponMuzzle.forward * (SelectedPoint.distance - TestBackDistance - (CurrentSlotHand.position - WeaponMuzzle.position).magnitude);
        TestEndPoint.position = CurrentSlotHand.position + CurrentSlotHand.forward * (SelectedPoint.distance - TestBackDistance);
        
    }

}
