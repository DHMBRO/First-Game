using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInformationScript : MonoBehaviour
{
    public HeadInterface MyHeadScript;
    public HpScript HealthScript;
    void Start()
    {
        MyHeadScript = gameObject.GetComponent<HeadInterface>();
        HealthScript = gameObject.GetComponent<HpScript>();
    }
    

}
