using System.Collections.Generic;
using UnityEngine;

public class LootDroper : MethodsFromDevelopers
{
    HpScript HpScriptBot;
    InfScript InfoScript;

    [SerializeField] List<Transform> AllLoot = new List<Transform>();
    [SerializeField] Transform DropSlot;

    void Start()
    {
        HpScriptBot = GetComponent<HpScript>();
        InfoScript = GetComponent<InfScript>();

        HpScriptBot.StateDelegate += DropLoot;
    }

    void DropLoot(bool Alive)
    {
        AllLoot.Add(InfoScript.Attack.TerroristWeaponScript.transform);


        if (Alive)
        {
            return;
        }

        if (AllLoot.Count > 0 && DropSlot)
        {
            for (int i = 0; i < AllLoot.Count; i++)
            {
                DropObjects(AllLoot[i], DropSlot, false);
                
            }
        }
        
    } 

}
