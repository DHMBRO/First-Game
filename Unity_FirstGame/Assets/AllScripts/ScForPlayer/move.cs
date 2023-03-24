using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    [SerializeField] float Force;
    [SerializeField] ForceMode ForceMode;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    } 

    // Update is called once per frame
    void Update()
    {
        float vert = Input.GetAxis("Vertical");
        float hore = Input.GetAxis("Horizontal");
        Vector3 Forse = new Vector3(vert * Force, 0, hore * Force);
        rb.AddRelativeForce(Forse, ForceMode);

    }
}
