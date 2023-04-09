using UnityEngine;

public class MethodsFromDevelopers : MonoBehaviour
{
    float SpeedForMove = 1.0f;
    


    
    private void Update()
    {
                
    }    

    protected Transform PutObjects(Transform ObjectForPut, Transform PosForPut)
    {
        int G = 0;
        ObjectForPut.transform.SetParent(PosForPut);

        ObjectForPut.position = PosForPut.transform.position;
        ObjectForPut.rotation = PosForPut.transform.rotation;

        return ObjectForPut;
    }



    
}
