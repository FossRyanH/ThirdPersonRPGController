using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField]
    GameObject _damageHitBox;

    void AttackArea()
    {
        _damageHitBox.SetActive(true);
    }

    void AttackAreaDisable()
    {
        _damageHitBox.SetActive(false);
    }
}
