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

    public void PlaySound()
    {
        Source.loop = true;
        Source.Play();
    }

    public void StopPlaySound()
    {
        Source.Stop();
    }

}
