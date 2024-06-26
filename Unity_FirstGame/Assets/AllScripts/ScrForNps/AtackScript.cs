using UnityEngine;

public class AtackScript : MonoBehaviour
{
    [SerializeField] Vector3 ScaleAttakcTrigger;
    [SerializeField] float LenghtOfAttackTrigger = 1.0f;
    [SerializeField] float TestDamage;
    [SerializeField] Transform TestCube;

    Vector3 PositionTrigger;
    Vector3 Scale;

    float RadiuseHitBoxParent = 0.0f;
    
    private void Start()
    {
        RadiuseHitBoxParent = gameObject.GetComponentInParent<CapsuleCollider>().radius;
        
    }

    private void Update()
    {
        Punch(TestDamage);
    }

    private void OnDrawGizmos()
    {
        PositionTrigger = transform.position + transform.forward * LenghtOfAttackTrigger;
        Scale = new Vector3(1.0f, 1.0f, 1.0f);

        Gizmos.DrawCube(PositionTrigger, Scale);
    }

    public void Punch(float Damage)
    {
        Collider[] Hits;
        GetDamageScript TargetHitBoxScr = null;

        transform.localPosition = transform.forward * (RadiuseHitBoxParent * 2.0f);

        

        Hits = Physics.OverlapBox(PositionTrigger, Scale);
        //Gizmos.DrawCube(PositionTrigger, Scale);

        TestCube.position = transform.position;
        TestCube.localScale = Scale;

        foreach (Collider hit in Hits) 
        {
            TargetHitBoxScr = hit.gameObject.GetComponent<GetDamageScript>();

            if (TargetHitBoxScr != null)
            {
                TargetHitBoxScr.GetDamage(Damage, TypeCaliber.Null);
                break;
            }
        }
        
    }

}
