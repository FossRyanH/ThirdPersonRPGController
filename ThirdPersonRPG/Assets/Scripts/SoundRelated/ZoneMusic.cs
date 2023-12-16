using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneMusic : MonoBehaviour
{
    [SerializeField]
    AudioClip _townMusic;
    [SerializeField]
    AudioClip _fieldMusic;

    [SerializeField]
    GameObject _player;

    GameObject _musicHandler;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _musicHandler = GameObject.FindGameObjectWithTag("MusicHandler");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_musicHandler.GetComponent<AudioSource>().isPlaying)
            {
                _musicHandler.GetComponent<AudioSource>().Stop();
            }
            _musicHandler.GetComponent<AudioSource>().volume = 1f;
            _musicHandler.GetComponent<SoundManager>()?.ChangeMusic(_townMusic);
        }
        else
        {
            return;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_musicHandler.GetComponent<AudioSource>().isPlaying)
            {
                _musicHandler.GetComponent<AudioSource>().Stop();
            }
            _musicHandler.GetComponent<AudioSource>().volume = 0.3f;
            _musicHandler.GetComponent<SoundManager>()?.ChangeMusic(_fieldMusic);
        }
        else
        {
            return;
        }
    }
}
