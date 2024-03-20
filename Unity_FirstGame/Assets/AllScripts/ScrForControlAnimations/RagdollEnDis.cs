using UnityEngine;

public class RagdollEnDis : MonoBehaviour
{
    [SerializeField] public RagdollControler MyRagdollControler;

    
    public void GetReferences()
    {
        if (MyRagdollControler)
        {
            MyRagdollControler.SetRagdollDelegat += SayResultOfWork;


            MyRagdollControler.SetRagdollDelegat += SetColision;
            MyRagdollControler.SetRagdollDelegat += SetJoints;
            MyRagdollControler.SetRagdollDelegat += SetRigidboby;


        }
        else Debug.Log(gameObject.name + " not set MyRagdollControler !!!");

    }

    public void SayResultOfWork(bool EnableRagdoll)
    {
        if (EnableRagdoll)
        {
            Debug.Log(MyRagdollControler.name + " enabled ragdoll");
        }
        else Debug.Log(MyRagdollControler.name + " disabled ragdoll");
    }

    public void SetRigidboby(bool EnableRagdoll)
    {
        Rigidbody LocalRigidbody = GetComponent<Rigidbody>();
        
        LocalRigidbody.isKinematic = !EnableRagdoll;
            
        //Destroy(LocalRigidbody);
    }

    public void SetColision(bool EnableRagdoll)
    {
        Collider LocalCollider = GetComponent<Collider>();
        
        LocalCollider.enabled = EnableRagdoll;
        LocalCollider.isTrigger = !EnableRagdoll;

        //Destroy(LocalCollider);
    }

    public void SetJoints(bool EnableRagdoll)
    {
        Joint LocalJoint = GetComponent<Joint>();

        //Destroy(LocalJoint);
        
    }

}
