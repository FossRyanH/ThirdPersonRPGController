using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : Collectible
{
    void Start()
    {
        _scoreValue = 25;
        _audioSource = GameObject.FindGameObjectWithTag("SoundManager");
    }

    protected override void OnTriggerEnter(Collider other)
    {
        float _restoreAmount = 30f;


        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>()?.RestoreHealth(_restoreAmount);
            Destroy(this.gameObject, 1f);
        }
        else
        {
            return;
        }
    }
}
