using UnityEngine;

public class ExecutoreScrForWeapons : MonoBehaviour
{
    //private SoundCreatorScript SCSript;
    //private bool CanWork = false;


    void Start()
    {
        //SCSript = GetComponent<SoundCreatorScript>();
        GetComponent<ShootControler>().SetShootDelegat += Work;

        //CanWork = true;
    }

    public void Work()
    {
        ExecutoreNoice();
    }

    private void ExecutoreNoice()
    {
        //SCSript
        
    }

}
