using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int _maxHealth = 100;
    [SerializeField]
    int _currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void DealDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        
        _currentHealth = Mathf.Max(_currentHealth - damageAmount, 0);

        if (_currentHealth <= 0)
        {
            Destroy(this.gameObject, 0.25f);
        }

        Debug.Log(_currentHealth);
    }
}
