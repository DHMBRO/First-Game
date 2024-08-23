using UnityEditor.Build.Content;
using UnityEngine;

public class SoundControlerForWeapon : MonoBehaviour
{
    [SerializeField] ShootControler WeaponControler;
    [SerializeField] AudioSource SoundSource;

    void Start()
    {
        if (WeaponControler)
        {
            WeaponControler.SetShootDelegat += PlaySound;
        }

    }

    private void PlaySound()
    {
        if (!SoundSource.clip)
        {
            Debug.Log("Not set SoundSource-AudioSource ! " + gameObject.name);
            return;
        }

        if (WeaponControler.GetShootTime() < Time.time && WeaponControler.HasAmmo())
        {
            SoundSource.Play();
        }
    }
}
