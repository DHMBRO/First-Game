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



    
}
