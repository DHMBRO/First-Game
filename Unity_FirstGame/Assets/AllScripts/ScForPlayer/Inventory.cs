using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<GameObject> SlotsForBackPack = new List<GameObject>();
    [SerializeField] public GameObject BackPack;

    [SerializeField] public GameObject a;

    [SerializeField] public float MaxMass = 5.0f;
    [SerializeField] public float CurrentMass = 0.0f;

    private void Start()
    {
        ChargingValueMaxMass();


    }

    void Update()
    {
        


    }
    
    public void ChargingValueMaxMass()
    {
        if (BackPack)
        {
            BackPackContorler BackPackContr = BackPack.GetComponent<BackPackContorler>();

            if (BackPackContr.LevelBackPack == 1) MaxMass = 12.0f;
            else if (BackPackContr.LevelBackPack == 2) MaxMass = 17.0f;
            else if (BackPackContr.LevelBackPack == 3) MaxMass = 25.0f;
        }
    }
}
