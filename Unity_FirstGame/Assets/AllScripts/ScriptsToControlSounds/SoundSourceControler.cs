using System.Collections.Generic;
using UnityEngine;

public class SoundSourceControler : MonoBehaviour
{
    [SerializeField] AudioSource Source;
    [SerializeField] public List<AudioSource> Clips = new List<AudioSource>();

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

    public void SetClip(AudioClip Clip)
    {
        Source.clip = Clip;
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
