using UnityEngine;

public class MethodsFromDevelopers : MonoBehaviour
{
    
    protected void PutObjects(Transform ObjectForPut, Transform PosForPut)
    {
        Rigidbody RigObject = ObjectForPut.gameObject.GetComponent<Rigidbody>();
        BoxCollider BCObject = ObjectForPut.gameObject.GetComponent<BoxCollider>();

        if (RigObject) Destroy(RigObject);
        if (BCObject) BCObject.enabled = false;
        else
        {
            BCObject = ObjectForPut.gameObject.AddComponent<BoxCollider>();
            BCObject.enabled = false;
        }

        Puting();
        
        void Puting()
        {
            ObjectForPut.transform.SetParent(PosForPut);
            CopyTransform(ObjectForPut, PosForPut);   
        }

    }

    protected void DropObjects(Transform ObjectToDrop, Transform ObecjtForCopy)
    {
        Rigidbody RigObject = ObjectToDrop.gameObject.GetComponent<Rigidbody>();
        BoxCollider BCObject = ObjectToDrop.gameObject.GetComponent<BoxCollider>();

        if (RigObject)
        {
            Droping(RigObject, BCObject);
        }
        else 
        {
            RigObject = ObjectToDrop.gameObject.AddComponent<Rigidbody>();
            if (!BCObject) BCObject = ObjectToDrop.gameObject.AddComponent<BoxCollider>();

            Droping(RigObject, BCObject);
        }
        
        void Droping(Rigidbody RigObject, BoxCollider BCObject)
        {
            RigObject.isKinematic = false;
            RigObject.useGravity = true;

            BCObject.enabled = true;

            CopyTransform(ObjectToDrop, ObecjtForCopy);

            ObjectToDrop.transform.SetParent(null);

           RigObject.AddRelativeForce(new Vector3(0.0f - 1.0f * 1.5f, 0.0f + 1.0f * 3.5f, 0.0f), ForceMode.Impulse);
           RigObject.AddRelativeTorque(new Vector3(0.0f + 1.0f, 0.0f - 1.0f * 32.0f, 0.0f));
        }

    }

    protected void CopyTransform(Transform ObjectToCopy, Transform ObjectForCopy)
    {
        //ObjectToCopy.SetParent(null);
        ObjectToCopy.position = ObjectForCopy.position;
        ObjectToCopy.rotation = ObjectForCopy.rotation;
        
    }

    

}
