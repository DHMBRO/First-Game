using UnityEngine;

public interface IUsebleInterFace
{
    public void Use();
}

public interface IReferencesInterface
{
    public void GetReferences(GameObject ObjectToCopy, GameObject ObjectForCopy);
    

}

public interface IImage
{
    public void GetImage(Sprite SpriteForLoot);
}

public interface IDrop
{
    public void Drop();
}