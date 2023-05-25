using UnityEngine;

public class DropControler : MethodsFromDevelopers
{
    [SerializeField] Transform PointForDrop;
    [SerializeField] GameObject ObjectToDrop;
    
    [SerializeField] SlotControler ControlerForSlots;

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

                if (ControlerForSlots.MyWeapon01 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyWeapon01.gameObject)
                    ControlerForSlots.MyWeapon01 = null;

                if (ControlerForSlots.MyWeapon02 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyWeapon02.gameObject)
                    ControlerForSlots.MyWeapon02 = null;

                if (ControlerForSlots.MyPistol01 &&  ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyPistol01.gameObject)
                    ControlerForSlots.MyPistol01 = null;

            }
        }
    }

   

}
