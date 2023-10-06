using UnityEngine;

public class ExecutoreScript : MonoBehaviour
{
    [SerializeField] SoundCreatorScript SoundScript;
    [SerializeField] bool IsWorked;
    
    private void OnCollisionEnter(Collision collision)
    {
        SoundScript = gameObject.GetComponent<SoundCreatorScript>();
        if (!SoundScript || IsWorked) return;
        
        Debug.Log("OnColisionIs work");
        SoundScript.CreateNoise();
        IsWorked = true;
    }
    
}
