using System.Collections;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvas = null;
    [SerializeField] PlayerData _playerData = default;
    [Space]
    [SerializeField] CanvasView _view = null;
    [SerializeField] DamageDisplay _damageUI = null;
    [SerializeField] StateController _state = null;
    [Space]
    [SerializeField] float _firstDuration = 2f;
    [SerializeField] float _secondDuration = 2f;
    [SerializeField] EnemyData[] _enemies = null;

    private EnemyData _enemyData = null;
    private EnemyHealth _enemyHealth = null;
    private PlayerHealth _playerHealth = null;

    public enum Turn
    {
        Player,
        Enemy
    }

    [Header("// DEBUG")]
    [SerializeField] Turn currentTurn = Turn.Player;

    public Turn CurrentTurn { get => currentTurn; }

    void Start()
    {
        _playerHealth = FindFirstObjectByType<PlayerHealth>();

        UpdateUI();
        //StartCombat();
    }

    public void StartCombat()
    {
        _view.Show();

        var _index = Random.Range(0, _enemies.Length);
        _enemyData = _enemies[_index];

        var _instance = Instantiate(_enemyData.Prefab, Vector3.zero, Quaternion.identity);
        _enemyHealth = _instance.GetComponent<EnemyHealth>();

        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        currentTurn = Turn.Player;
        UpdateUI();
    }

    public void PlayerAttack()
    {
        StartCoroutine(PlayerAttack_Routine());
    }

    IEnumerator PlayerAttack_Routine()
    {
        if (currentTurn != Turn.Player) yield break;

        _canvas.interactable = false;

        yield return new WaitForSeconds(_firstDuration);

        var _hasHit = Random.Range(0, 100) < _playerData.HitChance;

        if (_hasHit)
        {
            var _damage = Random.Range(_enemyData.DamageRange.x, _enemyData.DamageRange.y);
            var _bonus = _playerData.AttackType == _enemyData.Weakness ? 3 : 1;
            _enemyHealth.TakeDamage(_damage * _bonus);
            _damageUI.Play($"{_damage * _bonus}", _bonus == 1 ? Color.white : Color.yellow);
            Debug.Log("Player Hit");

            if (!_enemyHealth.IsAlive())
            {
                // wait enemy death animation.
                yield return new WaitForSeconds(_secondDuration);
                _state.PlayWalkingState();
                _view.Hide();
                yield break;
            }
        }
        else
        {
            _damageUI.Play("Miss", Color.white);
            Debug.Log("Player Missed");
        }

        yield return new WaitForSeconds(_secondDuration);

        StartEnemyTurn();
    }

    void StartEnemyTurn()
    {
        currentTurn = Turn.Enemy;
        UpdateUI();
        StartCoroutine(EnemyTurnRoutine());
    }

    private IEnumerator EnemyTurnRoutine()
    {
        yield return new WaitForSeconds(_firstDuration);

        var _hasHit = Random.Range(0, 100) < _enemyData.HitChance;

        if (_hasHit)
        {
            var _damage = Random.Range(_enemyData.DamageRange.x, _enemyData.DamageRange.y);
            _playerHealth.TakeDamage(_damage);
            _damageUI.Play($"{_damage}", Color.red);
            Debug.Log("Enemy Hit");
        }
        else
        {
            _damageUI.Play("Miss", Color.red);
            Debug.Log("Enemy Missed");
        }

        if (!_playerHealth.IsAlive())
        {
            // GAME OVER.
            yield return new WaitForSeconds(_secondDuration);
            _view.Hide();
            yield break;
        }

        yield return new WaitForSeconds(_secondDuration);

        StartPlayerTurn();
    }

    void UpdateUI()
    {
        // Player can only press the button during their own turn
        _canvas.interactable = (currentTurn == Turn.Player);
    }
}
