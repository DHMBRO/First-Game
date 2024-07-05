using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PostPocController : MonoBehaviour
{
    [SerializeField] Color NightVisionColor ;
    [SerializeField] Color NormalVisionColor ;

    [SerializeField] bool TurnedOn = false;
    UnityEngine.Rendering.Volume MyVolume;
    void Start()
    {
        
        MyVolume = gameObject.GetComponent<UnityEngine.Rendering.Volume>();
        MyVolume.weight = 0.0f;
        RenderSettings.ambientLight = NormalVisionColor;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            SwitchMode();
        }
    }
    void SwitchMode() 
    {
        TurnedOn = !TurnedOn;
        if (TurnedOn) 
        {
            MyVolume.weight = 1.0f;
            RenderSettings.ambientLight = NightVisionColor;
        }
        else
        {
            MyVolume.weight = 0.0f;
            RenderSettings.ambientLight = NormalVisionColor;
        }
    }
}
