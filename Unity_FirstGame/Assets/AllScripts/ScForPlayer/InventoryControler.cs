using UnityEngine;

public class InventoryControler : MonoBehaviour
{
    [SerializeField] public int M4Ammo = 0;
    private bool UcanTakeAmmo;
    [SerializeField] private GameObject AmmoM4Object;

    void Start()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ammo1Ver"))
        {
            UcanTakeAmmo = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo1Ver"))
        {
            UcanTakeAmmo = false;

        }
    }
    void Update()
    {
        if(AmmoM4Object != null&& Input.GetKeyDown(KeyCode.Q)&&UcanTakeAmmo)
        {
            TakeM4Ammo();
        }
        else
        {
            Debug.Log("U cant Take this");
        }
    }

    void TakeM4Ammo()
    {

         GameObject CopyGameObject = Instantiate(AmmoM4Object);


         CopyGameObject.transform.position = AmmoM4Object.transform.position;
         CopyGameObject.transform.rotation  = AmmoM4Object.transform.rotation;


         Transform CopyObjectTransform = AmmoM4Object.GetComponent<Transform>();
         GameObject Original = AmmoM4Object.gameObject;


         Destroy(Original);
                             
         M4Ammo += 10;
        Debug.Log("Ammp += 10");
    }
    void CounterMass()
    {
       

        

    }


}
