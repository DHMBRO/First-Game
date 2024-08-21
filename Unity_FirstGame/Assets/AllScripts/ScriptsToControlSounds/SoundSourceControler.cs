using System.Collections.Generic;
using UnityEngine;

public class SoundSourceControler : MonoBehaviour
{
    [SerializeField] AudioSource Source;
    [SerializeField] private List<AudioSource> Clips = new List<AudioSource>();

    void Start()
    {
        Source = GetComponent<AudioSource>();
        
        if(!Source)
        {
            Debug.LogError("Not set AudioSource-Source !" + gameObject.name);
        }
    }

    public List<AudioSource> GetListOfClips()
    {
        List<AudioSource> NewClips = Clips;
        return NewClips;
    }

    public void SetLoop(bool Loop)
    {
        Source.loop = Loop;
    }

    public void SetClip(int CountOfClip)
    {
        Source.clip = Clips[CountOfClip].clip;
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
