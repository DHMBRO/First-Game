using UnityEngine;

public class ScrForAllLoot : MonoBehaviour
{
    //Info This Object
    [SerializeField] public float Mass;
    [SerializeField] public bool CanCombining;
    [SerializeField] public bool HaveDescription;
    [SerializeField] public bool ShowTheAmmo;
    [SerializeField] public Sprite SpriteForLoot;
    [SerializeField] public bool HasOwner = false;
    [SerializeField] public bool ShowNameOfThisObject = true;

    //Description Object
    [SerializeField] public string NameOfThisObject;
    [SerializeField] public string Descrition;
    [SerializeField] public string[] ParametersLoot = new string[6];

    //Other
    Transform Parent;

    private void Update()
    {
        Parent = transform.parent;

        if (!Parent)
        {
            HasOwner = false;
            return;
        }

        if (Parent.gameObject.tag == "SlotOwner" || Parent.gameObject.tag == "SlotForUse")
        {
            HasOwner = true;
        }
        else HasOwner = false;
        
    }

}

   