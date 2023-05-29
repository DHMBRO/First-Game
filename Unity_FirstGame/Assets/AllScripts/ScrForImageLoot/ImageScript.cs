using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour, IImage
{
    Image m_image;
    void Start() 
    {
        m_image = GetComponent<Image>();
    }
    public void GetImage(Sprite SpriteForLoot)
    {
        m_image.sprite = SpriteForLoot;
    }
}
