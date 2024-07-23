using UnityEngine;

public class IKControlerForPlayer : MonoBehaviour
{
    [SerializeField] Transform PlayerPivot = null;
    AimControler ControlerAim;

    Animator PlayerControlerAnimator;
    PlayerControler ControlerPlayer;
    ShootControler ControlerShoot; 

    [SerializeField] private Transform LeftArmPosition;
    [SerializeField] private Transform LeftHandPoint;
    [SerializeField] private float CurrentWeight = 1.0f;
    [SerializeField] private Transform ParentPointObject;
    //private Quaternion OffSetRotationRightArm = new Quaternion();

    void Start()
    {
        ControlerPlayer = GetComponentInParent<PlayerControler>();
        ControlerAim = GetComponentInParent<AimControler>();
        PlayerControlerAnimator = GetComponent<Animator>(); ;
    }

    public void SetupIKReferences()
    {
        if (!ControlerPlayer)
        {
            return;
        }

        if (ControlerPlayer.ControlerShoot)
        {
            ControlerShoot = ControlerPlayer.ControlerShoot;
            LeftArmPosition = ControlerShoot.LeftArmPositionIK;
        }
        else LeftArmPosition = null;

    }

    public void SetupIKWeight(float Value)
    {
        CurrentWeight = Value;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!LeftArmPosition)
        {
            return;
        }

        PlayerControlerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, CurrentWeight);
        PlayerControlerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, CurrentWeight);

        PlayerControlerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, CurrentWeight);
        PlayerControlerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, CurrentWeight);


        PlayerControlerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Euler(ControlerAim.GetLeftHandRotation()));
        PlayerControlerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, ControlerAim.GetLeftHandPosition());

        //Aiming of weapon
        if (ControlerPlayer.StateCamera == CameraPlayer.Aiming)
        {
            
            PlayerControlerAnimator.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Euler(ControlerAim.GetRightHandRotation()));
            PlayerControlerAnimator.SetIKPosition(AvatarIKGoal.RightHand, ControlerAim.GetRightHandPosition());
            
        }


    }

}
