using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List <InfoForLoot> InfoForSlots = new List<InfoForLoot>();    
    
    [SerializeField] public Sprite None;
    
    [SerializeField] public GameObject BackPack;
    [SerializeField] public BackPackContorler BackPackPlayer;

    [SerializeField] public float MaxMass = 5.0f;
    [SerializeField] public float CurrentMass = 0.0f;


    private void Start()
    {
        if(BackPack) BackPackPlayer = BackPack.GetComponent<BackPackContorler>();


        ChargingValueMaxMass();
    }

    public void ChargingValueMaxMass()
    {
        if (BackPackPlayer)
        {
            MaxMass = BackPackPlayer.CurrentMaxMass;
        }
    }

    public void ChangeMassInInventory()
    {
        float SumeMass = 0.0f;
        int LastCount = InfoForSlots.Count;

        if (BackPack && !BackPackPlayer) BackPackPlayer = BackPack.GetComponent<BackPackContorler>();

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
                    BackPackPlayer.CurrentMass = CurrentMass;
                }
            }
        }
        else
        {
            CurrentMass = 0.0f;
            BackPackPlayer.CurrentMass = 0.0f;
        }


    }


}



