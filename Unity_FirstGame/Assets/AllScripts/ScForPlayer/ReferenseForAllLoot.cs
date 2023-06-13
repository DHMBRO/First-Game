using UnityEngine;
using System.Collections.Generic;


public class ReferenseForAllLoot : MonoBehaviour
{


    [SerializeField] public Dictionary<int, GameObject> ValueLoots = new Dictionary<int, GameObject>();
    [SerializeField] public List<GameObject> ReferencePrefabs = new List<GameObject>();
    

    void Start()
    {
        for (int i = 0; i < ReferencePrefabs.Count; i++)
        {
            if (ReferencePrefabs[i].gameObject/*.tag == "Ammo9MM"*/)
            {
                ValueLoots.Add(i,ReferencePrefabs[i]);
            }
            /*
            else if (ReferencePrefabs[i].gameObject.tag == "Ammo45_APC")
            {
                ValueLoots.Add(1, ReferencePrefabs[i]);
            }
            else if (ReferencePrefabs[i].gameObject.tag == "Ammo5_56MM")
            {
                ValueLoots.Add(2, ReferencePrefabs[i]);
            }
            else if (ReferencePrefabs[i].gameObject.tag == "Ammo7_62MM")
            {
                ValueLoots.Add(3, ReferencePrefabs[i]);
            } 
            */
        }
        
    }

    void Update()
    {
        
    }
    

}
