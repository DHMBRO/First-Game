using UnityEngine;

public class MethodsFromDevelopers : MonoBehaviour
{
    [SerializeField] SlotControler PlayerSlotControler;


    

    protected void PutObjects(Transform ObjectForPut, Transform PosForPut)
    {
        Puting();
        ChangeHierarchy(ObjectForPut, PosForPut);
        CopyTransform(ObjectForPut, PosForPut);

        void Puting()
        {
            Rigidbody ObgRig = ObjectForPut.gameObject.GetComponent<Rigidbody>();
            if (ObgRig) Destroy(ObgRig);
        }

    }

    protected void DropObjects(Transform ObjectToDrop, Transform ObecjtForCopy)
    {
        Droping();
        ChangeHierarchy(ObjectToDrop, null);
        CopyTransform(ObjectToDrop, ObjectToDrop);

        void Droping()
        {
            Rigidbody ObgRig = ObjectToDrop.gameObject.GetComponent<Rigidbody>();
            if (!ObgRig)
            {
                ObgRig = ObjectToDrop.gameObject.AddComponent<Rigidbody>();
            }

        }

    }

    protected void ChangeHierarchy(Transform ObjectToCopy, Transform ObjectForCopy)
    {
        ObjectToCopy.transform.SetParent(ObjectForCopy);

    }

    protected void CopyTransform(Transform ObjectToCopy, Transform ObjectForCopy)
    {
        ObjectToCopy.position = ObjectForCopy.position;
        ObjectToCopy.rotation = ObjectForCopy.rotation;
        
    }

    

}
