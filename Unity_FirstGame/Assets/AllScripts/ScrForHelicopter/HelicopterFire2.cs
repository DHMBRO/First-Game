using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HelicopterFire2 : MonoBehaviour
{
    [SerializeField] float shootSpeed;
    [SerializeField] ForceMode Mode;
    [SerializeField] Vector3 Force = new Vector3(0, 100, 0);
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /*
        Vector3 additionalRotation = new Vector3(45, 0, 0);
        gameObject.transform.rotation *= Quaternion.Euler(additionalRotation);
        */
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(Force * shootSpeed, Mode);
    }
    void OnCollisionEnter()
    {
            Destroy(gameObject);
    }
}
