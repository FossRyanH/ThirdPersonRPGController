using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Slider HealthBar;
    [SerializeField]
    int _pointValue = 75;

    GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GetComponentInChildren<Slider>();

        _target = GameObject.FindGameObjectWithTag("Player");
        if (_target == null)
        {
            Debug.LogWarning("Player not found");
        }
    }

    void Update()
    {
        FacePlayer();
    }

    public void SetHealthMaxValue(float maxHealth)
    {
        HealthBar.maxValue = maxHealth;
    }

    public void SetCurrentHealth(float currentHealth)
    {
        HealthBar.value = currentHealth;
    }

    void FacePlayer()
    {
        if (_target == null) { return; }
        Vector3 faceDirection = _target.transform.position - transform.position;
        faceDirection.y = 0f;

        transform.rotation = Quaternion.LookRotation(faceDirection);
    }

    void OnDestroy()
    {
        _target.GetComponent<PlayerController>()?.AddScore(_pointValue);
    }
}
