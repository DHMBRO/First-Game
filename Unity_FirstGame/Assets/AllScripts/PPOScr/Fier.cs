using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fier : MonoBehaviour
{
    [SerializeField] ForceMode forceMode;
    [SerializeField] public float Speed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(new Vector3(0, 0, 1) * Speed, forceMode);
    }
}
