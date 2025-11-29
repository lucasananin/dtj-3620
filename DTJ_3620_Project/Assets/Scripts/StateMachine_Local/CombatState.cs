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
        // spawn enemy.
        // show combat ui.
        // start turn system.
    }

    public void OnExit()
    {
        // hide combat ui.
    }

    public void OnUpdate()
    {
        // check turns.
        // if enemy turn, enemy attacks.
        // if player turn, wait player input.
        // combat ui disable = isEnemyTurn.
    }

    public string GetStateName()
    {
        return $"Walking";
    }
}
