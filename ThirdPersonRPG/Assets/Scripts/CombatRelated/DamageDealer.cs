using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    int _damageAmount;
    List<Collider> _collidedWithThisRound = new List<Collider>();

    GameObject _soundManager;

    void Start()
    {
        _soundManager = GameObject.FindGameObjectWithTag("SoundManager");
    }

    void OnEnable()
    {
        _collidedWithThisRound.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { return; }

        if (_collidedWithThisRound.Contains(other)) { return; }

        _collidedWithThisRound.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(_damageAmount);
            PlayAttackSound();
        }
    }

    public void SetAttackStrength(int damageAmount)
    {
        this._damageAmount = damageAmount;
    }

    void PlayAttackSound()
    {
        int soundSelection;
        soundSelection = Random.Range(0, _soundManager.GetComponent<SoundFX>().AttackBanks.Length);

        AudioClip randomAttackSound = _soundManager.GetComponent<SoundFX>()?.AttackBanks[soundSelection];
        _soundManager.GetComponent<AudioSource>()?.PlayOneShot(randomAttackSound);
    }
}
