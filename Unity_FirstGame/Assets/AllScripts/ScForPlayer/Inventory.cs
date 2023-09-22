using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List <InfoForLoot> InfoForSlots = new List<InfoForLoot>();    
    
    [SerializeField] public Sprite None;
    
    [SerializeField] public GameObject BackPack;    

    [SerializeField] public float MaxMass = 5.0f;
    [SerializeField] public float CurrentMass = 0.0f;


    private void Start()
    {
        ChargingValueMaxMass();
    }

    public void ChargingValueMaxMass()
    {
        if (BackPack)
        {
            BackPackContorler BackPackContr = BackPack.GetComponent<BackPackContorler>();
            MaxMass = BackPackContr.CurrentMaxMass;
        }
    }

    public void ChangeMassInInventory()
    {
        float SumeMass = 0.0f;
        int LastCount = InfoForSlots.Count;

        if (InfoForSlots.Count > 0)
        {
            for (int i = 0; i < InfoForSlots.Count; i++)
            {
                GameObject Loot = InfoForSlots[i].ObjectToInstantiate;
                ScrForAllLoot ScrLoot = Loot.GetComponent<ScrForAllLoot>();

                SumeMass += ScrLoot.Mass;

                if (i == LastCount)
                {
                    CurrentMass = SumeMass;
                }
            }
        }
        else CurrentMass = 0.0f;
        


    }


}



