using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCreatorScript : MonoBehaviour
{
    [SerializeField] protected float NoiseRadius;

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
           GameObject ColiderObject = Colider.gameObject;
            SoundTakerScript SoundScript = ColiderObject.GetComponentInParent<SoundTakerScript>();
            if (SoundScript && ColiderObject)
            {
                SoundScript.TakeSound(ColiderObject.transform.position);

            }
        }


    }
}
