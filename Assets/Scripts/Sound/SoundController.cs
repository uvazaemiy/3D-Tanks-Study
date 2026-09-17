using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [Space]
    [SerializeField] private AudioClip _dragon_shoot_sound;
    [SerializeField] private AudioClip _riffle_shoot_sound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _audioSource.PlayOneShot(_dragon_shoot_sound);
            Debug.Log(_dragon_shoot_sound.name);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _audioSource.PlayOneShot(_riffle_shoot_sound);
            Debug.Log(_riffle_shoot_sound.name);
        }
    }
}
