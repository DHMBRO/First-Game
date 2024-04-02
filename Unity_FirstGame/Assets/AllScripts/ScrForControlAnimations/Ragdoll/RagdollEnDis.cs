using UnityEngine;

public class RagdollEnDis : MonoBehaviour
{
    public void GetReferences(RagdollControler ControlerRagdoll)
    {
        ControlerRagdoll.SetRagdollDelegat += SetColision;
        ControlerRagdoll.SetRagdollDelegat += SetRigidboby;

        ControlerRagdoll.SetRagdollDelegat = delegate (bool Enable) { Debug.Log(Enable); };
        ControlerRagdoll.SetRagdollDelegat = (bool Enable) => { Debug.Log(Enable); }; 
                
    }

    private void SetRigidboby(bool EnableRagdoll)
    {
        Rigidbody LocalRigidbody = GetComponent<Rigidbody>();
        
        LocalRigidbody.isKinematic = !EnableRagdoll;
    }

    private void SetColision(bool EnableRagdoll)
    {
        Collider LocalCollider = GetComponent<Collider>();
        
        LocalCollider.enabled = EnableRagdoll;
        LocalCollider.isTrigger = !EnableRagdoll;
    }


}
