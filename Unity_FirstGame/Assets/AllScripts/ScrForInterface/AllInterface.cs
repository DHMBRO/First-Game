using UnityEngine;
using UnityEngine.UI;

public interface IUsebleInterFace
{
    public void Use();
}

public interface IImage
{
    public void GetImage(Sprite SpriteForLoot);
}

public interface IDrop
{
    public void Drop(string WhatDrop);
}