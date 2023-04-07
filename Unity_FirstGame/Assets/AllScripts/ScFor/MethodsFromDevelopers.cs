using UnityEngine;

public class MethodsFromDevelopers : MonoBehaviour
{       
    void Start()
    {
        
    }

    

    protected Transform PutObjects(Transform ObjectForPut, Transform PosForPut)
    {
        ObjectForPut.transform.SetParent(PosForPut);

        ObjectForPut.position = PosForPut.transform.position;
        ObjectForPut.rotation = PosForPut.transform.rotation;

        return ObjectForPut;
    }


}
