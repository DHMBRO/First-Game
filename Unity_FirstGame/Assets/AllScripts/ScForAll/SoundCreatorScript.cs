using UnityEngine;
public class SoundCreatorScript : MonoBehaviour
{
    [SerializeField] public float CurrentNoiceRadiuse;
    [SerializeField] private bool DebugingTheWork = false;

    [SerializeField] public Transform ZoneNoice;
    [SerializeField] bool ShowTheZoneNoice;

    public void CreateNoise(float NewNoiceRadius)
    {
        CurrentNoiceRadiuse = NewNoiceRadius;
        if(NewNoiceRadius <= 0.0f)
        {
            return;
        }
        Collider[] Colliders;
        Colliders = Physics.OverlapSphere(gameObject.transform.position, CurrentNoiceRadiuse);
        foreach (Collider Colider in Colliders)
        {
            if (Colider.gameObject && Colider.gameObject != gameObject)
            {
                SoundTakerScript SoundScript = Colider.gameObject.GetComponentInParent<SoundTakerScript>();
                if (SoundScript)
                {
                    SoundScript.TakeSound(gameObject.transform.position);
                }
            }
        }
        
        if (DebugingTheWork)
        {
            Debug.Log("Sound Created " + NewNoiceRadius + " == Radius  ");

        }

    }
}

