using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField]
    public PlayerController PlayerController { get; private set; }
    [field: SerializeField]
    public float MovementSpeed { get; private set; }
    [field: SerializeField]
    public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField]
    public float RotationDamping { get; private set; }
    [field: SerializeField]
    public Animator Animator { get; private set; }
    [field: SerializeField]
    public Targeter Targeter { get; private set; }
    [field: SerializeField]
    public Attack[] Attacks { get; private set; }

    public Transform MainCameraTransform { get; private set; }

    void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
}
