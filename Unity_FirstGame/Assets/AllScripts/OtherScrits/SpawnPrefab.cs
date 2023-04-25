using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    [SerializeField] GameObject cube;
    GameObject Cube;
    
    void Start()
    {
        
    }

    
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
