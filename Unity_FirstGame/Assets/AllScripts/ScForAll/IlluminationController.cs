using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminationController : MonoBehaviour
{
    float GlobalLight = 0.5f;
    public List<LightControl> LightSources = new List<LightControl>();
    [SerializeField] public GameObject HeadObject;
    void Start()
    {
        
    }
    void Update()
    {
       Debug.Log("Illumination" + GetIlluminatiLvl());
    }
    public float GetIlluminatiLvl()
    {
        float IlluminatonLvl = 0.0f;
        foreach (LightControl lightControl in LightSources)
        {  
            IlluminatonLvl += lightControl.HowMuchObjShine(gameObject);
        }
        return Mathf.Clamp(IlluminatonLvl + GetGlobalIlluminatiLvl(), 0.0f, 1.0f);
    }
    float GetGlobalIlluminatiLvl()
    {
        return GlobalLight;
    }
}
