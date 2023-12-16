using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field:Header("Components")]
    [field: SerializeField]
    public PlayerController PlayerController { get; private set; }
    [field: SerializeField]
    public Targeter Targeter { get; private set; }
    [field: SerializeField]
    public Animator Animator { get; private set; }
    [field: SerializeField]
    public CapsuleCollider Collider { get; private set; }

    [field: Header("Combat Components & Variables")]
    [field: SerializeField]
    public DamageDealer DamageDealer { get; private set; }
    [field: SerializeField]
    public Attack[] Attacks { get; private set; }

    [field:Header("Control Variables")]
    [field: SerializeField]
    public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField]
    public float RotationDamping { get; private set; }
    [field: SerializeField]
    public float MovementSpeed { get; private set; }

    public Transform MainCameraTransform { get; private set; }


    void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
}
