using UnityEngine;

public class IdleState : IState
{
    private StateController _controller = null;

    public void Awake(StateController _controller)
    {
        this._controller = _controller;
    }

    public void OnEnter()
    {
        // show tutorial screen.
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }

    public string GetStateName()
    {
        return $"Idle";
    }
}
