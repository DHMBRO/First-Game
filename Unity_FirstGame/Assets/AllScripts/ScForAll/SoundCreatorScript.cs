using UnityEngine;
public class SoundCreatorScript : MonoBehaviour
{
    [SerializeField] public float CurrentNoiceRadiuse;
    [SerializeField] private bool DebugingTheWork = false;

    [SerializeField] public Transform ZoneNoice;
    [SerializeField] bool ShowTheZoneNoice;

    public void CreateNoise(float CurrentNoiceRadius)
    {
        this.CurrentNoiceRadiuse = CurrentNoiceRadius;

        Collider[] Colliders;
        Colliders = Physics.OverlapSphere(gameObject.transform.position, CurrentNoiceRadius);
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

