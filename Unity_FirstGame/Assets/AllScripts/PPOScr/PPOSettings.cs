using UnityEngine;

public class PPOSettings : SingleInteractionActor
{
    [SerializeField] bool PPOIsEnable = true;

    [SerializeField] public float PPORotationSpeed;
    [SerializeField] setPPORotationSpeed SetPPORotationSpeed;
    
    [SerializeField] public float BulletSpeed;
    [SerializeField] setBulletSpeed SetBulletSpeed;

    enum setPPORotationSpeed
    {
        Yes,
        No
    }
    enum setBulletSpeed
    {
        Yes,
        No
    }

    
    public bool ReturnPPOIsEnable()
    {
        return PPOIsEnable;
    }

    override protected void CustomInteraction()
    {
        PPOIsEnable = false;
    }

    void Update()
    {
        if (SetBulletSpeed == setBulletSpeed.No)
        {
            BulletSpeed = 0f;
        }
        if (SetPPORotationSpeed == setPPORotationSpeed.No)
        {
            PPORotationSpeed = 0f;
        }
        Debug.Log("PPORotationSpeed = " + PPORotationSpeed + "; BulletSpeed = " + BulletSpeed);
    }
    
}
