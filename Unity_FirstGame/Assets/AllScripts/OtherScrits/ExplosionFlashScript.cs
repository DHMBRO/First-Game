using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] public GameObject SpotlightPrefab;
    [SerializeField] public float LightTime;
    
    public void CreateFlash(Transform Position)
    {
       GameObject Spotlight = Instantiate(SpotlightPrefab, Position.transform.position, Position.transform.rotation);
       if (!Spotlight) { return; }
       Destroy(Spotlight, LightTime);
    }
   
}
