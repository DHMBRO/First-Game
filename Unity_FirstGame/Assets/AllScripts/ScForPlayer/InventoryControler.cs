using UnityEngine;
//using System.Collections;
using System.Collections.Generic;

public class InventoryControler : MonoBehaviour
{
    [SerializeField] private List<GameObject> InventoryPlayer = new List<GameObject>();

    [SerializeField] public int NumberOfCells = 10;
    [SerializeField] private int i = 0;
    
    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0;i < InventoryPlayer.Count; i++)
        {
             
        }

    }    

    

}
