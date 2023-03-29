using UnityEngine;

public class ScContM4 : MonoBehaviour
{
    [SerializeField] private M4ControlerForFire M4ControlerForFire;    
    [SerializeField] private bool CanWork = false;
    [SerializeField] private Transform M4Parent;
    


    void Start()
    {        
        
        M4ControlerForFire = gameObject.GetComponent<M4ControlerForFire>();
        M4ControlerForFire.enabled = false;

        M4Parent = gameObject.GetComponentInParent<Transform>();
    }

    
    void Update()
    {
        if (M4ControlerForFire && M4Parent)
        {
            Debug.Log(M4Parent == transform.CompareTag("SlotForUse"));

            if (M4Parent == transform.CompareTag("SlotForUse"))
            {
                M4ControlerForFire.enabled = true;
                Debug.Log("2");
            }
            else
            {
                M4ControlerForFire.enabled = false;

            }
        }
        else
        {
            M4ControlerForFire.enabled = false;
        } 

        
    }
}
