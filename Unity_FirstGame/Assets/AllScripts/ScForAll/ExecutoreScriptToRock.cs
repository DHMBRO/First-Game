using UnityEngine;

public class ExecutoreScriptToRock : MonoBehaviour
{
    [SerializeField] SoundCreatorScript SoundScript;
    [SerializeField] float RadiuseNoice = 10.0f;

    private void OnCollisionEnter(Collision collision)
    {
        SoundScript = gameObject.GetComponent<SoundCreatorScript>();
        if (SoundScript) SoundScript.CreateNoise(RadiuseNoice);

        Destroy(gameObject);

    }

}
