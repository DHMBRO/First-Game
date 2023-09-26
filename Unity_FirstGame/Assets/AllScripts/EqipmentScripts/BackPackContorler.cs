using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackContorler : MonoBehaviour
{
    [SerializeField] public int LevelBackPack;
    [SerializeField] public float CurrentMaxMass;
    [SerializeField] public float CurrentMass;

    [SerializeField] float MaxMassTo1L = 12.0f;
    [SerializeField] float MaxMassTo2L = 17.0f;
    [SerializeField] float MaxMassTo3L = 25.0f;

    
    void Start()    
    {
        if (LevelBackPack == 1) CurrentMaxMass = MaxMassTo1L;
        else if (LevelBackPack == 2) CurrentMaxMass = MaxMassTo2L;
        else if (LevelBackPack == 3) CurrentMaxMass = MaxMassTo3L;
    }
    
    
}
