using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInterface : MonoBehaviour
{
    string DropingSlot = null;
    [SerializeField] DropControler dropControler;

    public void Drop()
    {
        dropControler.GetComponent<IDrop>().Drop(DropingSlot);
    }

    public void DropWeapon01()
    {
        DropingSlot = "weapon01";
    }

    public void DropWeapon02()
    {
        DropingSlot = "weapon02";
    }
    public void Pistol01()
    {
        DropingSlot = "pistol";
    }
}
