using UnityEngine;

public class ExecutoreScriptToRock : MonoBehaviour
{
    [SerializeField] SoundCreatorScript SoundScript;

    [SerializeField] bool DestroyOnTouch = true;
    //bool IsWorked = false;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (!DestroyOnTouch) return;
        
        SoundScript = gameObject.GetComponent<SoundCreatorScript>();
        if (SoundScript) SoundScript.CreateNoise();

        //Debug.Log("OnColisionIs work");
        //IsWorked = true;
        
        if(DestroyOnTouch) Destroy(gameObject);


    }

}
