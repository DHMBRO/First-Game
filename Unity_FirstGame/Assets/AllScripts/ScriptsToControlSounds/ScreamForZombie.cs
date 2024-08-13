using System.Collections.Generic;
using UnityEngine;

public class ScreamForZombie : MonoBehaviour
{
    [SerializeField] AudioSource Source;
    [SerializeField] List<AudioClip> AllScreamsForZombie = new List<AudioClip>();

    [SerializeField] float MinDeleyToPlayClip = 10.0f;
    [SerializeField] float MaxDeleyToPlayClip = 20.0f;
    [SerializeField] float TimeToPlay;
    [SerializeField] bool WorkFromUpdate = false;


    int RandomClip;
    bool CanWork = true;
    
    private void Start()
    {
        Source = GetComponent<AudioSource>();
        
        if (!Source)
        {
            Debug.LogError("Not set AudioSource-Source ! " + gameObject.name);
            CanWork = false;
        }

    }

    private void Update() // This Update only for test
    {
        if (!WorkFromUpdate)
        {
            return;
        }

        ImplementFunction();
    }

    public void ImplementFunction() // Attention, this function you have to call from some Update !     
    {
        if (!CanWork)
        {
            return;
        }

        if (Time.time >= TimeToPlay)
        {
            TimeToPlay = Time.time + Random.Range(MinDeleyToPlayClip, MaxDeleyToPlayClip);
            SelectClipAndPlay();
        }

    }

    private void SelectClipAndPlay()
    {
        if (!Source.isPlaying)
        {
            RandomClip = Random.Range(0, AllScreamsForZombie.Count);
            Source.clip = AllScreamsForZombie[RandomClip];
            Source.Play();
        }
    }

}
