using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<GameObject> SlotsForBackPack = new List<GameObject>();
    


    [SerializeField] public float MaxMass = 10.0f;
    [SerializeField] public float CurrentMass = 0.0f;
        
    void Update()
    {
        
    }
}
