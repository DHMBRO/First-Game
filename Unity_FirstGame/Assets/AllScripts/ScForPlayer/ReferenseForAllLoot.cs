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
            if (ReferencePrefabs[i].gameObject)
            {
                ValueLoots.Add(i,ReferencePrefabs[i]);
                //Debug.Log(ValueLoots[i]);
            }
        }
        

    }

    void Update()
    {
        
    }
    

}
