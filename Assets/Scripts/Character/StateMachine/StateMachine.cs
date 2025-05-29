using System.Collections.Generic;
using System;
using UnityEngine;

public class StateMachine
{
    private State _currentState;
    private Dictionary<Type, State> _states = new Dictionary<Type, State>();

    public void AddState(State state)
    {
        _states.Add(state.GetType(), state);
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }

    public void SetState<T>() where T : State
    {
        var type = typeof(T);

        if(_currentState?.GetType() == type)
        {
            return;
        }

        if (_states.TryGetValue(type, out State state))
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}
