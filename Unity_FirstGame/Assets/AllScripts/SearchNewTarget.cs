using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchNewTarget : MonoBehaviour
{
    [SerializeField] GameObject helicopter;
    [SerializeField] HelicopterScr helicopterScr;
    public List <GameObject> Targets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject ClosestTarget = gameObject;
        float ClosestDistans = float.PositiveInfinity;
        foreach(GameObject Target in Targets)
        {
            if(Vector3.Distance(Target.transform.position, helicopter.transform.position) < ClosestDistans)
            {
                ClosestDistans = Vector3.Distance(Target.transform.position, helicopter.transform.position);
                ClosestTarget = Target;
            }
        }
        helicopterScr.TargetToGun = ClosestTarget;
        if (ClosestTarget.gameObject.GetComponent<HpScript>().HealthPoint <= 0f)
        {
            Targets.Remove(ClosestTarget.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HpScript>() && other.gameObject.GetComponent<HpScript>().HealthPoint > 0f)
        {
            Targets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<HpScript>())
        {
            Targets.Remove(other.gameObject);
        }
    }
}
