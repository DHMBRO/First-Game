using UnityEngine;

public class TestIK : MonoBehaviour
{
    [SerializeField] Animator Animator;
    [SerializeField] Transform TargetPoint;


    void Start()
    {
        Animator = GetComponent<Animator>();
    
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        Animator.SetIKPosition(AvatarIKGoal.RightHand, TargetPoint.position);
        Animator.SetIKRotation(AvatarIKGoal.RightHand, TargetPoint.rotation);
    }


}
