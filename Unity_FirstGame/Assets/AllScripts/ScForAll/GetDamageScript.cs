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

        OwnerHpScript.HitBoxes += DisableHitBoxes;
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

    public void DisableHitBoxes()
    {
        SphereCollider HeadHitBox;
        CapsuleCollider BodyHitBox;


        if (BodyPart == PartBody.Head)
        {
            HeadHitBox = GetComponent<SphereCollider>();
            HeadHitBox.enabled = false;
        }

        if (BodyPart == PartBody.Body)
        {
            BodyHitBox = GetComponent<CapsuleCollider>();
            BodyHitBox.enabled = false;
        }
    }

}
