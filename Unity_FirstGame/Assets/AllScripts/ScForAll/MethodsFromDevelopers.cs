using UnityEngine;

public class MethodsFromDevelopers : MonoBehaviour
{
    
    protected void PutObjects(Transform ObjectForPut, Transform PosForPut)
    {
        Rigidbody RigObject = ObjectForPut.gameObject.GetComponent<Rigidbody>();
        if (RigObject)
        {
            Puting(RigObject);
        }
        else Puting(RigObject);
        
        void Puting(Rigidbody RigObject)
        {
            Destroy(RigObject);

            ObjectForPut.transform.SetParent(PosForPut);
            CopyTransform(ObjectForPut, PosForPut);
        }

    }

    protected void DropObjects(Transform ObjectToDrop, Transform ObecjtForCopy)
    {
        Rigidbody RigObject = ObjectToDrop.gameObject.GetComponent<Rigidbody>();
        
        //Vector3 DropPower = new Vector3(0.0f - 1.0f * 5.0f, 0.0f + 1.0f * 3.0f, 0.0f);
       // Vector3 RoateInDrop = new Vector3(0.0f + 1.0f * 5.0f, 0.0f, 0.0f);

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

           // RigObject.AddRelativeForce(DropPower, ForceMode.Impulse);
           // RigObject.AddRelativeTorque(DropPower, ForceMode.Impulse);
        }

    }

    protected void CopyTransform(Transform ObjectToCopy, Transform ObjectForCopy)
    {
        ObjectToCopy.position = ObjectForCopy.position;
        ObjectToCopy.rotation = ObjectForCopy.rotation;
        
    }

    

}
