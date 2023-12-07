using UnityEngine;
public class SoundCreatorScript : MonoBehaviour
{
    [SerializeField] public float NoiseRadius = 15.0f;
    [SerializeField] private bool DebugingTheWork = false;

    public void CreateNoise()
    {
        Collider[] Colliders;
        Colliders = Physics.OverlapSphere(gameObject.transform.position, NoiseRadius);
        foreach (Collider Colider in Colliders)
        {
            GameObject ColiderObject = Colider.gameObject;
            SoundTakerScript SoundScript = ColiderObject.GetComponentInParent<SoundTakerScript>();
            if (SoundScript && ColiderObject)
            {
                if (Colider.gameObject == gameObject)
                {
                    continue;
                }
                SoundScript.TakeSound(gameObject.transform.position);
            }
        }
        
        if (DebugingTheWork)
        {
            Debug.Log("Sound Created");

        }

    }
}

