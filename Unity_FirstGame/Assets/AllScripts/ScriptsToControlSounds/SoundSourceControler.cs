using UnityEngine;

public class SoundSourceControler : MonoBehaviour
{
    [SerializeField] AudioSource Source;
    
    void Start()
    {
        Source = GetComponent<AudioSource>();
        
        if(!Source)
        {
            Debug.LogError("Not set AudioSource-Source !" + gameObject.name);
        }
    }

    public void SetLoop(bool Loop)
    {
        Source.loop = Loop;
    }

    public void PlaySound()
    {
        Source.Play();
    }

    public void StopPlaySound()
    {
        Source.Stop();
    }

}
