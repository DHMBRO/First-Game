using UnityEngine;

public class GetDamageScript : MonoBehaviour
{
    [SerializeField] HpScript OwnerHpScript;
    [SerializeField] PartBody BodyPart;
    [SerializeField] float DamageMultiplier = 1.0f;

    IDamageAbsrption CurrentEqipment;

    enum PartBody
    {
        None,
        Head,
        Body,
    }

    void Start()
    {
        OwnerHpScript = GetComponentInParent<HpScript>();
        CurrentEqipment = GetComponentInChildren<IDamageAbsrption>();
    }

    public void UpdateEquipment(GameObject Equipment)
    {
        CurrentEqipment = Equipment.GetComponent<IDamageAbsrption>();
    }

    public void UpdateEquipment()
    {
        CurrentEqipment = null;
    }


    public void GetDamage(float Damage)
    {
        if (OwnerHpScript)
        {
            if (BodyPart == PartBody.Head)
            {
                Damage *= DamageMultiplier;
            }

            if(CurrentEqipment != null) Damage = CurrentEqipment.ReturnNewDamage(Damage);
            
            Debug.Log(Damage);

            OwnerHpScript.InflictingDamage(Damage);
        }
    }

}
