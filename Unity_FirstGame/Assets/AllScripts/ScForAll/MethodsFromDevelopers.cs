using UnityEngine;

public class MethodsFromDevelopers : MonoBehaviour
{
    
    protected void PutObjects(Transform ObjectForPut, Transform PosForPut, bool AddNoice)
    {
        Rigidbody RigObject = ObjectForPut.GetComponent<Rigidbody>();
        Collider BCObject = ObjectForPut.GetComponent<Collider>();

        SoundCreatorScript SCSObject;
        ExecutoreScriptToRock ESObject;

        if (AddNoice)
        {
            SCSObject = ObjectForPut.GetComponent<SoundCreatorScript>();
            ESObject = ObjectForPut.GetComponent<ExecutoreScriptToRock>();

            if (SCSObject) Destroy(SCSObject);
            if (ESObject) Destroy(ESObject);
        }

        if (RigObject) Destroy(RigObject);
        if (BCObject) BCObject.enabled = false;
        else
        {
            BCObject = ObjectForPut.gameObject.AddComponent<Collider>();
            //BCObject.enabled = false;
        }

        Put();
        
        void Put()
        {
            Vector3 Scale = ObjectForPut.localScale;
            ObjectForPut.transform.SetParent(PosForPut);
            
            //ObjectForPut.localScale = Scale;
            
            CopyTransform(ObjectForPut, PosForPut);   
            
            //Debug.Log("Saved Scale" + Scale);
            //Debug.Log("Current localScale" + ObjectForPut.localScale);    
        }

    }

    protected void DropObjects(Transform ObjectToDrop, Transform ObecjtForCopy, bool AddNoice)
    {
        if (!ObjectToDrop || !ObecjtForCopy)
        {
            return;
        }
        Rigidbody RigObject = ObjectToDrop.GetComponent<Rigidbody>();
        Collider BCObject = ObjectToDrop.GetComponent<Collider>();

        SoundCreatorScript SCSObject;
        ExecutoreScriptToRock ESObject;

        if (AddNoice)
        {
            SCSObject = ObjectToDrop.GetComponent<SoundCreatorScript>();
            ESObject = ObjectToDrop.GetComponent<ExecutoreScriptToRock>();

            if (!SCSObject) SCSObject = ObjectToDrop.gameObject.AddComponent<SoundCreatorScript>();
            if (!ESObject) ESObject = ObjectToDrop.gameObject.AddComponent<ExecutoreScriptToRock>();
        }

        if (RigObject)
        {
            Droping(RigObject, BCObject);
        }
        else
        {
            RigObject = ObjectToDrop.gameObject.AddComponent<Rigidbody>();
            if (!BCObject) BCObject = ObjectToDrop.gameObject.AddComponent<Collider>();

            Droping(RigObject, BCObject);
        }

        void Droping(Rigidbody RigObject, Collider BCObject)
        {
            RigObject.isKinematic = false;
            RigObject.useGravity = true;

            BCObject.enabled = true;

            CopyTransform(ObjectToDrop.transform, ObecjtForCopy.transform);

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
