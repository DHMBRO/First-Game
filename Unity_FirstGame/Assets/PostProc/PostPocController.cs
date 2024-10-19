using System.Collections;
using UnityEngine;

public class PostPocController : MonoBehaviour
{
    [SerializeField] Color NightVisionColor;
    [SerializeField] Color NormalVisionColor;

    bool CanSwitch = true;
    bool TurnedOn = false;

    [SerializeField, Range(0.0f, 10.0f)] float TurnOnDuration = 1.0f;
    [SerializeField, Range(0.0f, 10.0f)] float TurnOffDuration = 1.0f;
    [SerializeField, Range(0.0f, 10.0f)] float SwitchPow = 1.0f;

    UnityEngine.Rendering.Volume NVVolume;
    AudioSource NVAudioSource;

    void Start()
    {
        NVVolume = gameObject.GetComponent<UnityEngine.Rendering.Volume>();
        NVVolume.weight = 0.0f;
        RenderSettings.ambientLight = NormalVisionColor;

        NVAudioSource = GetComponent<AudioSource>();
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
        if (!CanSwitch)
        {
            return;
        }
        CanSwitch = false;

        TurnedOn = !TurnedOn;
        StartCoroutine(SmoothSwitchMode());
    }

    IEnumerator SmoothSwitchMode()
    {
        if (TurnedOn)
        {
            NVAudioSource.Play();
        }

        Color StartColor = !TurnedOn ? NightVisionColor : NormalVisionColor;
        Color EndColor = TurnedOn ? NightVisionColor : NormalVisionColor;
        float StartVolumeWeight = !TurnedOn ? 1.0f : 0.0f;
        float EndVolumeWeight = TurnedOn ? 1.0f : 0.0f;

        float TimeElapsed = 0.0f;
        float SwitchDuration = TurnedOn ? TurnOnDuration : TurnOffDuration;
        while (TimeElapsed < SwitchDuration)
        {
            float LerpT = TimeElapsed / SwitchDuration;
            LerpT = Mathf.Pow(LerpT, SwitchPow);

            NVVolume.weight = Mathf.Lerp(StartVolumeWeight, EndVolumeWeight, LerpT);
            RenderSettings.ambientLight = Color.Lerp(StartColor, EndColor, LerpT);

            TimeElapsed += Time.deltaTime;
            yield return null;
        }
        NVVolume.weight = EndVolumeWeight;
        RenderSettings.ambientLight = EndColor;

        CanSwitch = true;
    }
}
