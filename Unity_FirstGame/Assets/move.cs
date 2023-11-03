using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rigidbody.AddForce(new Vector3 (horizontal, 0, vertical), ForceMode.Force);
    }
}
