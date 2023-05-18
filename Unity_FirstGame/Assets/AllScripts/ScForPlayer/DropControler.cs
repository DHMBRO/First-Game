using UnityEngine;

public class DropControler : MethodsFromDevelopers
{
    [SerializeField] Transform PointForDrop;
    [SerializeField] GameObject DropObject;
    
    [SerializeField] SlotControler ControlerForSlots;

    [SerializeField] private float DeleyForDestory;
    [SerializeField] private float Deley = 2.5f;

    void Start()
    {
        ControlerForSlots = gameObject.GetComponent<SlotControler>();

      
        
    }

    void Update()
    {
        if (ControlerForSlots.ObjectInHand && ControlerForSlots.ObjectInHand.gameObject.tag != "Knife" && Input.GetKeyDown(KeyCode.Q))
        {
            DropObjects(ControlerForSlots.ObjectInHand.transform, DropObject.transform);
        }
        
        
    }

    

}
