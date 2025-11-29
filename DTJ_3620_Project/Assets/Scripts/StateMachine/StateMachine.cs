using UnityEngine;

public class StateMachine
{
    private IState _currentState;

    public void ChangeState(IState newState, StateController _controller)
    {
        if (_currentState == newState)
            return;

        _currentState?.OnExit();
        _currentState = newState;
        _currentState?.Awake(_controller);
        _currentState?.OnEnter();
    }

    public void Update()
    {
        _currentState?.OnUpdate();
    }

    public string GetCurrentStateName()
    {
        return _currentState.GetStateName();
    }
}
