using UnityEngine;

public class MainAmbientControler : MonoBehaviour
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


}
