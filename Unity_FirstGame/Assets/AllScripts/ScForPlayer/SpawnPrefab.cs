using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class SpawnPrefab : MonoBehaviour
{
    [SerializeField] GameObject cube;
    GameObject Cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Cube = GameObject.Instantiate(cube,
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(Cube);
    }
}
