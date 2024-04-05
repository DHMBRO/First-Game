using UnityEngine;

public class AddTorqueExample : MonoBehaviour
{
    [SerializeField] Rigidbody ObjectRIG;
    [SerializeField] float RotateSpeed = 1.0f;
    [SerializeField] int Count = 0;

    void Start()
    {
        ObjectRIG = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
     
    }


    private void Update()
    {
        if(Count == 0)
        B();        
    }

    private void B()
    {
        ObjectRIG.AddTorque(transform.right * RotateSpeed);
        Count++;
    }

}
