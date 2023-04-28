using UnityEngine;

public class MethodsFromDevelopers : MonoBehaviour
{
        
    private void Update()
    {
                
    }    

    protected void PutObjects(Transform ObjectForPut, Transform PosForPut)
    {
        
        ObjectForPut.transform.SetParent(PosForPut);

        ObjectForPut.position = PosForPut.transform.position;
        ObjectForPut.rotation = PosForPut.transform.rotation;
      
    }

    protected void DropObjects(Transform ObjectToDrop, Transform ObecjtForCopy)
    {
        ObjectToDrop.position = ObecjtForCopy.position;
        ObjectToDrop.rotation = ObecjtForCopy.rotation;
        
        ObjectToDrop.transform.SetParent(null);        
    }

    protected void CopyTransform(Transform ObjectToCopy, Transform ObjectForCopy)
    {
        ObjectToCopy.position = ObjectForCopy.position;
        ObjectToCopy.rotation = ObjectForCopy.rotation;
        
    }

    

}
