using UnityEngine;

public class IKControlerForPlayer : MonoBehaviour
{
    [SerializeField] Transform PlayerPivot = null;
    AimControler ContrlerAim;

    Animator PlayerControlerAnimator;
    PlayerControler ControlerPlayer;
    ShootControler ControlerShoot; 

    private Transform LeftArmPosition;
    private Quaternion OffSetRotationRightArm = new Quaternion();

    void Start()
    {
        ControlerPlayer = GetComponentInParent<PlayerControler>();
        ContrlerAim = GetComponentInParent<AimControler>();
        PlayerControlerAnimator = GetComponent<Animator>(); ;
    }

    public void SetSetupIKReferences()
    {
        if (!ControlerPlayer)
        {
            return;
        }

        if (ControlerPlayer.ControlerShoot)
        {
            ControlerShoot = ControlerPlayer.ControlerShoot;
            LeftArmPosition = ControlerPlayer.ControlerShoot.LeftArmPositionIK;
        }
        else LeftArmPosition = null;

    }

    private void Update()
    {
        if (PlayerPivot)
        {
            OffSetRotationRightArm = Quaternion.LookRotation((ContrlerAim.GetLookPoint() - PlayerPivot.localPosition));
            OffSetRotationRightArm.eulerAngles = new Vector3(OffSetRotationRightArm.eulerAngles.x, OffSetRotationRightArm.eulerAngles.y, -90.0f);
        }
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!LeftArmPosition)
        {
            PlayerControlerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
            PlayerControlerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);

            return;
        }

        PlayerControlerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);

        PlayerControlerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        PlayerControlerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        PlayerControlerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, LeftArmPosition.position);
        PlayerControlerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, LeftArmPosition.rotation);

        PlayerControlerAnimator.SetIKRotation(AvatarIKGoal.RightHand, OffSetRotationRightArm);

    }

}
