using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminationController : MonoBehaviour
{
    
    public List<LightControl> LightSources = new List<LightControl>();
    [SerializeField] public GameObject HeadObject;
    GameObject Sun;
    [SerializeField] public float BaseIlluminationLvl = 0.2f;
    void Start()
    {
        GameObject [] SunObj = GameObject.FindGameObjectsWithTag("Directional Light");
        foreach (GameObject obj in SunObj)
        {
            if (obj.GetComponent<Light>())
            {
                Sun = obj;
                break;
            }
        }
        //GlobalLight = 
    }
    void Update()
    {
        if (gameObject.CompareTag("Player01"))
        {
            Debug.Log("Illumination " + GetIlluminatiLvl() + gameObject.name);
        }
       
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
        if(!Sun)
        {
            //Debug.Log("Not set Sun");
            return 0.0f;
        }

        Debug.DrawLine(HeadObject.transform.position, HeadObject.transform.position + (-Sun.transform.forward * 50.0f));
        RaycastHit[] Hitresults = Physics.RaycastAll(HeadObject.transform.position, -Sun.transform.forward);
        foreach (RaycastHit Hitres in Hitresults)
        {
            if (Hitres.collider.gameObject.transform.root.gameObject == gameObject.transform.root || 
                !Hitres.collider.gameObject.transform.root.gameObject.isStatic)
            {
                continue;
            }
            return 0.0f;
        }
       
        return BaseIlluminationLvl;
    }
}
