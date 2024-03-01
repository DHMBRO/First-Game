using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminationController : MonoBehaviour
{
    
    public List<LightControl> LightSources = new List<LightControl>();
    [SerializeField] public GameObject HeadObject;
    GameObject Sun;
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
        Debug.DrawLine(HeadObject.transform.position, HeadObject.transform.position + (-Sun.transform.forward * 50.0f));
        RaycastHit[] Hitresults = Physics.RaycastAll(HeadObject.transform.position, -Sun.transform.forward);
        foreach (RaycastHit Hitres in Hitresults)
        {
            if (Hitres.collider.gameObject.transform.root.gameObject == gameObject.transform.root)
            {
                continue;
            }
            return 0.0f;
        }
       
        return Mathf.Clamp(((Sun.transform.eulerAngles.x + 20.0f) / 60), 0.0f, 1.0f);
    }
}
