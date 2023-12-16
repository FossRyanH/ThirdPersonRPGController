using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickup : Collectible
{
    [SerializeField]
    float _rotationSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GameObject.FindGameObjectWithTag("SoundManager");
        _scoreValue = 100;
    }

    void Update()
    {
        RotateCoin(_rotationSpeed);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
            Destroy(this.gameObject, 0.3f);
        else
            return;
    }

    void RotateCoin(float rotationSpeed)
    {
        transform.Rotate(new Vector3(rotationSpeed,0f, 0f) * Time.deltaTime);
    }
}
