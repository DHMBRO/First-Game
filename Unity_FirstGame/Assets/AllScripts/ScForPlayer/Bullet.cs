using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject audioToMetalic;
    GameObject audio;
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        audio = GameObject.Instantiate(audioToMetalic);
        Destroy(gameObject);
    }

    void Update()
    {
        Destroy(gameObject, 10.0f);
    }
}
