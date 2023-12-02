using UnityEngine;

public class ControlerAnimationsPlayer : MonoBehaviour
{
    
    [SerializeField] Animator Parameters;
    [SerializeField] PlayerControler ControlerPlayer;

    [SerializeField] float CurrentSpeed;
    [SerializeField] float DesirableSpeed;

    void Start()
    {
        if (!Parameters) 
        {
            Parameters = GetComponent<Animator>();
            Parameters.SetFloat("Speed", 0.0f);

        }
        else Parameters.SetFloat("Speed", 0.0f);

    }

    void Update()
    {
        switch (ControlerPlayer.MovementMode)
        {
            case ModeMovement.Null:
                DesirableSpeed = 0.0f;
                break;
            case ModeMovement.Go:
                DesirableSpeed = 0.5f;
                break;
            case ModeMovement.Aiming:
                break;
            case ModeMovement.Run:
                DesirableSpeed = 1.0f;
                break;
            case ModeMovement.Stels:
                break;
            default:
                break;
        }

        if (CurrentSpeed > DesirableSpeed) CurrentSpeed -= 0.01f;
        else if (CurrentSpeed < DesirableSpeed) CurrentSpeed += 0.01f;

        Parameters.SetFloat("Speed", CurrentSpeed);

    }
}
