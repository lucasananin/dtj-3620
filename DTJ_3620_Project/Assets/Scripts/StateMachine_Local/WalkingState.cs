using UnityEngine;

public class WalkingState : IState
{
    private StateController _controller = null;

    public void Awake(StateController _controller)
    {
        this._controller = _controller;
    }

    public void OnEnter()
    {
        _controller.MoveEnvironment();
        _controller.PlayWalkingTimer();
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }

    public string GetStateName()
    {
        return $"Walking";
    }
}
