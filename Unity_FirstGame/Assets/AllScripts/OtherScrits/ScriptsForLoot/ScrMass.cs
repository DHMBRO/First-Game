using UnityEngine;

public class ScrMass: MonoBehaviour
{
    [SerializeField] public float Mass;
    
    void Start()
    {
        if (gameObject.CompareTag("Ammo9MM"))
        {
            Mass = 0.35f;
        }
        else if (gameObject.CompareTag("Ammo45_APC"))
        {
            Mass = 0.4f;
        }
        else if (gameObject.CompareTag("Ammo5_56MM"))
        {
            Mass = 1.0f;
        }    
        else if (gameObject.CompareTag("Ammo7_62MM"))
        {
            Mass = 1.5f;
        }        
    }
}
