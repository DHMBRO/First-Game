using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HelicopterFire : MonoBehaviour
{
    [SerializeField]GameObject Ammo;
    float timerShootRate = 0.00f;
    float timerShooting = 0.00f;

    [SerializeField] float ShootDelay = 0.16f;
    [SerializeField] HelicopterScr Hscr;
    [SerializeField] float SootingRandomSceMin;
    [SerializeField] float SootingRandomSecMax;
    float timeOfSet;
    bool fire = true;
    // Start is called before the first frame update
    void Start()
    {
        timeOfSet = Random.Range(SootingRandomSceMin, SootingRandomSecMax);
    }
    // Update is called once per frame
    void Update()
    {
        if (Hscr.State == HelicopterScr.States.RoamAround)
        {
            timerShootRate += Time.deltaTime;
            timerShooting += Time.deltaTime;
            //Debug.Log("HelicopterFire" + " " + timer);
            if (Hscr.TargetToGun)
            {
                if(timerShooting > timeOfSet)
                {
                    fire = !fire;
                    timeOfSet = Random.Range(SootingRandomSceMin, SootingRandomSecMax);
                    timerShooting = 0.00f;
                }
                if (ShootDelay <= timerShootRate && fire)
                {
                    GameObject Amo = Instantiate(Ammo, gameObject.transform.position, gameObject.transform.rotation);
                    Destroy(Amo, 10f);
                    timerShootRate = 0.00f;
                }
            }
        }
    }
}
