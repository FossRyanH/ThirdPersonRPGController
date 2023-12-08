using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        _stateMachine.PlayerController.CancelTargetEvent += OnCancelTarget;

        _stateMachine.Animator.Play(TargetingBlendTreeHash);
    }
    
    public override void Tick(float deltaTime)
    {
        if (_stateMachine.Targeter.CurrentTarget == null)
        {
            _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
            return;
        }
        Vector3 movement = CalcMovement();

        Move(movement * _stateMachine.TargetingMovementSpeed);

        FaceTarget();
    }

    public override void ExitState()
    {
        _stateMachine.PlayerController.CancelTargetEvent -= OnCancelTarget;
    }

    void OnCancelTarget()
    {
        _stateMachine.Targeter.CancelTarget();

        _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
    }

    Vector3 CalcMovement()
    {
        Vector3 movement = new Vector3();

        movement += _stateMachine.transform.right * _stateMachine.PlayerController.MovementValue.x;
        movement += _stateMachine.transform.forward * _stateMachine.PlayerController.MovementValue.y;

        return movement;
    }
}
