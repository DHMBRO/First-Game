using UnityEngine;

public class ScrForAllLoot : MonoBehaviour
{
    //Info This Object
    [SerializeField] public float Mass;
    [SerializeField] public bool CanCombining;
    [SerializeField] public bool HaveDescription;
    [SerializeField] public bool ShowTheAmmo;
    [SerializeField] public Sprite SpriteForLoot;

    //Description Object
    [SerializeField] public string NameOfThisObject;
    [SerializeField] public string Descrition;
    [SerializeField] public string[] ParametersLoot = new string[6];

}

   