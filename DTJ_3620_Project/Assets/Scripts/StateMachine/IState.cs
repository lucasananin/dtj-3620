using UnityEngine;

public interface IState
{
    void Awake(StateController _controller);
    void OnEnter();
    void OnExit();
    void OnUpdate();
    string GetStateName();
}
