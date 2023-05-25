using UnityEngine;

public class DropControler : MethodsFromDevelopers
{
    [SerializeField] Transform PointForDrop;
    [SerializeField] GameObject ObjectToDrop;
    
    [SerializeField] SlotControler ControlerForSlots;

    [SerializeField] private float DeleyForDestory;
    [SerializeField] private float Deley = 2.5f;

    void Start()
    {
        ControlerForSlots = gameObject.GetComponent<SlotControler>();

      
        
    }

    void Update()
    {
        if (ControlerForSlots.ObjectInHand && Input.GetKeyDown(KeyCode.Q))
        {
            if (!ControlerForSlots.ObjectInHand.CompareTag("Knife"))
            {
                DropObjects(ControlerForSlots.ObjectInHand.transform, ObjectToDrop.transform);
            }
        }
    }

    

}
