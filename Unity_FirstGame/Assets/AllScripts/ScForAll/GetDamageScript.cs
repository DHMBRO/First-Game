using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GetDamageScript : MonoBehaviour
{
    [SerializeField] HpScript OwnerHpScript;
    [SerializeField] PartBody BodyPart;

    [SerializeField] List<TypeCaliber> AllCalibers = new List<TypeCaliber>();
    [SerializeField] List<float> ListOfMultiplerDamage = new List<float>();
    Dictionary<TypeCaliber, float> MultiplerOfDamage = new Dictionary<TypeCaliber, float>();


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

        if (AllCalibers.Count == ListOfMultiplerDamage.Count)
        {
            for (int i = 0; i < AllCalibers.Count; i++)
            {
                MultiplerOfDamage.Add(AllCalibers[i], ListOfMultiplerDamage[i]);
            }
        }
        else 
        {
            Debug.Log("AllCalibers.Count != ListOfMultiplerDamage.Count");
        }

    }

    public void UpdateEquipment(GameObject Equipment)
    {
        CurrentEqipment = Equipment.GetComponent<IDamageAbsrption>();
    }

    public void UpdateEquipment()
    {
        CurrentEqipment = null;
    }


    public void GetDamage(float Damage, TypeCaliber CaliberOfBullet)
    {
        if (OwnerHpScript)
        {
            if (BodyPart == PartBody.Head)
            {
                Damage = Damage * MultiplerOfDamage[CaliberOfBullet];
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
