using UnityEngine;

public class GunControler : MonoBehaviour
{
    [SerializeField] private Transform SlotGun;
    [SerializeField] private Transform SlotHand;
    [SerializeField] private Transform MyGun;

    [SerializeField] private int counter;
    [SerializeField] public bool CanFire = true;
        
    void Start()
    {
        CanFire = false;
        Appropriation01();
    }
    
    void Update()
    {
        if (counter == 1 && Input.GetKeyUp("1"))
        {
            counter = 0;
            Debug.Log("1");
        }
        MovingGunForSlots();    
    }
    
    void Appropriation01()
    {
        MyGun.transform.position = SlotGun.transform.position;
        MyGun.transform.rotation = SlotGun.transform.rotation;

    }

    void Appropriation02()
    {
        MyGun.transform.position = SlotHand.transform.position;

    }

    void MovingGunForSlots()
    {
        if (Input.GetKey("1"))
        {
            if (counter < 1 && !CanFire)
            {                
                Appropriation02();
                CanFire = true;
                counter++;
            }
            else if (counter < 1 && CanFire)
            {                
                Appropriation01();
                CanFire = false;
                counter++;                
            }                        
        }
    }
    
    

}
