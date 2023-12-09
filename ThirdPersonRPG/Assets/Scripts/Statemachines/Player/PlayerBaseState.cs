using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine _stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion)
    {
        _stateMachine.PlayerController.Rb.velocity = motion * _stateMachine.MovementSpeed;
    }

    protected void FaceTarget()
    {
        if (_stateMachine.Targeter.CurrentTarget == null)
        {
            return;
        }
        Vector3 lookPosition = _stateMachine.Targeter.CurrentTarget.transform.position - _stateMachine.transform.position;
        lookPosition.y = 0f;

        _stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
    }
}
