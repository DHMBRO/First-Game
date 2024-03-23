using UnityEngine;

public class RagdollEnDis : MonoBehaviour
{
    public void GetReferences(RagdollControler ControlerRagdoll)
    {
        ControlerRagdoll.SetRagdollDelegat += SetColision;
        ControlerRagdoll.SetRagdollDelegat += SetRigidboby;
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


}
