using UnityEngine;

public class ScrForAllLoot : MonoBehaviour, IImage
{
    [SerializeField] public float Mass;
    [SerializeField] public bool CanCombining;
    [SerializeField] public bool HaveDescription;

    [SerializeField] public Sprite SpriteForLoot;
    [SerializeField] public string ObjectDescription;


    private void Start()
    {
        if (SpriteForLoot) GetImage(SpriteForLoot);
        else Debug.Log(gameObject.name + "Dont have reference to SpriteForLoot");
        
    }

    public void GetImage(Sprite SpriteForLoot)
    {

    }

}
