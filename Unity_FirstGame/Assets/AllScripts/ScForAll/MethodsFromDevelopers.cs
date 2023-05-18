using UnityEngine;

public class MethodsFromDevelopers : MonoBehaviour
{
        
    private void Update()
    {
                
    }    

    protected void PutObjects(Transform ObjectForPut, Transform PosForPut)
    {
        Rigidbody RigObject = ObjectForPut.gameObject.GetComponent<Rigidbody>();
        if (RigObject)
        {
            Puting(RigObject);
        }
        else
        {
            RigObject = ObjectForPut.gameObject.AddComponent<Rigidbody>();
            Puting(RigObject);
        }
        
        void Puting(Rigidbody RigObject)
        {
            RigObject.enabled = false;
            ObjectForPut.transform.SetParent(PosForPut);

            ObjectForPut.position = PosForPut.transform.position;
            ObjectForPut.rotation = PosForPut.transform.rotation;
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
            RigObject.enabled = true;

            ObjectToDrop.position = ObecjtForCopy.position;
            ObjectToDrop.rotation = ObecjtForCopy.rotation;

            ObjectToDrop.transform.SetParent(null);
        }

    }

    protected void CopyTransform(Transform ObjectToCopy, Transform ObjectForCopy)
    {
        ObjectToCopy.position = ObjectForCopy.position;
        ObjectToCopy.rotation = ObjectForCopy.rotation;
        
    }

    

}
