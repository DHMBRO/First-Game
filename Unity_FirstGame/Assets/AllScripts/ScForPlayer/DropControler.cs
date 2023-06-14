using UnityEngine;

public class DropControler : MethodsFromDevelopers
{
    [SerializeField] public Transform PointForDrop;
    [SerializeField] GameObject ObjectToDrop;
    
    [SerializeField] GameObject weapon01;
    [SerializeField] GameObject weapon02;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject knife;
    [SerializeField] Sprite none;
    
    [SerializeField] SlotControler ControlerForSlots;

    void Start()
    {
        ControlerForSlots = gameObject.GetComponent<SlotControler>();

      
        
    }

    void Update()
    {
        
        if (PointForDrop && ControlerForSlots.ObjectInHand && Input.GetKeyDown(KeyCode.Q))
        {
            if (!ControlerForSlots.ObjectInHand.CompareTag("Knife"))
            {
                DropObjects(ControlerForSlots.ObjectInHand.transform, PointForDrop.transform);

                if (ControlerForSlots.MyWeapon01 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyWeapon01.gameObject)
                {
                    ControlerForSlots.MyWeapon01 = null;
                    //weapon01.GetComponent<IImage>().GetImage(none);
                }

                if (ControlerForSlots.MyWeapon02 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyWeapon02.gameObject)
                {
                    ControlerForSlots.MyWeapon02 = null;
                    //weapon02.GetComponent<IImage>().GetImage(none);
                }

                if (ControlerForSlots.MyPistol01 && ControlerForSlots.ObjectInHand.gameObject == ControlerForSlots.MyPistol01.gameObject)
                {
                    ControlerForSlots.MyPistol01 = null;
                    //pistol.GetComponent<IImage>().GetImage(none);
                }

            }
        }
    }

    void Drop(Transform PointForDrop, GameObject ObjectToDrop)
    {
        
    }
































}
