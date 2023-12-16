using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    int _damageAmount;
    List<Collider> _collidedWithThisRound = new List<Collider>();

    void OnEnable()
    {
        _collidedWithThisRound.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) { return; }

        if (_collidedWithThisRound.Contains(other)) { return; }

        _collidedWithThisRound.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(_damageAmount);
        }
    }

    public void SetAttackStrength(int damageAmount)
    {
        this._damageAmount = damageAmount;
    }
}
