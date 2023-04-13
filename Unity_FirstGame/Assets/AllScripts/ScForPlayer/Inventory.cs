using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<GameObject> SlotsForBackPack= new List<GameObject>();  

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0;i < SlotsForBackPack.Count; i++)
        {

        }
    }
}
