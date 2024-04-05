using UnityEngine;

public class ExecutoreScrForWeapons : MonoBehaviour
{
    SoundCreatorScript SCSript;
    ShootControler ControlerShoot;

    [SerializeField] float RadiuseNoise = 5.0f;
    [SerializeField] float DeleayToDisable = 0.25f;
    
    [SerializeField] GameObject ZoneNoise;
    Transform LocalZoneNoise;
    
    [SerializeField] bool ShowZoneNoise = false;

    void Start()
    {
        SCSript = GetComponent<SoundCreatorScript>();
        ControlerShoot = GetComponent<ShootControler>();

        ControlerShoot.SetShootDelegat += ExecutoreNoise;
                
    }

    void ExecutoreNoise()
    {
        if (ShowZoneNoise && ZoneNoise)
        {
            if (!LocalZoneNoise) LocalZoneNoise = Instantiate(ZoneNoise).transform;

            LocalZoneNoise.position = ControlerShoot.WeaponMuzzle();
            LocalZoneNoise.localScale = new Vector3(RadiuseNoise, RadiuseNoise, RadiuseNoise) * 2.0f;

            LocalZoneNoise.gameObject.SetActive(true);
            Invoke("DisactiveZoneNoice", DeleayToDisable);
        }
        else if (!ZoneNoise) Debug.Log("Not set ZoneNoise");

        SCSript.CreateNoise(RadiuseNoise);
        
    }

    void DisactiveZoneNoice()
    {
        LocalZoneNoise.gameObject.SetActive(false);
    }


}
