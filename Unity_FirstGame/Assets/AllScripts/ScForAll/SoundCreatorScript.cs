using UnityEngine;
public class SoundCreatorScript : MonoBehaviour
{
    [SerializeField] public float CurrentNoiceRadiuse = 0.0f;
    [SerializeField] bool DebugingTheWork = false;
    
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

