using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarHandler : MonoBehaviour
{
     EnemyUI _enemyUI;

    [SerializeField]
    Health _healthBar;

    // Start is called before the first frame update
    void Start()
    {
        _enemyUI = GetComponentInChildren<EnemyUI>();
        _healthBar = GetComponent<Health>();

        UpdateMaxHealth(_healthBar.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(_healthBar.CurrentHealth);
    }

    void UpdateMaxHealth(float maxHealth)
    {
        _enemyUI.SetHealthMaxValue(maxHealth);
    }

    public void SetHealth(float currentHealth)
    {
        _enemyUI.SetCurrentHealth(currentHealth);
    }
}
