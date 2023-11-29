using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    State _currentState;

    void Update()
    {
        if (_currentState != null)
        {
            _currentState.Tick(Time.deltaTime);
        }
    }

    public void SwitchState(State newState)
    {
        if (_currentState != null)
        {
            _currentState.ExitState();
        }
            _currentState = newState;
            _currentState.EnterState();
    }
}
