using UnityEngine;
using System.Collections.Generic;

public class InventoryControler : MonoBehaviour
{
    [SerializeField] public List<GameObject> A = new List<GameObject>();

    [SerializeField] public float MaxMass = 100.0f;
    [SerializeField] public float CurrentMass = 0.0f;     
    


    void Update()
    {
        
    }
}
