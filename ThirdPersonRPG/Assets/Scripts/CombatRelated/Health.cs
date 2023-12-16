using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float MaxHealth = 100f;
    [SerializeField]
    public float CurrentHealth;

    public event Action OnTakeDamage;

    public event Action OnRestoreHealth;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void DealDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        
        CurrentHealth = Mathf.Max(CurrentHealth - damageAmount, 0);

        OnTakeDamage?.Invoke();

        if (CurrentHealth <= 0)
        {
            Destroy(this.gameObject, 0.25f);
        }
    }

    public void RestoreHealth(float restoreAmount)
    {
        CurrentHealth += restoreAmount;

        OnRestoreHealth?.Invoke();

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
}
