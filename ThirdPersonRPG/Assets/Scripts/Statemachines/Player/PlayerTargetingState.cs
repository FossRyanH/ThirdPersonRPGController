using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    readonly int TargetingForwardHash = Animator.StringToHash("TargetingForwardSpeed");
    readonly int TargetingHorizontalHash = Animator.StringToHash("TargetingHorizontalSpeed");

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        _stateMachine.PlayerController.CancelEvent += OnCancel;

        _stateMachine.Animator.Play(TargetingBlendTreeHash);
    }
    
    public override void Tick(float deltaTime)
    {
        if (_stateMachine.PlayerController.IsAttacking)
        {
            _stateMachine.SwitchState(new PlayerAttackingState(_stateMachine, 0));
            return;
        }

        if (_stateMachine.Targeter.CurrentTarget == null)
        {
            _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
            return;
        }

        Vector3 movement = CalcMovement();

        Move(movement * _stateMachine.TargetingMovementSpeed);

        UpdateAnimator(deltaTime);

        FaceTarget();
    }

    public override void ExitState()
    {
        _stateMachine.PlayerController.CancelEvent -= OnCancel;
    }

    void OnCancel()
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

    void UpdateAnimator(float deltaTime)
    {
        if (_stateMachine.PlayerController.MovementValue.y == 0f)
        {
            _stateMachine.Animator.SetFloat(TargetingForwardHash, 0f, 0.1f, deltaTime);
        }
        else
        {
            float value = _stateMachine.PlayerController.MovementValue.y > 0 ? 1f : -1f;
            _stateMachine.Animator.SetFloat(TargetingForwardHash, value, 0.1f, deltaTime);
        }

        if (_stateMachine.PlayerController.MovementValue.x == 0f)
        {
            _stateMachine.Animator.SetFloat(TargetingHorizontalHash, 0f, 0.1f, deltaTime);
        }
        else
        {
            float value = _stateMachine.PlayerController.MovementValue.x > 0 ? 1f : -1f;
            _stateMachine.Animator.SetFloat(TargetingHorizontalHash, value, 0.1f, deltaTime);
        }
    }
}
