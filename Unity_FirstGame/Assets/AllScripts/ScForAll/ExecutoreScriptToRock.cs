using UnityEngine;

public class ExecutoreScriptToRock : MonoBehaviour
{
    [SerializeField] SoundCreatorScript SoundScript;
    [SerializeField] float RadiuseNoice = 10.0f;

    [SerializeField] bool DestroyOnTouch = true;
    //bool IsWorked = false;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (!DestroyOnTouch) return;
        
        SoundScript = gameObject.GetComponent<SoundCreatorScript>();
        if (SoundScript) SoundScript.CreateNoise(RadiuseNoice);

        //Debug.Log("OnColisionIs work");
        //IsWorked = true;
        
        Destroy(gameObject);


    }

}
