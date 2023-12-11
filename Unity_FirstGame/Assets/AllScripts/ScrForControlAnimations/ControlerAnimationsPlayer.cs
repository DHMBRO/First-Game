using UnityEngine;

public class ControlerAnimationsPlayer : MonoBehaviour
{
    //Other components
    [SerializeField] Animator Parameters;
    [SerializeField] PlayerControler ControlerPlayer;
    [SerializeField] MovePlayer MovePlayer;
    [SerializeField] Rigidbody RigPlayer;

    //References to additional objects
    [SerializeField] Transform BaseBodyPlayerForAnimations;
    
    //Parameters
    [SerializeField] float CurrentSpeed;
    [SerializeField] float DesirableSpeed;
    
    //Additional parameters to work 
    //[SerializeField] public float CurrentAddedRotateBodyPlayer;

    [SerializeField] float RotateBodyWhenAiming;
    //[SerializeField] float RotateBodyPlayerWhenAimingInStelth;


    void Start()
    {
        Parameters = GetComponent<Animator>();
        RigPlayer = ControlerPlayer.GetComponent<Rigidbody>();
        //MovePlayer = GetComponent<MovePlayer>();

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
        if (Input.GetKey(KeyCode.LeftShift)) DesirableSpeed = 1.0f;

        if (CurrentSpeed > DesirableSpeed) CurrentSpeed -= (Time.deltaTime);
        if (CurrentSpeed < DesirableSpeed) CurrentSpeed += (Time.deltaTime);

        //Change the float parameters
        if (CurrentSpeed > DesirableSpeed) CurrentSpeed -= (Time.deltaTime);
        if (CurrentSpeed < DesirableSpeed) CurrentSpeed += (Time.deltaTime);

        Parameters.SetFloat("CurrentSpeed", CurrentSpeed); //Current speed player
        Parameters.SetFloat("SpeedHorizontal", Horizontal); 
        Parameters.SetFloat("SpeedVertical", Vertical);

        //Change the bool parameters
        Parameters.SetBool("IsAiming", ControlerPlayer.IsAiming); 
        Parameters.SetBool("HaveWeaponInHand", ControlerPlayer.HaveWeaponInHand);
        Parameters.SetBool("HavePistolInHand", ControlerPlayer.HavePistolInHand);
        Parameters.SetBool("InStealth", ControlerPlayer.InStealth);
        Parameters.SetBool("UsingLoot", ControlerPlayer.IsUsingLoot);

        

        //Triggers
        if (ControlerPlayer.IsJuming) 
        {
            Parameters.SetTrigger("Jump");
            Debug.Log("True");
        }



        if (ControlerPlayer.IsAiming || ControlerPlayer.InStealth)
        {
            BaseBodyPlayerForAnimations.transform.localEulerAngles = new Vector3(0.0f, RotateBodyWhenAiming, 0.0f);
        }
        //if (ControlerPlayer.IsAiming && ControlerPlayer.InStealth)
        {
            //BaseBodyPlayerForAnimations.transform.localEulerAngles = new Vector3(0.0f, RotateBodyWhenAiming, 0.0f);
            //BaseBodyPlayerForAnimations.transform.localEulerAngles = new Vector3(0.0f, RotateBodyPlayerWhenAimingInStelth, 0.0f);
            //Debug.Log("Audit 2 is work");
        }
        if (!ControlerPlayer.InStealth && !ControlerPlayer.IsAiming)
        {
            BaseBodyPlayerForAnimations.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        if(Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift)) 
            BaseBodyPlayerForAnimations.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);



    }
}
