using UnityEngine;

public interface IUsebleInterFace
{
    public void Use(GameObject Target, ScrSaveAndGiveInfo InfoLoot, UseAndDropTheLoot SelectObj);
    public bool Audit(GameObject Target, ScrSaveAndGiveInfo InfoLoot, UseAndDropTheLoot SelectObj);

}

public interface IInteractionWithObjects
{
    public bool CheckToUse();
    public void Interaction();
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
    public void GetInfo(ScrSaveAndGiveInfo ObjectForGetInfo, GameObject ObjetToGiveInfo);
}

public interface IEquipment
{
    public void ChangePosition(bool Use);
}

public interface IDamageAbsrption
{
    public float ReturnNewDamage(float Damage);
}
