using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfScript : MonoBehaviour
{
    protected HpScript MyHpScript;
    protected LocateScript MyLocateScript;
    
    void Start()
    {
        MyHpScript = this.gameObject.GetComponent<HpScript>();
        MyLocateScript = this.gameObject.GetComponent<LocateScript>();
    }

    void Update()
    {

    }
    public bool DoIHaveATarget()
    {
        if (MyLocateScript.Target)
        {
            return true;
        }
        return false;
    }
    public float HowMuchHpIHave()
    { 
        return MyHpScript.HealthPoint;
    }
    public bool CanISeeEnemy()
    {
        return MyLocateScript.CanISeeTarget();
    }
}
