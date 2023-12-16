using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowWave : MonoBehaviour
{
    Light _light;

    [SerializeField]
    float _glowMaxrange = 7.5f;

    [SerializeField]
    float _shimmerMaxBrightness = 2f;


    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        ShimmerGlow(_shimmerMaxBrightness);
        LightRange(_glowMaxrange);
    }

    void ShimmerGlow(float value)
    {
        _light.intensity = Mathf.PingPong(Time.time, value);
    }

    void LightRange(float range)
    {
        _light.range = Mathf.Sin(Time.time) * range;
    }
}
