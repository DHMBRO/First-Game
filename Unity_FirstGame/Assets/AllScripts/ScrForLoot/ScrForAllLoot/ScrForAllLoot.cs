using UnityEngine;

public class ScrForAllLoot : MonoBehaviour, IImage
{
    [SerializeField] public float Mass;
    //[SerializeField] public TypeLoot The;
    [SerializeField] public Sprite SpriteForLoot;
    

    private void Start()
    {
        if (SpriteForLoot) GetImage(SpriteForLoot);
        else Debug.Log(gameObject.name + "Dont have reference to SpriteForLoot");
        
    }

    public void GetImage(Sprite SpriteForLoot)
    {

    }

}
