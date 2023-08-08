using UnityEngine;

public class MethodsFromDevelopers : MonoBehaviour
{
    
    protected void PutObjects(Transform ObjectForPut, Transform PosForPut)
    {
        Rigidbody RigObject = ObjectForPut.gameObject.GetComponent<Rigidbody>();
        
        if (RigObject) Destroy(RigObject);
        
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
        
        if (RigObject)
        {
            Droping(RigObject);
        }
        else 
        {
            RigObject = ObjectToDrop.gameObject.AddComponent<Rigidbody>();
            Droping(RigObject);
        }
        
        void Droping(Rigidbody RigObject)
        {
            RigObject.isKinematic = false;
            RigObject.useGravity = true;

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
