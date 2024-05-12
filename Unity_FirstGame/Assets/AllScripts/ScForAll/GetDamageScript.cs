using UnityEngine;

public class GetDamageScript : MonoBehaviour
{
    [SerializeField] HpScript OwnerHpScript;
    [SerializeField] PartBody BodyPart;
    [SerializeField] float DamageMultiplier = 1.0f;

    enum PartBody
    {
        None,
        Head,
        Body,
    }

    void Start()
    {
        OwnerHpScript = GetComponentInParent<HpScript>();
    }
    
    public void GetDamage(float Damage)
    {
        if (OwnerHpScript)
        {
            if (BodyPart == PartBody.Head)
            {
                Damage *= DamageMultiplier;
            }

            OwnerHpScript.InflictingDamage(Damage);
        }
    }

}
