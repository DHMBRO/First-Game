using UnityEngine;
using System.Collections.Generic;


public class ReferenseForAllLoot : MonoBehaviour
{


    [SerializeField] public Dictionary<string, GameObject> ValueLoots = new Dictionary<string, GameObject>();
    [SerializeField] public List<GameObject> ReferencePrefabs = new List<GameObject>();
    
    void Start()
    {
        for (int i = 0; i < ReferencePrefabs.Count; i++)
        {
            if (ReferencePrefabs[i].gameObject.tag == "Ammo9MM")
            {
                ValueLoots.Add("9MM",ReferencePrefabs[i]);
            }
            else if (ReferencePrefabs[i].gameObject.tag == "Ammo45_APC")
            {
                ValueLoots.Add("45ACP", ReferencePrefabs[i]);
            }
            else if (ReferencePrefabs[i].gameObject.tag == "Ammo5_56MM")
            {
                ValueLoots.Add("5,56MM", ReferencePrefabs[i]);
            }
            else if (ReferencePrefabs[i].gameObject.tag == "Ammo7_62MM")
            {
                ValueLoots.Add("7,62MM", ReferencePrefabs[i]);
            }                
        }
        
    }

    void Update()
    {
        
    }
    

}
