using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorControler : MonoBehaviour
{
    [SerializeField] public int LevelArmor;

    [SerializeField] public Transform SlotShop01;
    [SerializeField] public Transform SlotShop02;
    [SerializeField] public Transform SlotShop03;
    [SerializeField] public Transform SlotBackPack;

    [SerializeField] public Transform SlotPistol01;
    [SerializeField] public Transform SlotKnife01;

    public bool Use;

    private void Start()
    {
        if (LevelArmor == 1)
        {

        }
        else if (LevelArmor == 2)
        {

        }
        else if (LevelArmor == 3)
        {

        }

    }


    void Update()
    {
            
    }
}
