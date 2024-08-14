using UnityEngine;

public class SoundSourceControler : MonoBehaviour
{
    [SerializeField] AudioSource Source;

    void Start()
    {
        Source = GetComponent<AudioSource>();
        
        if (Source)
        {
            Source.loop = true;
            Source.Play();

        }
        else
        {
            Debug.LogError("Not set AudioSource-Source !" + gameObject.name);
        }

    }


    public void StopPlaySound()
    {
        Source.Stop();
    }

}
