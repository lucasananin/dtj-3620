using System.Collections;
using TMPro;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField] Vector2 _walkingDurationRange = new(2f, 4f);
    [SerializeField] float _finalWalkDuration = 3f;

    [Header("// REFERENCES")]
    [SerializeField] CombatHandler _combat = null;
    [SerializeField] BackgroundMover _environment = null;

    [Header("// DEBUG")]
    [SerializeField] string _currentStateName = null;

    private StateMachine _machine;

    private void Start()
    {
        _machine = new StateMachine();

        _machine.ChangeState(new IdleState(), this);
        //PlayWalkingState();
    }

    private void Update()
    {
        _machine.Update();

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    _machine.ChangeState(new WalkingState(), this);
        //}

        _currentStateName = _machine.GetCurrentStateName();
    }

    internal void PlayWalkingTimer()
    {
        StartCoroutine(WalkingRoutine());
    }

    private IEnumerator WalkingRoutine()
    {
        if (CombatHandler.Instance.HasDefeatedAllEnemies())
        {
            yield return new WaitForSeconds(_finalWalkDuration);
            EndgamePanel.Instance.Init(true);
            yield break;
        }

        var _walkingDuration = Random.Range(_walkingDurationRange.x, _walkingDurationRange.y);
        yield return new WaitForSeconds(_walkingDuration);
        _machine.ChangeState(new CombatState(), this);
    }

    internal void StartCombat()
    {
        _combat.StartCombat();
    }

    public void PlayWalkingState()
    {
        _machine.ChangeState(new WalkingState(), this);
    }

    public void MoveEnvironment()
    {
        _environment.Play();
    }

    public void StopEnvironment()
    {
        _environment.Stop();
    }

    //public void EndCombat()
    //{
    //}
}
