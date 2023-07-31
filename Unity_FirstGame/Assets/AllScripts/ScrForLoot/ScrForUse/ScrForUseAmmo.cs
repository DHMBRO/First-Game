using UnityEngine;
using System.Collections.Generic;

public class ScrForUseAmmo : MonoBehaviour, IUsebleInterFace
{
    [SerializeField] public int CurrentAmmo;
    [SerializeField] private int MaxAmmo;
    //[SerializeField] private string KeyToShops;

    public TypeCaliber CaliberToBox;

    public void Use(GameObject Target, SelectAnObject SelectObj)
    {
       
    }

}
