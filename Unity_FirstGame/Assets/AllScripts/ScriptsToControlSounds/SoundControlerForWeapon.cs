using UnityEditor.Build.Content;
using UnityEngine;

public class SoundControlerForWeapon : MonoBehaviour
{
    [SerializeField] ShootControler WeaponControler;
    [SerializeField] AudioSource SoundSource;

    
    public void PlaySound()
    {
        if (!SoundSource.clip)
        {
            Debug.Log("Not set SoundSource-AudioSource ! " + gameObject.name);
            return;
        }

        SoundSource.Play();

    }
}
