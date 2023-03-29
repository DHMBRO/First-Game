using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject TakeObject;

    [SerializeField] private Transform[] TakeObjects;    
    
    [SerializeField] private int Count = 0;


    [SerializeField] private SlotControler MySlotControler;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("M4"))
        {
            TakeObject = other.gameObject;
            Debug.Log("You can pick up M4");
        }
       

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("M4"))
        {
            TakeObject = null;
            Debug.Log("You cant pick up M4");
        }

    }

    void Start()
    {
        MySlotControler = gameObject.GetComponent<SlotControler>();
    }

    void Update()
    {
        if (MySlotControler && TakeObject && Input.GetKey(KeyCode.E))
        {
            TakeOject();
        }
        else if (Count == 1)
        {
            Count = 0;
        }                
    }

    void TakeOject()
    {
        if (Count == 0)
        {            
            GameObject CopyObject = Instantiate(TakeObject);
            
            CopyObject.transform.position = TakeObject.transform.position;
            CopyObject.transform.rotation = TakeObject.transform.rotation;

            Transform TransforForCopyObject = CopyObject.GetComponent<Transform>();

            GameObject OriginalObject = TakeObject.gameObject;
            Destroy(OriginalObject);

            if (CopyObject.gameObject.CompareTag("M4"))
            {
                MySlotControler.MyGun = TransforForCopyObject;
                MySlotControler.Appropriation01();
            }

            Count++;
        }
        
    } 

}
