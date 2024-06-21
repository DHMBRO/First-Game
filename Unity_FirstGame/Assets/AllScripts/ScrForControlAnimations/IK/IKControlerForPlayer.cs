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

    public void SetSetupIKReferences()
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

    private void Update()
    {
        if (PlayerPivot)
        {
            //OffSetRotationRightArm = Quaternion.LookRotation((ContrlerAim.GetLookPoint() - PlayerPivot.localPosition));
            //OffSetRotationRightArm.eulerAngles = new Vector3(OffSetRotationRightArm.eulerAngles.x, OffSetRotationRightArm.eulerAngles.y, -90.0f);
        }
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!LeftArmPosition)
        {
            Debug.Log("Not set LeftArmPosition !");

            //PlayerControlerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
            //PlayerControlerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);

            return;
        }

        //Debug.Log("1");

        PlayerControlerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        PlayerControlerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        
        PlayerControlerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        PlayerControlerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);


        //Aiming of weapon
        if (ControlerPlayer.StateCamera == CameraPlayer.Aiming)
        {
            //Debug.Log("2");

            if (LeftArmPosition)
            {
                //LeftHandPoint.position = LeftArmPosition.position;
                //LeftHandPoint.rotation = LeftArmPosition.rotation;

                PlayerControlerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, LeftArmPosition.position);
                PlayerControlerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, LeftArmPosition.rotation);

                //Debug.Log(LeftArmPosition.position);
            }

            PlayerControlerAnimator.SetIKPosition(AvatarIKGoal.RightHand, ControlerAim.GetRightHandPoint());
            PlayerControlerAnimator.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Euler(ControlerAim.GetRightHandRotation()));
        }


    }

}
