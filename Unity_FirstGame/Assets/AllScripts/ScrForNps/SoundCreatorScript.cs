using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCreatorScript : MonoBehaviour
{
    [SerializeField] public float NoiseRadius = 15.0f;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void CreateNoise()
    {
        Collider[] Colliders;
        Colliders = Physics.OverlapSphere(gameObject.transform.position, NoiseRadius);
        foreach(Collider Colider in Colliders)
        {
            if (Colider.gameObject == gameObject)
            {
                continue;
            }
            SoundTakerScript SoundScript = Colider.gameObject.GetComponentInParent<SoundTakerScript>();
            if (SoundScript)
            {
                SoundScript.TakeSound(gameObject.transform.position);
            }
        }
        Debug.Log("Sound Created");

    }
}
