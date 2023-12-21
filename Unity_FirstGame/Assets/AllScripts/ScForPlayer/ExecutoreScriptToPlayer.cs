using UnityEngine;


public class ExecutoreScriptToPlayer : MonoBehaviour
{
    [SerializeField] SoundCreatorScript SCScript;
    
    [SerializeField] float DeleyToAddNoise = 0.25f;
    [SerializeField] float TimeToAddNoise = 0.0f;

    [SerializeField] float RadiusNoiseWhenInStels = 0.5f;
    [SerializeField] float RadiusNoiseWhenAiming = 3.0f;
    [SerializeField] float RadiusNoiseWhenGo = 5.0f;
    [SerializeField] float RadiusNoiseWhenRun = 7.0f;
    //
    [SerializeField] GameObject ZoneNoise;
    [SerializeField] GameObject LocalZoneNoise;
    [SerializeField] bool ShowZoneNoise;


    private void Start()
    {
        SCScript = GetComponent<SoundCreatorScript>();
        
    }

    public void ExecutoreNoice(ModeMovement MovementMode)
    {
        if (Time.time >= TimeToAddNoise)
        {
            float RadiusNoise = 0.0f;
            
            switch (MovementMode)
            {
                case ModeMovement.Stelth:
                    RadiusNoise = RadiusNoiseWhenInStels;
                    break;
                case ModeMovement.Aiming:
                    RadiusNoise = RadiusNoiseWhenAiming;
                    break;
                case ModeMovement.Go:
                    RadiusNoise = RadiusNoiseWhenGo;
                    break;
                case ModeMovement.Run:
                    RadiusNoise = RadiusNoiseWhenRun;
                    break;
                default:
                    //RadiusNoise = 0.0f;
                    break;

            }
            
            //Show Noice Zone
            if (ShowZoneNoise && ZoneNoise) 
            {
                if(!LocalZoneNoise) LocalZoneNoise = Instantiate(ZoneNoise);
                
                LocalZoneNoise.transform.position = transform.position;
                LocalZoneNoise.transform.localScale = new Vector3(RadiusNoise, RadiusNoise, RadiusNoise) * 2.0f;            
            }
            if (LocalZoneNoise) LocalZoneNoise.gameObject.SetActive(ShowZoneNoise);
            
            //Main
            TimeToAddNoise = Time.time + DeleyToAddNoise;
            SCScript.CreateNoise(RadiusNoise);
            //Debug.Log("ExecutoreNoice is work");

        }


    }

}
