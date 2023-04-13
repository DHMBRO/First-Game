using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAmmo : MonoBehaviour
{
    [SerializeField] public float Mass;
    

    void Start()
    {
        if (gameObject.CompareTag("Ammo9MM"))
        {
            Mass = 0.35f;
        }
        else if (gameObject.CompareTag("Ammo45_APC"))
        {
            Mass = 0.5f;
        }
        else if (gameObject.CompareTag("Ammo5_56MM"))
        {
            Mass = 1.0f;
        }    
        else if (gameObject.CompareTag("Ammo7_62MM"))
        {
            Mass = 1.5f;
        }        
    }

    void Update()
    {
        
    }
}
