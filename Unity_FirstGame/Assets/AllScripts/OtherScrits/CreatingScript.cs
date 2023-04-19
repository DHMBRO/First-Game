using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingScript : MonoBehaviour
{

    [SerializeField] Material StandartMaterial;
    [SerializeField] Material ChangeMaterial;

    MeshRenderer MeshRenderer;
    void Start()
    {
        MeshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BulletM4"))
        {
            MeshRenderer.material = ChangeMaterial;
            Debug.Log("work");
        }

    }
    void Update()
    {

    }
}

