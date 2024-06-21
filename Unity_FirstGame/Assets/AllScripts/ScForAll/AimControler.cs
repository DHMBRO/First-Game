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
    [SerializeField] Transform TestRightArm;

    [SerializeField] Vector3 RightHandPoint;
    [SerializeField] Transform RightHandAnimation;
    [SerializeField] Transform TestRigtHand;
    
    [SerializeField] Transform TestObjectPoint;


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

        if(CurrentSlotHand) CurrentSlotHand.localEulerAngles = new Vector3(-90.0f, 0.0f, 90.0f);

        CurrentSlotHand = ControlerSlot.CurrentSlotHand;
    }

    public void EliminateReferenceWeapoinMuzzle()
    {
        WeaponMuzzle = null;
    }

    public Vector3 GetRightHandPoint()
    {
        return RightHandPoint;
    }

    public Vector3 GetRightHandRotation()
    {
        return new Vector3(PlayerCamera.eulerAngles.x, PlayerCamera.eulerAngles.y, -90.0f);
    }

    private void Update()
    {
        if (ControlerPlayer.ControlerShoot && RightArm)
        {
            ShootControler CurrentWeapon = ControlerPlayer.ControlerShoot.GetComponent<ShootControler>();

            RightArm.position = RightArmAnimation.position;
            RightArm.eulerAngles = new Vector3(PlayerCamera.eulerAngles.x, RightArm.eulerAngles.y, RightArm.eulerAngles.z);

            RightHandPoint = RightArmAnimation.position;

            RightHandPoint += RightArm.forward * CurrentWeapon.ShoulderOffSet.z;
            RightHandPoint += RightArm.right * CurrentWeapon.ShoulderOffSet.x;
            RightHandPoint += RightArm.up * CurrentWeapon.ShoulderOffSet.y;

            TestRigtHand.position = RightHandPoint;     

            TestRightArm.position = RightArm.position;
            TestRightArm.localEulerAngles = RightArm.eulerAngles;

             
        }


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
            if (HitPoints[i].collider != null /*&& HitPoints[i].collider.gameObject.layer == LayerMask.GetMask("Hit Box")*/)
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
            //CurrentSlotHand.LookAt(SelectedPoint.point);
            TestObjectPoint.position = SelectedPoint.point;
        }
        else
        {
            SelectedPoint.point = PlayerCamera.position + PlayerCamera.forward * MaxDistanceEyes;

            WeaponMuzzle.LookAt(SelectedPoint.point);
            //CurrentSlotHand.LookAt(SelectedPoint.point);
            TestObjectPoint.position = SelectedPoint.point;
        }
         
        //Additioanll
        CurrentSlotHand.eulerAngles = new Vector3(CurrentSlotHand.eulerAngles.x, DirectionWeapon.y, DirectionWeapon.z);
        Debug.DrawRay(WeaponMuzzle.position, WeaponMuzzle.forward * MaxDistanceEyes, Color.green);
        
    }

}
