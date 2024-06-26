using UnityEngine;
using UnityEngine.UIElements;

public class AtackScript : MonoBehaviour
{
    [SerializeField] Vector3 ScaleAttakcTrigger = new Vector3(1.0f, 1.0f, 1.0f);
    [SerializeField] float LenghtOfAttackTrigger = 1.0f;
    [SerializeField] float TestDamage;
    [SerializeField] bool Bite = false;

    Vector3 PositionTrigger;
    Vector3 Scale;

    float RadiuseHitBoxParent = 0.0f;
    float LastLeghtOfAttackTrigger;

    private void Update()
    {
        if (Bite)
        {
            Punch(TestDamage);
        }
    }

    private void OnDrawGizmos()
    {
        PositionTrigger = transform.position + transform.forward * LenghtOfAttackTrigger;
        Scale = new Vector3(ScaleAttakcTrigger.x, ScaleAttakcTrigger.y, (LenghtOfAttackTrigger * 2.0f) - 1.0f);

        Gizmos.DrawCube(PositionTrigger, Scale);
    }

    public void Punch(float Damage)
    {
        Collider[] Hits;
        GetDamageScript TargetHitBoxScr = null;

        Hits = Physics.OverlapBox(PositionTrigger, Scale);
        //Gizmos.DrawCube(PositionTrigger, Scale);

        foreach (Collider hit in Hits) 
        {
            TargetHitBoxScr = hit.gameObject.GetComponent<GetDamageScript>();

            if (TargetHitBoxScr != null)
            {
                TargetHitBoxScr.GetDamage(Damage, TypeCaliber.Bite);
                break;
            }
        }
        
    }

}
