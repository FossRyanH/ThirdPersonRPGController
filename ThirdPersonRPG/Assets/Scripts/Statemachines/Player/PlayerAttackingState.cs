using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttackingState : PlayerBaseState
{
    Attack _attack;
    float _previousFrameTime;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        _attack = _stateMachine.Attacks[attackIndex];
    }

    public override void EnterState()
    {
        _stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        FaceTarget();

        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= _previousFrameTime && normalizedTime < 1f)
        {
            if (_stateMachine.PlayerController.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (_stateMachine.Targeter.CurrentTarget != null)
            {
                _stateMachine.SwitchState(new PlayerTargetingState(_stateMachine));
            }
            else
            {
                _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
            }
        }

        _previousFrameTime = normalizedTime;
    }
    
    public override void ExitState()
    {
        //
    }

    void TryComboAttack(float normalizedTime)
    {
        if (_attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < _attack.ComboAttackTime) { return; }

        _stateMachine.SwitchState(new PlayerAttackingState(_stateMachine, _attack.ComboStateIndex));
    }

    float GetNormalizedTime()
    {
        AnimatorStateInfo currentStateInfo = _stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextStateInfo = _stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (_stateMachine.Animator.IsInTransition(0) && nextStateInfo.IsTag("Attack"))
        {
            return nextStateInfo.normalizedTime;
        }
        else if (!_stateMachine.Animator.IsInTransition(0) && currentStateInfo.IsTag("Attack"))
        {
            return currentStateInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
