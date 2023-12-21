using UnityEngine;
public class SoundCreatorScript : MonoBehaviour
{
    [SerializeField] public float CurrentNoiceRadiuse;
    [SerializeField] private bool DebugingTheWork = false;

    public void CreateNoise(float LocalNoiceRadius)
    {
        this.CurrentNoiceRadiuse = LocalNoiceRadius;

        Collider[] Colliders;
        Colliders = Physics.OverlapSphere(gameObject.transform.position, LocalNoiceRadius);
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

