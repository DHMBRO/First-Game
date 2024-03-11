using UnityEngine;

public class RagdollEnDis : MonoBehaviour
{
    [SerializeField] public RagdollControler MyRagdollControler;

    
    public void GetReferences()
    {
        if (MyRagdollControler)
        {
            MyRagdollControler.SetRagdollDelegat += SayResultOfWork;


            MyRagdollControler.SetRagdollDelegat += SetRigidboby;
            MyRagdollControler.SetRagdollDelegat += SetColision;
            //MyRagdollControler.SetRagdollDelegat += SetJoints;

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
        
    }

    public void SetColision(bool EnableRagdoll)
    {
        Collider LocalCollider = GetComponent<Collider>();
        
        LocalCollider.enabled = EnableRagdoll;
        LocalCollider.isTrigger = !EnableRagdoll;

    }

    public void SetJoints(bool EnableRagdoll)
    {

    }

}
