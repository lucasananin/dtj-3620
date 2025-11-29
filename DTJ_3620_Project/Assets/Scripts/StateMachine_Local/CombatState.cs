using UnityEngine;

public class CombatState : IState
{
    private StateController _controller = null;

    public void Awake(StateController _controller)
    {
        this._controller = _controller;
    }

    public void OnEnter()
    {
        _controller.StartCombat();
    }

    public void OnExit()
    {
        //_controller.EndCombat();
    }

    public void OnUpdate()
    {
    }

    public string GetStateName()
    {
        return $"Combat";
    }
}
