using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPElements : MonoBehaviour
{
    [SerializeField]
    GameObject _healthBar;

    [SerializeField]
    GameObject _healthBarSprite;


    // Start is called before the first frame update
    void Start()
    {
        _healthBarSprite = GameObject.FindGameObjectWithTag("HealthBarFill");
    }

    public void SetHealthMaxValue(float maxHealth)
    {
        _healthBar.GetComponent<Slider>().maxValue = maxHealth;
    }

    public void SetHealthLevel(float currentHealth)
    {
        _healthBar.GetComponent<Slider>().value = currentHealth;

        if (currentHealth >= (_healthBar.GetComponent<Slider>().maxValue * 0.8f))
        {
            _healthBarSprite.GetComponent<Image>().color = new Color32(71, 255, 0, 188);
        }
        else if (currentHealth >= (_healthBar.GetComponent<Slider>().maxValue * 0.45f))
        {
            _healthBarSprite.GetComponent<Image>().color = new Color32(255, 201, 0, 188);
        }
        else if (currentHealth > _healthBar.GetComponent<Slider>().minValue)
        {
            _healthBarSprite.GetComponent<Image>().color = new Color32(255, 0, 0, 188);
        }
    }
}
