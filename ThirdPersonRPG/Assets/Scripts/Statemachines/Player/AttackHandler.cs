using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField]
    GameObject _rightHand;
    [SerializeField]
    GameObject _leftHand;
    [SerializeField]
    GameObject _rightFoot;

    void RightHook()
    {
        _rightHand.SetActive(true);
    }

    void DisableRightHand()
    {
        _rightHand.SetActive(false);
    }

    void UpperCutLeft()
    {
        _leftHand.SetActive(true);
    }

    void DisableUppercut()
    {
        _leftHand.SetActive(false);
    }

    void Kick()
    {
        _rightFoot.SetActive(true);
    }

    void DisableFoot()
    {
        _rightFoot.SetActive(false);
    }
}
