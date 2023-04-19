using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4MarkScript : MonoBehaviour
{
    [SerializeField] Material ChangeMaterial;
    MeshRenderer MeshRenderer;
    void Start()
    {
        MeshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BulletM4"))
        {
            MeshRenderer.material = ChangeMaterial;
            Debug.Log("work");

        }
    }

    void Update()
    {
        
    }
}
