using UnityEngine;

public class IKControlerForPlayer : MonoBehaviour
{
    Animator PlayerControlerAnimator;
    PlayerControler ControlerPlayer;
    
    [SerializeField] Transform LeftArmPosition;
    
    void Start()
    {
        ControlerPlayer = GetComponentInParent<PlayerControler>();
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
            LeftArmPosition = ControlerPlayer.ControlerShoot.LeftArmPositionIK;
        }
        else LeftArmPosition = null;

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

        PlayerControlerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, LeftArmPosition.position);
        PlayerControlerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, LeftArmPosition.rotation);

    }

}
