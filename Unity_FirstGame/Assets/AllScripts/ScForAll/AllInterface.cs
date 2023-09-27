using UnityEngine;

public interface IUsebleInterFace
{
    //public void Use(GameObject Target, SelectAnObject SelectObj);
    public void Use(GameObject Target, InfoForLoot InfoLoot, SelectAnObject SelectObj);

}

public interface IReferencesInterface
{
    public void GetReferences(GameObject ObjectToCopy, GameObject ObjectForCopy);
}

public interface IUpdateInfo
{
    public void UpdateInfo();

}

public interface IControlerToInfo
{
    public void GetInfo(InfoForLoot ObjectForGetInfo, GameObject ObjetToGiveInfo);
}

public interface IEquipment
{
    public void ChangePosition(bool Use);
}