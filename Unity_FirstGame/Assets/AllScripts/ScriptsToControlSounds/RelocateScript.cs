using UnityEngine;

public class RelocateScript : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] bool CanWork = true;

    [SerializeField] float TimeToRelocate = 0.0f;
    [SerializeField] float RelocateDeley = 1.0f;

    [SerializeField] float MinDistanceToTarget = 50.0f;
    [SerializeField] float RandomDistance = 100.0f;

    Vector3 NewPosition;

    void Start()
    {
        if (!Target)
        {
            Debug.LogError("Not set Target-Transfrom !" + gameObject.name);
        }
        else
        {
            CanWork = true;
        }

        NewPosition = Target.position;
    }

    void Update()
    {
        if (!CanWork) return;

        if (Time.time > TimeToRelocate)
        {
            TimeToRelocate = Time.time + RelocateDeley;

            NewPosition = Target.position + new Vector3(RandomNumber(RandomDistance), 0.0f, RandomNumber(RandomDistance));

            transform.position = NewPosition;
        }        

    }

    
    private float RandomNumber(float Value)
    {
        return Random.Range(-Value, Value);
    }

    private float GetDistanceBetwenTargetAndNewVector(Vector3 NewPosition)
    {
        return (Target.position - transform.position).magnitude;
    }
}
