using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    float _timer = 5f;

    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Enter");
    }
    
    public override void Tick(float deltaTime)
    {
        // Debug.Log("Tick");
        _timer -= deltaTime;
        Debug.Log(_timer);
        if (_timer <= 0)
        {
            _stateMachine.SwitchState(new PlayerTestState(_stateMachine));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit");
    }
}
