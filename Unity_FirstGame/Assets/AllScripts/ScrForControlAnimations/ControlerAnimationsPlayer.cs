using UnityEngine;

public class ControlerAnimationsPlayer : MonoBehaviour
{
    //Other components
    [SerializeField] Animator Parameters;
    [SerializeField] PlayerControler ControlerPlayer;
    [SerializeField] Rigidbody RigPlayer;

    //References to additional objects
    [SerializeField] Transform BaseBodyPlayerForAnimations;
    
    //Parameters
    [SerializeField] float CurrentSpeed;
    [SerializeField] float DesirableSpeed;

    //Additional parameters to work 
    [SerializeField] float RotateBaseBodyWhenAiming;


    void Start()
    {
        Parameters = GetComponent<Animator>();
        RigPlayer = ControlerPlayer.GetComponent<Rigidbody>();

        if (Parameters) Parameters.SetFloat("Speed", 0.0f);
        else Parameters.SetFloat("Speed", 0.0f);

    }

    void Update()
    {
        //Type date for work
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        //Vector3 HowRotateBasePlayer;
        //

        //Speed Movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) DesirableSpeed = 0.5f;
        else DesirableSpeed = 0.0f;
        if (Input.GetKey(KeyCode.LeftShift)) DesirableSpeed= 1.0f;

        if (CurrentSpeed > DesirableSpeed) CurrentSpeed -= 0.01f;
        if (CurrentSpeed < DesirableSpeed) CurrentSpeed += 0.01f;

        //Change the float parameters
        Parameters.SetFloat("Speed", CurrentSpeed); //Current speed player
        Parameters.SetFloat("SpeedHorizontal", Horizontal); 
        Parameters.SetFloat("SpeedVertical", Vertical);

        //Change the bool parameters
        Parameters.SetBool("IsAiming", ControlerPlayer.Aiming); 
        Parameters.SetBool("HaveWeaponInHand", ControlerPlayer.HaveWeaponInHand);
        Parameters.SetBool("HavePistolInHand", ControlerPlayer.HavePistolInHand);
        Parameters.SetBool("InStealth", ControlerPlayer.InStealth);

        if (ControlerPlayer.Aiming || ControlerPlayer.InStealth)
        {
            BaseBodyPlayerForAnimations.transform.localEulerAngles = new Vector3(0.0f, RotateBaseBodyWhenAiming, 0.0f);
        }
        if(Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift)) 
            BaseBodyPlayerForAnimations.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);



    }
}
