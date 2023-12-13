using UnityEngine;


public class ExecutoreScriptToPlayer : MonoBehaviour
{
    [SerializeField] SoundCreatorScript SCScript;
    
    [SerializeField] float DeleyToAddNoice = 0.0f;
    [SerializeField] float TimeToAddNoice = 0.0f;

    [SerializeField] float RadiusNoiceWhenInStels = 2.0f;
    [SerializeField] float RadiusNoiceWhenAiming = 5.0f;
    [SerializeField] float RadiusNoiceWhenGo = 10.0f;
    [SerializeField] float RadiusNoiceWhenRun = 15.0f;
    //
    [SerializeField] Transform ZoneNoice;
    [SerializeField] bool ShowRadiuse;

    private void Start()
    {
        SCScript = GetComponent<SoundCreatorScript>();
        
    }

    public void ExecutoreNoice(ModeMovement MovementMode)
    {
        if (Time.time >= TimeToAddNoice)
        {
            float RadiusNoice = 0.1f;
            
            switch (MovementMode)
            {
                case ModeMovement.Stelth:
                    RadiusNoice = RadiusNoiceWhenInStels;
                    break;
                case ModeMovement.Aiming:
                    RadiusNoice = RadiusNoiceWhenAiming;
                    break;
                case ModeMovement.Go:
                    RadiusNoice = RadiusNoiceWhenGo;
                    break;
                case ModeMovement.Run:
                    RadiusNoice = RadiusNoiceWhenRun;
                    break;

            }
            
            //Show Noice Zone
            if (ZoneNoice) ZoneNoice.gameObject.SetActive(ShowRadiuse);
            if (ShowRadiuse && ZoneNoice) 
            {
                ZoneNoice.localScale = new Vector3(RadiusNoice, RadiusNoice, RadiusNoice);
                ZoneNoice.position = transform.position;
            }
            //
            TimeToAddNoice = Time.time + DeleyToAddNoice;
            //SCScript.NoiseRadius = RadiusNoice;
            //
            SCScript.CreateNoise(RadiusNoice);
            //Debug.Log("ExecutoreNoice is work");

        }


    }

}
