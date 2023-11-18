using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List <ScrSaveAndGiveInfo> InfoForSlots = new List<ScrSaveAndGiveInfo>();    
    
    [SerializeField] public Sprite None;
    
    [SerializeField] public GameObject BackPack;
    [SerializeField] public BackPackContorler BackPackPlayer;

    [SerializeField] public float SimpleMaxMass = 5.0f;
    [SerializeField] public float MaxMass;
    [SerializeField] public float CurrentMass = 0.0f;


    private void Start()
    {
        MaxMass = SimpleMaxMass;
        if(BackPack) BackPackPlayer = BackPack.GetComponent<BackPackContorler>();
    }

    public void ChargingValueMaxMass(BackPackContorler BackPackPlayer)
    {
        this.BackPackPlayer = BackPackPlayer;
        MaxMass = BackPackPlayer.CurrentMaxMass;
    }

    public void ChangeMassInInventory()
    {
        float SumeMass = 0.0f;
        int LastCount = InfoForSlots.Count;

        if (BackPack) BackPackPlayer = BackPack.GetComponent<BackPackContorler>();

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
                    MaxMass = BackPackPlayer.CurrentMaxMass;
                }
            }
        }
        else
        {
            CurrentMass = 0.0f;
            if (BackPackPlayer)
            {
                BackPackPlayer.CurrentMass = 0.0f;
                MaxMass = BackPackPlayer.CurrentMaxMass;
            }
        }


    }


}



