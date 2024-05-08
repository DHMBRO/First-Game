using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    HpScript hpScript;
    void Start()
    {
        hpScript = gameObject.GetComponentInParent<HpScript>();
    }

    public void KillMe()
    {
        hpScript.CallRagdollControler();
        Debug.Log(hpScript + " --->   hp ");
    }
    public void KillMe2()
    {
        hpScript.InstanceKill();
    }

}
