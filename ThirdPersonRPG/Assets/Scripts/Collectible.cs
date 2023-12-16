using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected int _scoreValue = 50;
    [SerializeField]
    protected AudioClip _activationSound;
    protected GameObject _audioSource;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().AddScore(_scoreValue);
            PlaySound(_activationSound);
        }
    }

    protected void PlaySound(AudioClip sound)
    {
        _audioSource.GetComponent<AudioSource>()?.PlayOneShot(sound);
    }
}
