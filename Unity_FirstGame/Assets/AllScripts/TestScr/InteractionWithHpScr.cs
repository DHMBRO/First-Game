using UnityEngine;

public class InteractionWithHpScr : MonoBehaviour
{
    [SerializeField] HpScript LocalHpScr;
    [SerializeField] float SpeedOfInteraction = 1.0f;
    
    [SerializeField] bool Heal = false;
    [SerializeField] bool Work = false;

    private void OnTriggerStay(Collider other)
    {
        LocalHpScr = other.GetComponent<HpScript>();

        if (LocalHpScr)
        {
            if (Heal)
            {
                LocalHpScr.HealHp(SpeedOfInteraction);
            }
            else
            {
                LocalHpScr.HealHp(-SpeedOfInteraction);
            }
        }

    }

}
