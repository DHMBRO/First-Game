using UnityEngine;

public class DropControler : MethodsFromDevelopers
{
    [SerializeField] Transform PointForDrop;
    [SerializeField] GameObject DropObject;
    
    [SerializeField] SlotControler PlSlotControler;

    [SerializeField] private float DeleyForDestory;
    [SerializeField] private float Deley = 2.5f;

    void Start()
    {
        PlSlotControler = gameObject.GetComponent<SlotControler>();

      
        
    }

    void Update()
    {
        if (PlSlotControler.ObjectInHand) DropObject = PlSlotControler.ObjectInHand.gameObject;

        if (PlSlotControler.ObjectInHand && PlSlotControler.ObjectInHand.gameObject.tag != "Knife" && Input.GetKeyDown(KeyCode.Q))
        {
            DropObjects(PlSlotControler.ObjectInHand.transform, DropObject.transform);
        
            if (PlSlotControler.MyWeapon01) ChangeReferences(DropObject, PlSlotControler.MyWeapon01.gameObject);
            if (PlSlotControler.MyWeapon02) ChangeReferences(DropObject, PlSlotControler.MyWeapon02.gameObject);
            if (PlSlotControler.MyPistol01) ChangeReferences(DropObject, PlSlotControler.MyPistol01.gameObject);

            if (PlSlotControler.MyShope01) ChangeReferences(DropObject, PlSlotControler.MyShope01.gameObject);
            if (PlSlotControler.MyShope02) ChangeReferences(DropObject, PlSlotControler.MyShope02.gameObject);
            if (PlSlotControler.MyShope03) ChangeReferences(DropObject, PlSlotControler.MyShope03.gameObject);

            if (PlSlotControler.MyHelmet) ChangeReferences(DropObject, PlSlotControler.MyHelmet.gameObject);
            if (PlSlotControler.MyArmor) ChangeReferences(DropObject, PlSlotControler.MyArmor.gameObject);
            if (PlSlotControler.MyBackPack) ChangeReferences(DropObject, PlSlotControler.MyBackPack.gameObject);


        }

        void ChangeReferences(GameObject ObjectToChange, GameObject ObjectToComparison)
        {
            if (ObjectToChange == ObjectToComparison)
            {
                ObjectToComparison = null;
                Debug.Log("1");
            }
            


        }
    }

    

}
