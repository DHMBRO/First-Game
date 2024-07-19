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
    public bool ISeeDeadPeople = false;
    protected LocateScript MyLocateScript;
    void Start()
    {
        CheckingTime = Time.time + Interval;
        MyInfo = gameObject.GetComponent<InfScript>();
        MyLocateScript = gameObject.GetComponent<LocateScript>();
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
        Collider[] Allies = Physics.OverlapSphere(gameObject.transform.position, 25.0f);
        foreach (Collider AllieCloider in Allies)
        {
            if (DeadAllies.Contains(AllieCloider.gameObject))
            {
                continue;
            }

            GameObject AllieGameObject = AllieCloider.gameObject.transform.root.gameObject;
            if (AllieGameObject == gameObject)
            {
                continue;
            }

            HpScript AllieHpScript = AllieCloider.GetComponent<HpScript>();
            if (!AllieHpScript)
            {
                continue;
            }

            if (AllieHpScript.MyLive == HpScript.Live.Alive)
            {
                continue;
            }

            if (!MyLocateScript.CanISee(AllieGameObject, false))
            {
                continue;
            }

            InfScript MyInfo = gameObject.GetComponent<InfScript>();
            if (!MyInfo)
            {
                continue;
            }

            MyInfo.InterestPosition = AllieGameObject.transform.position;
            ISeeDeadPeople = true;
            DeadAllies.Add(AllieGameObject);
        }
    }
    public void NullInterest()
    {
        ISeeDeadPeople = false;
    }
}
