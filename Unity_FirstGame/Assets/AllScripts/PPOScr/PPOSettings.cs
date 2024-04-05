using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPOSettings : MonoBehaviour
{
    enum setPPORotationSpeed
    {
        Yes,
        No
    }
    enum setBulletSpeed
    {
        Yes,
        No
    }
    [SerializeField] public float PPORotationSpeed;
    [SerializeField] setPPORotationSpeed SetPPORotationSpeed;
    [SerializeField] public float BulletSpeed;
    [SerializeField] setBulletSpeed SetBulletSpeed;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SetBulletSpeed == setBulletSpeed.No)
        {
            BulletSpeed = 0f;
        }
        if (SetPPORotationSpeed == setPPORotationSpeed.No)
        {
            PPORotationSpeed = 0f;
        }
        Debug.Log("PPORotationSpeed = " + PPORotationSpeed + "; BulletSpeed = " + BulletSpeed);
    }
    
}
