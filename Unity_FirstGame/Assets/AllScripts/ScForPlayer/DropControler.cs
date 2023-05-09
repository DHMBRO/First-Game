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

        if (ControlerForSlots && ControlerForSlots.ObjectInHand)
        {
            DropObject = ControlerForSlots.ObjectInHand.gameObject;
        }
        
    }

    void Update()
    {
        if (ControlerForSlots && ControlerForSlots.ObjectInHand)
        {
            DropObject = ControlerForSlots.ObjectInHand.gameObject;
        }
        if (DropObject)
        {
            Rigidbody RigidbodyObject01 = DropObject.GetComponent<Rigidbody>();

            if (Input.GetKeyDown(KeyCode.Q) && Time.time >= DeleyForDestory)
            {
                DeleyForDestory = Time.time + Deley;
                if (!RigidbodyObject01 && Time.time >= DeleyForDestory)
                {
                    Rigidbody RigidbodyObject02 = DropObject.AddComponent<Rigidbody>();

                    RigidbodySettinbgs(RigidbodyObject02);
                    DropObjects(DropObject.transform, PointForDrop);
                    if (ControlerForSlots.ObjectInHand.gameObject.tag == "Glok")
                    {
                        ControlerForSlots.MyPistol01 = null;                        
                    }
                    else if (ControlerForSlots.ObjectInHand.gameObject.tag == "M4")
                    {
                        ControlerForSlots.MyWeapon01 = null;
                    }
                    else if (ControlerForSlots.ObjectInHand.gameObject.tag == "M4")
                    {
                        ControlerForSlots.MyWeapon02 = null;
                    }                    

                }
                else if (RigidbodyObject01 && Time.time >= DeleyForDestory)
                {
                    RigidbodySettinbgs(RigidbodyObject01);
                    DropObjects(DropObject.transform, PointForDrop);                                        
                }
            }
        }

        
                        
    }

    void RigidbodySettinbgs(Rigidbody RigidbodyObject)
    {
        RigidbodyObject.isKinematic= false;
        RigidbodyObject.useGravity = true;

        RigidbodyObject.angularDrag = 0.05f;
        RigidbodyObject.drag = 0.0f;

    }

}
