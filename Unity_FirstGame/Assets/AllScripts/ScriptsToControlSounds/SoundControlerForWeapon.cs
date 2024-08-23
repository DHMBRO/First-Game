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
        if (WeaponControler.GetShootTime() < Time.time)
        {
            SoundSource.Play();
        }
    }
}
