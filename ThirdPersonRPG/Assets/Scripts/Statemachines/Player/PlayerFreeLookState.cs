using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookMovement");
    const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        _stateMachine.PlayerController.TargetEvent += OnTarget;
        
        _stateMachine.Animator.Play(FreeLookBlendTreeHash);
    }
    
    public override void Tick(float deltaTime)
    {
        Vector3 movementVector = CalcMovement();

        // Takes the player Input and multiplies it by the states movement speed.
        Move(movementVector);

        if (_stateMachine.PlayerController.MovementValue == Vector2.zero)
        {
            _stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f, AnimatorDampTime, deltaTime);
            return;
        }
        _stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0.5f, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movementVector, deltaTime);
    }

    public override void ExitState()
    {
        _stateMachine.PlayerController.TargetEvent -= OnTarget;
    }

    Vector3 CalcMovement()
    {
        Vector3 camForwardMove = _stateMachine.MainCameraTransform.forward;
        Vector3 camRightMove = _stateMachine.MainCameraTransform.right;

        camForwardMove.y = 0f;
        camRightMove.y = 0f;

        camForwardMove.Normalize();
        camRightMove.Normalize();

        return camForwardMove * _stateMachine.PlayerController.MovementValue.y + camRightMove * _stateMachine.PlayerController.MovementValue.x;
    }

    void FaceMovementDirection(Vector3 movementVector, float deltaTime)
    {
        _stateMachine.transform.rotation = Quaternion.Lerp(_stateMachine.transform.rotation, Quaternion.LookRotation(movementVector), deltaTime * _stateMachine.RotationDamping);
    }

    void OnTarget()
    {
        if (!_stateMachine.Targeter.SelectTarget())
        {
            return;
        }

        _stateMachine.SwitchState(new PlayerTargetingState(_stateMachine));
    }
}
