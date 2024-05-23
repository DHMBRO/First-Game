using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesCheckingScript : MonoBehaviour
{
    protected InfScript MyInfo;
    int LayerMask = 6;
    protected List<GameObject> DeadAllies = new List<GameObject>();
    float CheckingTime = 0.0f;
    const float Interval = 1.0f;
    
    void Start()
    {
        CheckingTime = Time.time + Interval;
        MyInfo = gameObject.GetComponent<InfScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckingTime <= Time.time)
        {
            CheckingTime = Time.time + Interval;
            CheckAllies();
        }
    }
    public void CheckAllies()
    {
        Collider[] Allies = Physics.OverlapSphere(gameObject.transform.position, 25.0f, LayerMask);
        foreach (Collider AllieCloider in Allies)
        {
            LocateScript MyLocateScript = gameObject.GetComponent<LocateScript>();
            if (MyLocateScript.CanISee(AllieCloider.gameObject))
            {
                HpScript AllieHpScript = AllieCloider.GetComponent<HpScript>();

                if (AllieHpScript)
                {
                    if (AllieHpScript.MyLive == HpScript.Live.Alive)
                    {
                        continue;
                    }
                    else
                    {
                        InfScript MyInfo = gameObject.GetComponent<InfScript>();
                        if (MyInfo)
                        {
                            MyInfo.InterestPosition = AllieCloider.gameObject.transform.position;
                            DeadAllies.Add(AllieCloider.gameObject);
                        }
                    }
                }
            }
        }
    }
}
